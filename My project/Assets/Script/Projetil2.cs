using UnityEngine;
using System.Collections;

public class Projetil2 : MonoBehaviour
{
    public float velocidade;
    public int dano;
    public Color cor;

    public SpriteRenderer sr => GetComponent<SpriteRenderer>();
    
    private float intervaloVariacaoX = 0.45f;
    private float countX;
    private int sentidoX = 1;


    private Rigidbody2D rb;

    void OnEnable()
    {
        cor.a = 1;
        sr.color = cor;
    }


    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void Atirar(Vector2 direcao)
    {
        gameObject.SetActive(true);

        rb.linearVelocity = direcao.normalized * velocidade;
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

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Player") || other.gameObject.CompareTag("Parede"))
        {
            rb.linearVelocity = Vector2.zero;

            gameObject.SetActive(false);

            if(other.gameObject.GetComponent<Vida>())
            {
                other.gameObject.GetComponent<Vida>().CausarDano(dano);
            }
            
        }
    }
}