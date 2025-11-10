
using UnityEngine;
using TMPro;
using System.Collections;

public class RangeAttack : MonoBehaviour
{

    [SerializeField] private GameObject Bullet;  
    [SerializeField] private GameObject Gun;
    [SerializeField] private GameObject model;
    [SerializeField] private GameObject player;
    [SerializeField] private GunScript gunScript;
   
    [SerializeField] private TMP_Text ammoDisplay;

    private bool aiming;

    
    private float nextFireTime = 0f;

    private void OnEnable()
    {        
        if (ammoDisplay != null)
        {
            StartCoroutine(FadeInText(ammoDisplay, 0.5f));
        }
    }

    private void OnDisable()
    {
       
        if (ammoDisplay != null)
        {
            StartCoroutine(FadeOutText(ammoDisplay, 0.5f));
        }
    }

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
        UpdateAmmoUI();

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

    public void UpdateAmmoUI()
    {
        if (ammoDisplay == null || gunScript == null) return;

        int currentAmmo = gunScript.GetCurrentBullets();
        int maxAmmo = gunScript.GetMaxMagazine();  

        ammoDisplay.text = $"{currentAmmo} / {maxAmmo}";
    }

    private IEnumerator FadeInText(TMP_Text text, float duration)
    {
        float startAlpha = text.alpha;
        float time = 0f;

        while (time < duration)
        {
            time += Time.deltaTime;
            text.alpha = Mathf.Lerp(startAlpha, 1f, time / duration);
            yield return null;
        }
        text.alpha = 1f;
    }

    private IEnumerator FadeOutText(TMP_Text text, float duration)
    {
        float startAlpha = text.alpha;
        float time = 0f;

        while (time < duration)
        {
            time += Time.deltaTime;
            text.alpha = Mathf.Lerp(startAlpha, 0f, time / duration);
            yield return null;
        }
        text.alpha = 0f;
    }


}
