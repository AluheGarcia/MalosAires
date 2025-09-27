using UnityEngine;
using UnityEngine.SceneManagement;
public class LoadLevel3 : MonoBehaviour
{

    private void OnTriggerEnter(Collider colider)
    {

        if (colider.gameObject.CompareTag("Player"))
        {

            SceneManager.LoadScene("Level3");

        }


    }


}
