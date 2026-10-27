using UnityEngine;
using System.Collections;

public class Projetil2 : MonoBehaviour
{
    public float velocidade;
    public int dano;    
    private float intervaloVariacaoX = 0.45f;
    private float countX;
    private int sentidoX = 1;


    private Rigidbody2D rb;


    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if(countX < intervaloVariacaoX)
        {
            countX += Time.deltaTime;
        }
        else
        {
            countX = 0;
            sentidoX *= -1;
        }

        rb.linearVelocity = new Vector2(sentidoX * (velocidade / 1.5f), rb.linearVelocity.y);      

    }

}