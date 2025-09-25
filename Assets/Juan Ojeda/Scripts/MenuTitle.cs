using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuTitle : MonoBehaviour
{
    public void GameStart()
    {
        SceneManager.LoadScene("Escena Inventario");
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
