using UnityEngine;

public class ParticleBlood : MonoBehaviour
{
    public ParticleSystem particles;
    public HealthManagerZombieBase enemyHealth;

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
}
