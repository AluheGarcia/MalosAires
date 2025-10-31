using UnityEngine;

public class ObjectTrigger : MonoBehaviour
{

    public GameObject objectiveObject1;
    public GameObject objectiveObject2;
    public GameObject objectiveObject3;
    public GameObject objectiveObject4;

    private void OnTriggerEnter(Collider colider)
    {
        if (objectiveObject1 != null)
        {
            if (colider.gameObject.CompareTag("Player"))
            {
                objectiveObject1.SetActive(true);
            }
        }

        if (objectiveObject2 != null)
        {
            if (colider.gameObject.CompareTag("Player"))
            {
                objectiveObject2.SetActive(false);
            }
        }

        if (objectiveObject3 != null)
        {
            if (colider.gameObject.CompareTag("Player"))
            {
                objectiveObject3.SetActive(true);
            }
        }

        if (objectiveObject4 != null)
        {
            if (colider.gameObject.CompareTag("Player"))
            {
                objectiveObject4.SetActive(false);
            }
        }
    }


}
