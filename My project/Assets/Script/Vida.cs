using UnityEngine;

public class Vida : MonoBehaviour
{
    [SerializeField] private int vida;
    
    public void CausarDano(int dano)
    {
        vida -= dano;
        Debug.Log(vida);

        if(vida <= 0)
        {
            Morte();
        }
    }

    public void Morte()
    {
        vida = 0;
        return;
    }
}
