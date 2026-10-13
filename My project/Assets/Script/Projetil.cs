using UnityEngine;

public class Projetil : MonoBehaviour
{
    public float velocidade = 15f;
    public int dano;
    [SerializeField] private bool bossProjectile;

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

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(!other.gameObject.CompareTag("Projetil") && bossProjectile == other.gameObject.CompareTag("Player"))
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