using UnityEngine;

public class EnemyMovement : MonoBehaviour
{

    [SerializeField] private float _MoveSpeed = 2f;
    [SerializeField] private Rigidbody Player;

    private float _DistanceToPlayer;
    private float _ChaseDistance = 20f;
    private float _Proximity = 2f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        _DistanceToPlayer = Vector3.Distance(this.transform.position, Player.position);

        if (_DistanceToPlayer <= _ChaseDistance && _DistanceToPlayer > _Proximity)
        {
            transform.position = Vector3.MoveTowards(this.transform.position, Player.position, _MoveSpeed * Time.deltaTime);
        }
    }
}
