using UnityEditor;
using UnityEngine;

public class WeaponSwitch : MonoBehaviour
{

    private int weapon;

    void Update()
    {

        if (Input.GetKey(KeyCode.E))
        {
            weapon++;
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
