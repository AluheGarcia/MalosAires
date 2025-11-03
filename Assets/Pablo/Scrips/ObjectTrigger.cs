using UnityEngine;

public class ObjectTrigger : MonoBehaviour
{

    public GameObject objectiveObject1;
    public GameObject objectiveObject2;
    public GameObject objectiveObject3;


    private void OnTriggerEnter(Collider colider)
    {
        if (objectiveObject1 != null)
        {
            if (colider.gameObject.CompareTag("Player"))
            {
                objectiveObject1.SetActive(true);
                Destruirse();
            }
        }

        if (objectiveObject2 != null)
        {
            if (colider.gameObject.CompareTag("Player"))
            {
                objectiveObject2.SetActive(false);
                Destruirse();
            }
        }

        if (objectiveObject3 != null)
        {
            if (colider.gameObject.CompareTag("Player"))
            {
                objectiveObject3.SetActive(false);
                Destruirse();
            }
        }

    }

    void Destruirse()
    {
        Destroy(gameObject);
    }

}
