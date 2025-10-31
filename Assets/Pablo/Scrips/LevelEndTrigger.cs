using UnityEngine;

public class LevelEndTrigger : MonoBehaviour
{

    public GameObject objectiveObject1;
    public GameObject objectiveObject2;

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


    }


}
