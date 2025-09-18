using UnityEngine;
using UnityEngine.XR;

public class PlayerMovement : MonoBehaviour
{

    [SerializeField] float _speed = 5f;
    [SerializeField] float _rotationSpeed = 180f;

    Rigidbody rb;
    float _moveH, _moveV;
    Vector3 _movement;
    Vector3 _moveDirection;
    Vector3 _moveSideways;
    float _rotationAmount;
    Quaternion _turnOffset;



        private void Start()
    {
        rb = GetComponent<Rigidbody>();

        rb.freezeRotation = true;
    }

    void FixedUpdate()
    {
        _moveH = Input.GetAxis("Horizontal");
        _moveV = Input.GetAxis("Vertical");


        _moveDirection = transform.forward * _moveV * _speed * Time.deltaTime;

        if (Input.GetKey(KeyCode.LeftShift))
        {
            _moveSideways = transform.right * _moveH * _speed * Time.deltaTime;
        }
        else
        {
            _moveSideways = Vector3.zero;

            _rotationAmount = _moveH * _rotationSpeed * Time.deltaTime;
            _turnOffset = Quaternion.Euler(0, _rotationAmount, 0);
            rb.MoveRotation(rb.rotation * _turnOffset);

        }

        _movement = rb.position + _moveDirection + _moveSideways;
        rb.MovePosition(_movement);

    }
}
