using UnityEngine;

public class ZombieExplosion : HealthManagerZombieBase
{
    public ParticleSystem particles;
    protected private bool zombieExplosion = false;
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
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("tocado");
            particles.Play();
            Explosion();
        }
    }
    public void Explosion()
    {
        zombieExplosion = true;
        Destroy(gameObject,0.3f);   
             
    }

}
