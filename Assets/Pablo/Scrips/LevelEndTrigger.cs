using UnityEngine;

public class LevelEndTrigger : MonoBehaviour
{

    public GameObject levelEnd;

    private void OnTriggerEnter(Collider colider)
    {

        if (colider.gameObject.CompareTag("Player"))
        {

            levelEnd.SetActive(true);

            Debug.Log("Final del nivel abierto");
        }


    }


}
