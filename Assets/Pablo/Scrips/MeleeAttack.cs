using Unity.VisualScripting;
using UnityEditor.ShaderKeywordFilter;
using UnityEngine;

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
