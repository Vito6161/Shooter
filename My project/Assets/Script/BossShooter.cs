//using System.Numerics;
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
            pontoDeDisparo = new Vector3(Random.Range(-transform.localScale.x, transform.localScale.x), transform.localScale.y, 0f);
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