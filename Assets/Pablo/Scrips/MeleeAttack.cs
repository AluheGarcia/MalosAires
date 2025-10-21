using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class MeleeAttack : MonoBehaviour
{

    [SerializeField] private GameObject Attack;

    [SerializeField] private GameObject AttackDirection;


    private bool ataque;

    private void Update()
    {
        ataque = Input.GetButtonDown("Fire1");

        if (ataque == true)
        {

            Instantiate(Attack, AttackDirection.transform);

        }



    }


}
