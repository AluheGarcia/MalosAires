
using UnityEngine;

public class RangeAttack : MonoBehaviour
{

    //[SerializeField] private GameObject Bullet;
    //[SerializeField] private GameObject BulletDirection;
    [SerializeField] private GameObject Gun;
    [SerializeField] private GameObject model;
    [SerializeField] private GameObject player;     
    [SerializeField] private InventoryBehaviour inventory;

    private GunScript gunScript;
    private bool aiming;

    //private float fireRate = 0.7f;
    //private float nextFireTime = 0f;

    private void Update()
    {

        //if (Input.GetButton("Fire1") && Time.time >= nextFireTime && aiming == true)
        //{

        //    nextFireTime = Time.time + fireRate;

        //    Instantiate(Bullet, BulletDirection.transform.position, BulletDirection.transform.rotation);

        //    model.GetComponent<PlayerAnimController>().Shooting();

        //}
        if (inventory == null) return;

        UpdateGunReference();
        HandleAiming();
        HandleShooting();



        //aiming = Input.GetMouseButton(1);

        //if (aiming == true)
        //{
        //    model.GetComponent<PlayerAnimController>().Aiming();
        //    player.GetComponent<PlayerMovement>().Aiming();
        //}
        //else
        //{
        //    model.GetComponent<PlayerAnimController>().StopAiming();
        //    player.GetComponent<PlayerMovement>().NotAiming();
        //}

    }

    private void UpdateGunReference()
    {
        // Verifica si hay un arma equipada
        if (inventory.Equippeditem != null)
        {
            // Busca el GunScript en el item equipado
            gunScript = inventory.Equippeditem.GetComponent<GunScript>();
        }
        else
        {
            gunScript = null;
        }
    }

    private void HandleAiming()
    {
        aiming = Input.GetMouseButton(1);

        if (aiming)
        {
            model.GetComponent<PlayerAnimController>()?.Aiming();
            player.GetComponent<PlayerMovement>()?.Aiming();
        }
        else
        {
            model.GetComponent<PlayerAnimController>()?.StopAiming();
            player.GetComponent<PlayerMovement>()?.NotAiming();
        }
    }

    private void HandleShooting()
    {
        if (gunScript == null) return;
        if (aiming && Input.GetButton("Fire1") && gunScript.CanShoot())
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

}
