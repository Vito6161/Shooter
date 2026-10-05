using System.Collections.Generic;
using UnityEngine;

public class BossPool : MonoBehaviour
{
    [SerializeField] private Projetil prefab;
    [SerializeField] private int quantidadeInicial = 20;

    private Queue<Projetil> pool = new Queue<Projetil>();

    private void Awake()
    {
        for (int i = 0; i < quantidadeInicial; i++)
        {
            Projetil projetil = Instantiate(prefab, transform);

            projetil.gameObject.SetActive(false);

            pool.Enqueue(projetil);
        }
    }

    public Projetil PegarProjetil()
    {
        Projetil projetil;

        if (pool.Count > 0)
        {
            projetil = pool.Dequeue();
        }
        else
        {
            projetil = Instantiate(prefab, transform);
        }

        return projetil;
    }

    public void DevolverProjetil(Projetil projetil)
    {
        projetil.gameObject.SetActive(false);

        pool.Enqueue(projetil);
    }
}