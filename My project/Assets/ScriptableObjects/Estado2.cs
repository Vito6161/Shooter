using UnityEngine;

[CreateAssetMenu(fileName = "Estado1", menuName = "Scriptable Objects/Estado1")]
public class Estado2 : ScriptableObject
{
    [SerializeField] private float intervalo;
    [SerializeField] private float velocidade;
    [SerializeField] private int dano;
    [SerializeField] private GameObject projetil1, projetil2, projetil3;
}
