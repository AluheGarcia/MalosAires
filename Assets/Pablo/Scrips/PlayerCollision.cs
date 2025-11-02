using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    public GameObject player;

    private void OnCollisionEnter(Collision collision)
    {
        var life = player.GetComponent<PlayerLife>();

        if (collision.gameObject.CompareTag("Enemy"))
        {
            life.TakeHit(); 
            Debug.Log("Daño al player por Enemy");
        }

        if (collision.gameObject.CompareTag("EnemyExp"))
        {
            life.TakeHit(); 
            
            Debug.Log("Daño doble al player por EnemyExp");
        }
    }
}

