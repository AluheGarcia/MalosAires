using UnityEngine;

public class ZombieExplosion : HealthBarManagerZombieBase
{
    public ParticleSystem particles;
    void Start()
    {
        particles.Stop();
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            particles.Play();
            Debug.Log("Manual: listo");
        }
    }
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
