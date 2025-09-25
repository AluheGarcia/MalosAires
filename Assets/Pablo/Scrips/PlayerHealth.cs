using UnityEngine;

public class PlayerHealth : MonoBehaviour
{

    [SerializeField] int health = 3;
    public int phealth => health;



    public void TakeDamage()
    {

        health--;

        Debug.Log(health);
    }



    private void Update()
    {
        if (health == 0)
        {
            Destroy(gameObject);
        }
    }

}
