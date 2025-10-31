using JetBrains.Annotations;
using UnityEngine;

public class GunScript : Item
{
    [SerializeField] private int MaxBulletCapacity = 6;    
    [SerializeField] private int BulletinMagazine = 0;
    [SerializeField] private int totalAmmo = 0;
    
    [SerializeField] private GameObject BulletPrefab;
    public GameObject bulletPrefab => BulletPrefab;
    [SerializeField] private Transform BulletDirection;

    private float FireRate = 0.5f;
    private float NextFireRate = 0f;

    

    private void Start()
    {
        inventory = GetComponentInParent<InventoryBehaviour>();
        BulletinMagazine = 0;
        Transform boca = transform.Find("Boca");

        if (boca != null)
        {
            BulletDirection = boca.transform;
        }
       
    }

  
    public bool CanShoot()
    {
        return BulletinMagazine > 0 && Time.time >= NextFireRate;
    }

    public void ConsumeBullet()
    {
        BulletinMagazine--;
        NextFireRate = Time.time + FireRate;
    }

    public Transform GetMuzzle() => BulletDirection;
    public GameObject GetBulletPrefab() => BulletPrefab;


    public void Reload()
    {
        if (inventory == null)
        {
            
            return;
        }

        PlayerAmmo playerAmmo = inventory.GetComponent<PlayerAmmo>();
        if (playerAmmo == null)
        {
            
            return;
        }
          
        
      int bulletsNeeded = MaxBulletCapacity - BulletinMagazine;
       

        if (bulletsNeeded > 0 && playerAmmo.TryConsumeAmmo(bulletsNeeded))
        {
            BulletinMagazine = MaxBulletCapacity;
            
        }       
       

    }
     public int GetCurrentBullets() => BulletinMagazine;
        public int GetMaxMagazine() => MaxBulletCapacity;     
       

}
