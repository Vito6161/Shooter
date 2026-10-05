using UnityEngine;

public class BossShooter : MonoBehaviour
{
    [SerializeField] private ProjectilePool pool;
    [SerializeField] private Transform pontoDeDisparo;

    private void Update()
    {
        
    }

    private void Atirar()
    {
        Projetil projetil = pool.PegarProjetil();

        projetil.transform.position = pontoDeDisparo.position;

        projetil.Atirar(Vector2.down);
    }
}