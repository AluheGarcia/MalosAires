using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuTitle : MonoBehaviour
{
    public void GameStart()
    {
        SceneManager.LoadScene("Level1");
        Time.timeScale = 1f;
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
