using UnityEngine;

public class ZombieExplosion : HealthBarManagerZombieBase
{
    private void OnTriggerEnter(Collider colider)
    {

        if (colider.gameObject.CompareTag("Player"))
        {
            Explosion();
        }
    }
    public void Explosion()
    {   
            Destroy(gameObject);       
    }

}
