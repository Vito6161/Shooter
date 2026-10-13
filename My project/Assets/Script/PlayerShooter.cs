using UnityEngine;

public class Shooter : MonoBehaviour
{
    [SerializeField] private ProjectilePool pool;
    [SerializeField] private Transform pontoDeDisparo;
    [SerializeField] private float intervalo;
    private float count;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Atirar();
        }
       

        if(Input.GetMouseButton(0))
        {
            if(count < intervalo) 
            {
                count += Time.deltaTime; 
            }
            else 
            {
                Atirar(); 
                count = 0;
            }
        }
    }

    private void Atirar()
    {
        Projetil projetil = pool.PegarProjetil();

        projetil.transform.position = pontoDeDisparo.position;

        projetil.Atirar(Vector2.up);
    }
}