
using UnityEngine;
using UnityEngine.Pool;

public class ProjectilePool : MonoBehaviour
{
    [SerializeField] private Projetil prefab;
    [SerializeField] private int quantidadeInicial = 20;

    private int countProjetil = 0;
    private int criados = 0;

    [SerializeField] private States[] dataArray;
    private States data;

    private ObjectPool<Projetil> pool;


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

        projetil.projetilIndex = countProjetil;
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

    public void UpdateData(int state, int projetil)
    {
        data = dataArray[state];
        countProjetil = projetil;
                
    }
}
