using UnityEditor;
using UnityEngine;

public class WeaponSwitch : MonoBehaviour
{

    private int weapon;
    [SerializeField] private GameObject model;

    void Update()
    {

        if (Input.GetKeyUp(KeyCode.E))
        {
            weapon++;
            model.GetComponent<PlayerAnimController>().Switch();

        }

        if (weapon%2 == 0)
        {

            GetComponent<MeleeAttack>().enabled = true;
            GetComponent<RangeAttack>().enabled = false;

        }
        else if (weapon%2 == 1)
        {

            GetComponent<MeleeAttack>().enabled = false;
            GetComponent<RangeAttack>().enabled = true;

        }


    }



}
