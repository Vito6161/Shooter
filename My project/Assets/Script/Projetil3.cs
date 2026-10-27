using UnityEngine;
using System.Collections;

public class Projetil3 : MonoBehaviour
{
    public float velocidade;
    public int dano;
    private Rigidbody2D rb;

    void OnEnable()
    {
        StartCoroutine(MovimentoProjetil());
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

}