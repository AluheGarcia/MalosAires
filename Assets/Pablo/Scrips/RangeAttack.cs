using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class RangeAttack : MonoBehaviour
{

    [SerializeField] private GameObject Bullet;

    [SerializeField] private GameObject BulletDirection;



    private bool shot;

    private void Update()
    {
        shot = Input.GetButtonDown("Fire1");

        if (shot == true)
        {

            Instantiate(Bullet, BulletDirection.transform.position, BulletDirection.transform.rotation);

        }



    }

}
