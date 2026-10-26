using UnityEngine;
using System.Collections;

public class Projetil3 : MonoBehaviour
{
    public float velocidade;
    public int dano;
    public Color cor;

    public SpriteRenderer sr => GetComponent<SpriteRenderer>();
    [SerializeField] private bool bossProjectile;

    private Rigidbody2D rb;

    void OnEnable()
    {
        StartCoroutine(MovimentoProjetil());
        
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