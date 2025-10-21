using UnityEngine;

public class GunScript : Item
{
    [SerializeField] private int MaxBulletCapacity = 6;    
    [SerializeField] private int BulletinMagazine = 0;
    [SerializeField] private int totalAmmo = 0;
    
    [SerializeField] private GameObject BulletPrefab;
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

    private void Update()
    {
        if (inventory == null || inventory.Equippeditem != gameObject)
        {
            return; 
        }

        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }
       

        if (Input.GetKeyDown(KeyCode.R))
        {
            Reload();
        }
        
    }

    public void Reload()
    {
       PlayerAmmo playerAmmo = inventory.GetComponent<PlayerAmmo>();
        if (playerAmmo == null)
        
          return;
        
      int bulletesNeeded = MaxBulletCapacity - BulletinMagazine;

        if (bulletesNeeded > 0 && playerAmmo.TryConsumeAmmo(bulletesNeeded))
        {
            BulletinMagazine = MaxBulletCapacity;
        }
        

    }

    
    public void Shoot()
    {
        if (BulletinMagazine <= 0)
        {
           return;
        }

        NextFireRate = Time.time + FireRate;

        if (BulletDirection == null)
        {                     
          return;
        }

        BulletinMagazine--;

        if (BulletPrefab != null && BulletDirection != null)
        {
            GameObject Bullet = Instantiate(BulletPrefab, BulletDirection.position, BulletDirection.rotation);
        }
       
    }

    

}
