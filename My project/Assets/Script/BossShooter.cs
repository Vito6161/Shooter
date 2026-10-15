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

    void Start()
    {
        UpdateStats(state.state);
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
        Projetil projetil = pool.PegarProjetil();

        projetil.transform.position = pontoDeDisparo;

        projetil.Atirar(Vector2.down);
    }

    private void UpdateStats(int index)
    {
        data = dataArray[index];
        intervalo = data.intervalo;
    }
}