
using UnityEngine;
using UnityEngine.Pool;

public class ProjectilePool : MonoBehaviour
{
    [SerializeField] private Projetil prefab;
    [SerializeField] private int quantidadeInicial = 20;

    private Animator anim => GameObject.FindGameObjectWithTag("Boss").GetComponent<Animator>();

    private int countProjetil = 0;
    private int criados = 0;

    [SerializeField] private States[] dataArray;
    private States data;

    private ObjectPool<Projetil> pool;

    private void OnEnable()
    {
        StateOM.StateChanged += UpdateData;
    }

    private void OnDisable()
    {
        StateOM.StateChanged -= UpdateData;
    }

    private void Awake()
    {
        data = dataArray[0];

        pool = new ObjectPool<Projetil>(
            CriarProjetil,
            null,
            DevolverParaPool,
            DestruirProjetil,
            true,
            quantidadeInicial
        );

        // Pré-carrega a quantidade inicial de projéteis.
        for (int i = 0; i < quantidadeInicial; i++)
        {
            Projetil projetil = pool.Get();
            pool.Release(projetil);
        }
    }

    private Projetil CriarProjetil()
    {
        Projetil origem;

        if (criados < quantidadeInicial)
        {
            origem = prefab;
        }
        else
        {
            origem = data.projetil1 != null
                ? data.projetil1
                : prefab;
        }

        criados++;

        Projetil projetil = Instantiate(origem, transform);
        projetil.gameObject.SetActive(false);

        return projetil;
    }

    private void DevolverParaPool(Projetil projetil)
    {
        projetil.gameObject.SetActive(false);
    }

    private void DestruirProjetil(Projetil projetil)
    {
        Destroy(projetil.gameObject);
    }

    public Projetil PegarProjetil()
    {
        Projetil projetil = pool.Get();

        if (gameObject.name != "Pool")
        {
            switch (countProjetil)
            {
                default:
                case 0:
                    countProjetil++;
                    projetil.projetilIndex = 0;

                    anim.SetBool("Ataque1", true);
                    anim.SetBool("Ataque2", false);
                    anim.SetBool("Ataque3", false);
                    break;

                case 1:
                    countProjetil++;
                    projetil.projetilIndex = 1;

                    anim.SetBool("Ataque1", false);
                    anim.SetBool("Ataque2", true);
                    anim.SetBool("Ataque3", false);
                    break;

                case 2:
                    countProjetil = 0;
                    projetil.projetilIndex = 2;

                    anim.SetBool("Ataque1", false);
                    anim.SetBool("Ataque2", false);
                    anim.SetBool("Ataque3", true);
                    break;
            }
        }

        projetil.cor = data.cor;
        projetil.dano = data.dano;
        projetil.velocidade = data.velocidade;

        return projetil;
    }

    public void DevolverProjetil(Projetil projetil)
    {
        if (projetil != null)
        {
            pool.Release(projetil);
        }
    }

    private void UpdateData(int index)
    {
        if (gameObject.name == "Pool")
        {
            data = dataArray[0];
        }
        else
        {
            data = dataArray[index];
        }
    }
}
