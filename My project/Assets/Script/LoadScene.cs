using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{
    [SerializeField] private GameObject winInterface, loseInterface;
    private UIManager uiManager => GameObject.Find("UIManager").GetComponent<UIManager>();
    void Start()
    {
        if(uiManager.ganhou)
        {
            winInterface.SetActive(true);
            loseInterface.SetActive(false);
        }
        else
        {
            loseInterface.SetActive(true);
            winInterface.SetActive(false);
        }

        Destroy(uiManager.gameObject);
    }
    public void LoadGame()
    {
        SceneManager.LoadScene(0);
    }
}
