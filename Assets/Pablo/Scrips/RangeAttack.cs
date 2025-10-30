
using UnityEngine;

public class RangeAttack : MonoBehaviour
{

    //[SerializeField] private GameObject Bullet;
    //[SerializeField] private GameObject BulletDirection;
    [SerializeField] private GameObject Gun;
    [SerializeField] private GameObject model;
    [SerializeField] private GameObject player;
    [SerializeField] private GunScript gunScript;

    private bool aiming;

    //private float fireRate = 0.7f;
    private float nextFireTime = 0f;

    private void Update()
    {

       

        if (gunScript == null)
        {
            
            gunScript = GetComponentInChildren<GunScript>(true);
            
            if (gunScript == null)
                return; 
        }
        if (gunScript != null && gunScript.inv == null) 
        {
            var inv = player.GetComponent<InventoryBehaviour>();
            if (inv != null)
            {
                gunScript.SetInventory(inv);
            }
            
        }

        HandleAiming();
        HandleShooting();
        HandleReload();

    }

    private void HandleAiming()
    {
        aiming = Input.GetMouseButton(1);

        var anim = model.GetComponent<PlayerAnimController>();
        var movement = player.GetComponent<PlayerMovement>();

        if (aiming)
        {
            anim?.Aiming();
            movement?.Aiming();
        }
        else
        {
            anim?.StopAiming();
            movement?.NotAiming();
        }
    }

    private void HandleShooting()
    {
        if (!aiming) return;
        if (Input.GetButton("Fire1") && gunScript.CanShoot())
        {
            gunScript.ConsumeBullet();

            Transform muzzle = gunScript.GetMuzzle();
            GameObject bulletPrefab = gunScript.GetBulletPrefab();

            if (muzzle != null && bulletPrefab != null)
            {
                Instantiate(bulletPrefab, muzzle.position, muzzle.rotation);
            }

            model.GetComponent<PlayerAnimController>()?.Shooting();
        }
    }

    private void HandleReload()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            gunScript.Reload();
        }
    }


}
