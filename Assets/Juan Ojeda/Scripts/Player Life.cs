using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerLife : MonoBehaviour
{
    [SerializeField] private int QHitsSuported = 3;
    public int hitsSuported => QHitsSuported;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //if (QHitsSuported < 1)
        //{
        //    Destroy(gameObject);
        //}
    }
    //private void OnCollisionEnter(Collision other)
    //{
    //    if (other.gameObject.CompareTag("Enemy"))
    //    {
    //        TakeHit();
    //    }
    //}

    public void TakeHit ()
    {
        QHitsSuported--;
    }

    public void ResetHits ()
    {
        
            QHitsSuported = 3;
                
    }

    
}
