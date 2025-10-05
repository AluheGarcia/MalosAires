using UnityEngine;

public class DoorTrigger1 : MonoBehaviour
{

    public GameObject door;

    private void OnTriggerEnter(Collider colider)
    {

        if (colider.gameObject.CompareTag("Player"))
        {

            Destroy(door);

            Debug.Log("Puerta1 abierta");
        }


    }


}
