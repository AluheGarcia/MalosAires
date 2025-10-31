using UnityEngine;

public class DoorTrigger : MonoBehaviour
{

    public GameObject door;

    private void OnTriggerEnter(Collider colider)
    {

        if (colider.gameObject.CompareTag("Player"))
        {

            Destroy(door);

        }


    }


}
