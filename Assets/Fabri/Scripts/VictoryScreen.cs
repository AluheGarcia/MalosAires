using UnityEngine;

public class VictoryScreen : MonoBehaviour
{

    [SerializeField] private GameObject ScreenVictory;

    private void OnTriggerEnter(Collider colider)
    {

        if (colider.gameObject.CompareTag("Player"))
        {
            ScreenVictory.SetActive(true);
            Time.timeScale = 0;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    
    }   
}
