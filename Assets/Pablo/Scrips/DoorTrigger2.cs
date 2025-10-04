using UnityEngine;

public class DoorTrigger2 : MonoBehaviour
{

    public GameObject door2;

    private void OnTriggerEnter(Collider colider)
    {

        if (colider.gameObject.CompareTag("Player"))
        {

            Destroy(door2);

            Debug.Log("Puerta2 abierta");
        }


    }


}
