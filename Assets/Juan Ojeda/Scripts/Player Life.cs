using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerLife : MonoBehaviour
{
    [SerializeField] private int QHitsSuported = 1;
    public int hitsSuported => QHitsSuported;
   

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    

    public void TakeHit ()
    {
        QHitsSuported--;

    }

    public void ResetHits ()
    {
        
            QHitsSuported = 1;
                
    }

    
}
