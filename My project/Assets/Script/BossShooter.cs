//using System.Numerics;
using Unity.VisualScripting;
using UnityEngine;

public class BossShooter : MonoBehaviour
{
    [SerializeField] private ProjectilePool pool;
    private Vector3 pontoDeDisparo;
    
    [SerializeField] private float intervalo;
    private float count;

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


}