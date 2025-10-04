using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLevelScript : MonoBehaviour
{
    [SerializeField] GameObject NextStage;
    [SerializeField] GameObject NextLevelScreen;


    public void Start()
    {
        NextLevelScreen.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
       

        if (other.CompareTag("Player"))
        {
            NextLevel();
            Time.timeScale = 0f;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            NextStage.SetActive(true);
           
        }
    }

    public void NextLevel()
    {
        NextLevelScreen.SetActive(true);
        SceneManager.LoadScene("SampleScene");
    }
}
