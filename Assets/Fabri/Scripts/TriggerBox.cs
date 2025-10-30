using UnityEngine;

public class TriggerZone : MonoBehaviour
{
    public string triggerKey;
    private bool hasTriggered = false;


    void OnTriggerEnter(Collider other)
    {
        if (!hasTriggered && other.CompareTag("Player"))
        {
            hasTriggered = true;
            TextTriggerManager manager = FindObjectOfType<TextTriggerManager>();
            if (manager != null)
            {
                manager.ShowMessage(triggerKey);
            }
        }

    }
}
