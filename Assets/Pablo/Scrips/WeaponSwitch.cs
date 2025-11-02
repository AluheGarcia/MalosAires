using UnityEditor;
using UnityEngine;

public class WeaponSwitch : MonoBehaviour
{

    private int weapon;
    [SerializeField] private GameObject model;
    [SerializeField] private GameObject revolver;
    [SerializeField] private GameObject knife;

    private MeleeAttack meleeAttack;
    private RangeAttack rangeAttack;

    void Start()
    {
        
        meleeAttack = GetComponent<MeleeAttack>();
        rangeAttack = GetComponent<RangeAttack>();
        
    }


    void Update()
    {
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        if (Input.GetKeyUp(KeyCode.C) || scroll != 0)
        {
            weapon++;
            model.GetComponent<PlayerAnimController>().Switch();

        }

        if (weapon % 2 == 0)
        {

            GetComponent<MeleeAttack>().enabled = true;
            GetComponent<RangeAttack>().enabled = false;
            revolver.SetActive(false);
            knife.SetActive(true);
        }
        else if (weapon % 2 == 1)
        {

            GetComponent<MeleeAttack>().enabled = false;
            GetComponent<RangeAttack>().enabled = true;
            revolver.SetActive(true);
            knife.SetActive(false);

        }


    }



}
