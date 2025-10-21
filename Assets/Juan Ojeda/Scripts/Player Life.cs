using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerLife : MonoBehaviour
{
    [SerializeField] private int QHitsSuported = 1;
    public int hitsSuported => QHitsSuported;    
    

    public void TakeHit ()
    {
        QHitsSuported--;

    }

    public void ResetHits ()
    {
        
        QHitsSuported = 1;
                
    }

    
}
