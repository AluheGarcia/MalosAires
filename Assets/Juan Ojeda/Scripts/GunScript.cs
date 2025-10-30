using JetBrains.Annotations;
using UnityEngine;

public class GunScript : Item
{
    [SerializeField] private int MaxBulletCapacity = 6;    
    [SerializeField] private int BulletinMagazine = 0;
    [SerializeField] private int totalAmmo = 0;
    
    [SerializeField] private GameObject BulletPrefab;
    public GameObject bulletPrefab => BulletPrefab;
    private Transform BulletDirection;

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
            Debug.LogWarning("Inventory is null in GunScript.");
            return;
        }

        PlayerAmmo playerAmmo = inventory.GetComponent<PlayerAmmo>();
        if (playerAmmo == null)
        {
            Debug.LogWarning("PlayerAmmo component not found on the player.");
            return;
        }
          
        
      int bulletsNeeded = MaxBulletCapacity - BulletinMagazine;
        Debug.Log($"Intentando recargar. Necesita {bulletsNeeded} balas. PlayerAmmo: { playerAmmo.TotalAmmo}");

        if (bulletsNeeded > 0 && playerAmmo.TryConsumeAmmo(bulletsNeeded))
        {
            BulletinMagazine = MaxBulletCapacity;
            Debug.Log("Recargado completamente.");
        }       
       

    }
     public int GetCurrentBullets() => BulletinMagazine;
        public int GetMaxMagazine() => MaxBulletCapacity;     
       

}
