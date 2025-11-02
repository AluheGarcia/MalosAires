using UnityEngine;

public class WeaponSwitcher : MonoBehaviour
{
    
    [SerializeField] private GameObject Model;
    [SerializeField] private Transform WeaponHolder;
    [SerializeField] private InventoryBehaviour inventory;

    [SerializeField] private GameObject Revolver;
    [SerializeField] private GameObject Knife;

    private MeleeAttack meleeAttack;
    private RangeAttack rangeAttack;

    private int currentWeapon = 0; 

    void Start()
    {
        if (inventory == null)
        {
            inventory = GetComponent<InventoryBehaviour>();
            if (inventory == null)
            {
                inventory = GetComponentInParent<InventoryBehaviour>();
            }
        }

        if (inventory == null)
        {
            Debug.LogError(" InventoryBehaviour no asignado en WeaponSwitcher. Asignalo en el Inspector.");
        }

        meleeAttack = GetComponent<MeleeAttack>();
        rangeAttack = GetComponent<RangeAttack>();
        if (WeaponHolder == null)
        {
            Debug.LogError(" WeaponHolder no asignado en el Inspector.");
            return;
        }

        foreach (Transform child in WeaponHolder.GetComponentsInChildren<Transform>(true))
        {
            if (child.name == "Revolver")
                Revolver = child.gameObject;
            else if (child.name == "Knife")
                Knife = child.gameObject;
        }
        

        if (Revolver == null || Knife == null)
        {
            Debug.LogError(" No se encontró el objeto 'Revolver' o 'Knife' dentro de " + WeaponHolder.name);
            return;
        }

       

        UpdateWeaponState();
    }

    void Update()
    {
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        if (Input.GetKeyDown(KeyCode.C) || scroll != 0)
        {
            ToggleWeapon();
        }
    }

    private void ToggleWeapon()
    {
        
        currentWeapon = (currentWeapon + 1) % 2;

        Model.GetComponent<PlayerAnimController>()?.Switch();

        UpdateWeaponState();
    }

    private void UpdateWeaponState()
    {

        if (Knife == null || Revolver == null)
        {
            return;
        }

        bool hasKnife = inventory == null || inventory.ContainsItem(Knife);
        bool hasRevolver = inventory == null || inventory.ContainsItem(Revolver);


        if (currentWeapon == 0 && hasKnife)
        { 
          ActivateWeapon(Knife, Revolver, true);
        }
        else if (currentWeapon == 1 && hasRevolver)
        { 
          ActivateWeapon(Revolver, Knife, false);
        }
       
    }

    private void ActivateWeapon(GameObject ActiveWeapon, GameObject InactiveWeapon, bool isMelee)
    {
        meleeAttack.enabled = isMelee;
        rangeAttack.enabled = !isMelee;

        if (ActiveWeapon != null) ActiveWeapon.SetActive(true);
        if (InactiveWeapon !=null) InactiveWeapon.SetActive(false);
        
    }
}

