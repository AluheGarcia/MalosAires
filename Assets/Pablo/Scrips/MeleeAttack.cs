using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class MeleeAttack : MonoBehaviour
{

    [SerializeField] private GameObject Attack;
    [SerializeField] private GameObject AttackDirection;
    [SerializeField] private GameObject model;



    private float fireRate = 0.7f;
    private float nextFireTime = 0f;

    private void Update()
    {

        if (Input.GetButton("Fire1") && Time.time >= nextFireTime)
        {

            nextFireTime = Time.time + fireRate;

            Instantiate(Attack, AttackDirection.transform);

            model.GetComponent<PlayerAnimController>().Attack();

        }



    }


}
