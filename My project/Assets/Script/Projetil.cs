using UnityEngine;
using System.Collections;

public class Projetil : MonoBehaviour
{
    public float velocidade;
    public int dano;
    public Color cor;

    public SpriteRenderer sr => GetComponent<SpriteRenderer>();
    [SerializeField] private bool bossProjectile;
    
    
    public int projetilIndex;
    private float intervaloVariacaoX = 0.45f;
    private float countX;
    private int sentidoX = 1, sentidoY = 1;


    private Rigidbody2D rb;

    void OnEnable()
    {
        if(projetilIndex == 2)
        {
            StartCoroutine(MovimentoProjetil());
        }

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

    IEnumerator MovimentoProjetil()
    {
        // Desce inicialmente
        rb.linearVelocity = new Vector2(rb.linearVelocity.x, -velocidade);

        yield return new WaitForSeconds(0.45f);

        // Sobe um pouco
        rb.linearVelocity = new Vector2(rb.linearVelocity.x, velocidade * 0.5f);

        yield return new WaitForSeconds(0.15f);

        // Volta a descer
        rb.linearVelocity = new Vector2(rb.linearVelocity.x, -velocidade);

        yield return new WaitForSeconds(0.45f);

        // Sobe um pouco
        rb.linearVelocity = new Vector2(rb.linearVelocity.x, velocidade * 0.5f);

        yield return new WaitForSeconds(0.15f);

        // Volta a descer
        rb.linearVelocity = new Vector2(rb.linearVelocity.x, -velocidade);
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
        
        ////////////////////

        if(projetilIndex == 1 && bossProjectile) rb.linearVelocity = new Vector2(sentidoX * (velocidade / 1.5f), rb.linearVelocity.y);
        //else if (projetilIndex == 2 && bossProjectile) StartCoroutine(MovimentoProjetil());
        

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(!other.gameObject.CompareTag("Projetil") && (bossProjectile == other.gameObject.CompareTag("Player") || bossProjectile == other.gameObject.CompareTag("Parede") ))
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