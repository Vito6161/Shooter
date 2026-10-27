using UnityEngine;
using System;
using System.Collections;

public class Vida : MonoBehaviour
{
    [SerializeField] public int vida;
    [SerializeField] private int vidaMaxima = 300;

    [Header("Invulnerabilidade do Boss")]
    [SerializeField] private float tempoInvulneravel = 2f;
    [SerializeField] private float intervaloPiscar = 0.1f;
    [SerializeField] private Color corInvulneravel = new Color(0.6f, 0f, 1f);

    public bool invulneravel { get; private set; }

    public int state;
    public int comportamento;

    private int estadoAnterior = -1;
    private SpriteRenderer spriteRenderer;
    private Color corOriginal;
    private Coroutine rotinaInvulnerabilidade;
    private Animator anim => GetComponent<Animator>();

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        if (spriteRenderer != null)
            corOriginal = spriteRenderer.color;
    }


    public void CausarDano(int dano)
    {
        // Impede que o boss receba dano durante a invulnerabilidade.
        if (gameObject.CompareTag("Boss") && invulneravel)
            return;

        if(!anim.GetBool("Transition")) vida -= dano;
        vida = Mathf.Max(vida, 0);

        

        if (!gameObject.CompareTag("Player"))
        {
            anim.SetFloat("Vida", vida);
        }

        if (vida <= 0)
            Morte();
    }



    public IEnumerator Invulnerabilidade()
    {
        invulneravel = true;

        float tempoDecorrido = 0f;
        bool roxo = false;

        while (tempoDecorrido < tempoInvulneravel)
        {
            roxo = !roxo;

            if (spriteRenderer != null)
                spriteRenderer.color = roxo ? corInvulneravel : corOriginal;

            yield return new WaitForSeconds(intervaloPiscar);
            tempoDecorrido += intervaloPiscar;
        }

        if (spriteRenderer != null)
            spriteRenderer.color = corOriginal;

        invulneravel = false;
        rotinaInvulnerabilidade = null;

        anim.SetBool("Transition", false);
    }

    public int ProximoComportamento()
    {
        comportamento = (comportamento + 1) % 3;
        return state * 3 + comportamento;
    }

    public void Morte()
    {
        vida = 0;

        if (rotinaInvulnerabilidade != null)
            StopCoroutine(rotinaInvulnerabilidade);

        invulneravel = false;

        if (spriteRenderer != null)
            spriteRenderer.color = corOriginal;

        bool temp;

        if (gameObject.CompareTag("Player"))
            temp = false;
        else if (gameObject.CompareTag("Boss"))
            temp = true;
        else
            temp = false;

        StateOM.OnEndGame(temp);
    }
}

public class StateOM
{

    public static event Action<bool> EndGame;

    public static void OnEndGame(bool resultado)
    {
        EndGame?.Invoke(resultado);
    }
}
