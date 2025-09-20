using UnityEngine;

public class PlayerCollision : MonoBehaviour
{

    public GameObject player;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("enemie"))
        {
            player.GetComponent<PlayerHealth>().TakeDamage();
            Debug.Log("Daño al player");
        }
    }

}
