using System.Collections.Generic;
using UnityEngine;

public class ProjectilePool : MonoBehaviour
{
    [SerializeField] private Projetil prefab;
    [SerializeField] private int quantidadeInicial = 20;
    private int countProjetil = 0;

    [SerializeField] private States[] dataArray;
    private States data;

    private Queue<Projetil> pool = new Queue<Projetil>();


    private void OnEnable()
    {
        StateOM.StateChanged += UpdateData;
    }

    private void OnDisable()
    {
        StateOM.StateChanged -= UpdateData;
    }

    void Start()
    {
        data = dataArray[0];
    }

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
            projetil = Instantiate(data.projetil1, transform);
        }

        if(gameObject.name != "Pool")
        {
            switch(countProjetil)
            {
                default:
                case 0:
                    countProjetil++;
                    projetil.projetilIndex = 0;
                    break;

                case 1:
                    countProjetil++;
                    projetil.projetilIndex = 1;
                    break;

                case 2:
                    countProjetil = 0;
                    projetil.projetilIndex = 2;
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
        projetil.gameObject.SetActive(false);

        pool.Enqueue(projetil);
    }

    private void UpdateData(int index)
    {
        if(gameObject.name == "Pool")
        {
            data = dataArray[0];
        }
        else
        {
            data = dataArray[index];
        }
    }
}