using UnityEngine;

public class EnemieHealth : MonoBehaviour
{

    private int enemieHealth = 3;


    public void TakeMeleeDamage()
    {

        enemieHealth--;

    }

    public void TakeRangeDamage()
    {

        enemieHealth -= 3;

    }

    private void Update()
    {
        if (enemieHealth ==0)
        {
            Destroy(gameObject);
        }
    }

}
