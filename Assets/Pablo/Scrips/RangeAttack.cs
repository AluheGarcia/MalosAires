
using UnityEngine;

public class RangeAttack : MonoBehaviour
{

    [SerializeField] private GameObject Bullet;
    [SerializeField] private GameObject BulletDirection;
    [SerializeField] private GameObject Gun;
    [SerializeField] private GameObject model;
    [SerializeField] private GameObject player;     
    [SerializeField] private InventoryBehaviour inventory;

    private GunScript gunScript;
    private bool aiming;

    private float fireRate = 0.7f;
    private float nextFireTime = 0f;

    private void Update()
    {

        if (Input.GetButton("Fire1") && Time.time >= nextFireTime && aiming == true)
        {

            nextFireTime = Time.time + fireRate;

            Instantiate(Bullet, BulletDirection.transform.position, BulletDirection.transform.rotation);
            Gun.SetActive(true);
            model.GetComponent<PlayerAnimController>().Shooting();

        }
      
        aiming = Input.GetMouseButton(1);

        if (aiming == true)
        {
            model.GetComponent<PlayerAnimController>().Aiming();
            player.GetComponent<PlayerMovement>().Aiming();
        }
        else
        {
            model.GetComponent<PlayerAnimController>().StopAiming();
            player.GetComponent<PlayerMovement>().NotAiming();
        }

    }
   

}
