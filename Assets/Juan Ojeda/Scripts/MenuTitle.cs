using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuTitle : MonoBehaviour
{
    public void GameStart()
    {
        SceneManager.LoadScene("SubwayLineB");
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
