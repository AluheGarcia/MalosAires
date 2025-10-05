using UnityEngine;

public class PlayerCollision : MonoBehaviour
{

    public GameObject player;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            player.GetComponent<PlayerLife>().TakeHit();
            Debug.Log("Da�o al player");
        }
    }

}
