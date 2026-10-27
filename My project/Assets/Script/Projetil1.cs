using UnityEngine;
using System.Collections;

public class Projetil1 : MonoBehaviour
{
    public float velocidade;
    public int dano;
    private Rigidbody2D rb;



    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void Atirar(Vector2 direcao)
    {
        gameObject.SetActive(true);

        rb.linearVelocity = direcao.normalized * velocidade;
    }


}