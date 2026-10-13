using UnityEngine;

public class Projetil : MonoBehaviour
{
    [SerializeField] private float velocidade = 15f;
    [SerializeField] private int dano;

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
        if(!other.gameObject.CompareTag("Projetil"))
        {
            rb.linearVelocity = Vector2.zero;

            gameObject.SetActive(false);
        }

        if(other.gameObject.GetComponent<Vida>())
        {
            other.gameObject.GetComponent<Vida>().CausarDano(dano);
        }
    }
}