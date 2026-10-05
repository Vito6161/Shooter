using UnityEngine;

public class Projetil : MonoBehaviour
{
    [SerializeField] private float velocidade = 15f;

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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        

        if(!collision.gameObject.CompareTag("Projetil") && !collision.gameObject.CompareTag("Player"))
        {
            rb.linearVelocity = Vector2.zero;

            gameObject.SetActive(false);
        }
    }
}