using UnityEngine;
using System;
using System.Collections;

public class Vida : MonoBehaviour
{
    [SerializeField] public int vida;
    public int state;
    
    public void CausarDano(int dano)
    {
        vida -= dano;

        if(!gameObject.CompareTag("Player"))
        {
            switch(vida)
            {
                case >= 200: //estado 1 | PADRÃO
                    state = 0;
                    break;
                case >= 100 when vida < 200: // estado 2 | VIDA MÉDIA
                    state = 1;
                    break;

                case < 100 when vida > 0: //estado 3 | ULTIMO ESTÁGIO
                    state = 2;
                    break;
            }

            StateOM.OnStateChanged(state);
        }

        if(gameObject.CompareTag("Player")) Debug.Log($"VIDA {vida} | STATE: {state}");

        if(vida <= 0) Morte();
        
    }

    public void Morte()
    {
        vida = 0;
        bool temp;

        if(gameObject.CompareTag("Player")) // player morreu
        {
            temp = false;
        }
        else if (gameObject.CompareTag("Boss")) // boss morreu
        {
            temp = true;
        }
        else temp = false;

        StateOM.OnEndGame(temp);
    }

}

public class StateOM
{
    public static event Action<int> StateChanged;

    public static void OnStateChanged(int index)
    {
        StateChanged?.Invoke(index);
    }

    public static event Action<bool> EndGame;

    public static void OnEndGame(bool resultado)
    {
        EndGame?.Invoke(resultado);
    }
}
