using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;
using System.Collections;

public class UIManager : MonoBehaviour
{
    [SerializeField] GameObject nave1, nave2, nave3;
    [SerializeField] Slider bossBar;
    
    private int vidaPlayer => GameObject.FindWithTag("Player").GetComponent<Vida>().vida;
    private int vidaBoss => GameObject.FindWithTag("Boss").GetComponent<Vida>().vida;


    public bool ganhou;

    public static UIManager instance;

    void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void OnEnable()
    {
        StateOM.EndGame += GameEnded;
    }
    void OnDisable()
    {
        StateOM.EndGame -= GameEnded;
    }
    void Update()
    {
        switch(vidaPlayer)
        {
            default:
            case 4:
                nave1.SetActive(true);
                nave2.SetActive(true);
                nave3.SetActive(true);
                break;
            case 3:
                nave1.SetActive(false);
                break;
            case 2:
                nave2.SetActive(false);
                break;
            
            case <= 1:
                nave3.SetActive(false);
                break;
        }


        bossBar.value = vidaBoss;

    }

    void GameEnded(bool resultado)
    {
        ganhou = resultado;
        SceneManager.LoadScene(1);
    }
}
