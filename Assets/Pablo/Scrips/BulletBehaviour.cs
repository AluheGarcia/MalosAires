using UnityEngine;

public class BulletBehaviour : MonoBehaviour
{

    private int speed = 40;

    void Start()
    {
        Destroy(gameObject, 2);
    }


    void Update()
    {

        transform.Translate(0, 0, speed * Time.deltaTime * 2);

    }

}
