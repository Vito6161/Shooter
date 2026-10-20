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

        vida -= dano;
        vida = Mathf.Max(vida, 0);

        if (!gameObject.CompareTag("Player"))
        {
            AtualizarEstado();
        }

        if (vida <= 0)
            Morte();
    }

    private void AtualizarEstado()
    {
        float porcentagemVida = (float)vida / vidaMaxima;

        if (porcentagemVida > 0.5f)
        {
            state = 0;
        }

        else if (porcentagemVida > 0.2f)
        {
            state = 1;
        }
        else
        {
            state = 2;
        }

        if (state != estadoAnterior)
        {
            estadoAnterior = state;
            comportamento = 0;

            anim.SetBool("Transition", true);

            //StateOM.OnStateChanged(state);

            Debug.Log($"Boss: estado de vida {state}");

            if (gameObject.CompareTag("Boss"))
            {
                if (rotinaInvulnerabilidade != null)
                    StopCoroutine(rotinaInvulnerabilidade);

                rotinaInvulnerabilidade =
                    StartCoroutine(Invulnerabilidade());
            }
        }
    }

    private IEnumerator Invulnerabilidade()
    {
        anim.SetBool("Transition", true);
        anim.SetBool("Estado1", false);
        anim.SetBool("Estado2", false);
        anim.SetBool("Estado3", false);
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

        switch(state)
        {
            case 0:
                anim.SetBool("Estado1", true);
                break;
            case 1:
                anim.SetBool("Estado2", true);
                break;
            case 2:
                anim.SetBool("Estado3", true);
                break;
        }

        StateOM.OnStateChanged();

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
    public static event Action StateChanged;

    public static void OnStateChanged()
    {
        StateChanged?.Invoke();
    }

    public static event Action<bool> EndGame;

    public static void OnEndGame(bool resultado)
    {
        EndGame?.Invoke(resultado);
    }
}
