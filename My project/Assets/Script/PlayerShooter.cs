using UnityEngine;

public class Shooter : MonoBehaviour
{
    [SerializeField] private ProjectilePool pool;
    [SerializeField] private Transform pontoDeDisparo;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Atirar();
        }
    }

    private void Atirar()
    {
        Projetil projetil = pool.PegarProjetil();

        projetil.transform.position = pontoDeDisparo.position;

        projetil.Atirar(Vector2.up);
    }
}