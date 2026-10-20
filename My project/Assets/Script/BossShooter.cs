//using System.Numerics;
using UnityEngine;

public class BossShooter : MonoBehaviour
{
    [SerializeField] private States[] dataArray;
    private States data;
    [SerializeField] private ProjectilePool pool;
    private Vector3 pontoDeDisparo;
    
    private float intervalo;
    private float count;

    private Vida state => gameObject.GetComponent<Vida>();

    private Animator anim => GetComponent<Animator>();

    void Start()
    {
        UpdateStats();
    }

    void OnEnable()
    {
        StateOM.StateChanged += UpdateStats;
    }

    void OnDisable()
    {
        StateOM.StateChanged -= UpdateStats;
    }

    private void Update()
    {
        if(count < intervalo)
        {
            count += Time.deltaTime;
        }
        else
        {
            float largura = transform.lossyScale.x / 2f;
            float altura = transform.lossyScale.y / 2f;

            pontoDeDisparo = new Vector3(Random.Range(transform.position.x - largura, transform.position.x + largura), transform.position.y - altura, transform.position.z);
            Atirar();
            count = 0;
        }
    }

    private void Atirar()
    {
        if(!state.invulneravel)
        {
            Projetil projetil = pool.PegarProjetil();

            projetil.transform.position = pontoDeDisparo;

            projetil.Atirar(Vector2.down);
        }
        
    }

    private void UpdateStats()
    {
        int bossState = 0;

        if(anim.GetBool("Estado1"))
        {
            bossState = 0;
        }
        else if(anim.GetBool("Estado2"))
        {
            bossState = 1;
        }
        else if(anim.GetBool("Estado3"))
        {
             bossState = 2;
        }

        data = dataArray[bossState];

        intervalo = data.intervalo;
    }
}