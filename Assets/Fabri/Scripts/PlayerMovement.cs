using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
   
    public float moveSpeed;

    public float groundDrag;
    
    public float playerHeight;
    public LayerMask whatIsGround;
    bool grounded;

    public Transform orientation;


    float horizontalInput;
    float verticalInput;
    

    public float maxSlopeAngle;
    private RaycastHit slopeHit;


    [SerializeField] private GameObject model;
    private bool sprint;
    private bool aiming;
    private bool attacking;
    private bool walking;
    private bool sidewalk;
    private bool switching;

    Vector3 moveDirection;

    Rigidbody rb;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;

    }

    private void Update()
    {
        grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.2f, whatIsGround);

        MyInput();
        SpeedControl();

        if (grounded)
            rb.linearDamping = groundDrag;
        else
            rb.linearDamping = 0;



        //FUNCIONES DE ANIMACION

        if (verticalInput > 0)
        {
            model.GetComponent<PlayerAnimController>().Walking();
            walking = true;
        }
        else
        {
            model.GetComponent<PlayerAnimController>().Still();
            walking = false;
        }

        if (verticalInput < 0)
        {
            model.GetComponent<PlayerAnimController>().WalkingBack();
        }
        
        if (horizontalInput > 0)
        {
            model.GetComponent<PlayerAnimController>().WalkingRight();
            sidewalk = true;
        }

        if (horizontalInput < 0)
        {
            model.GetComponent<PlayerAnimController>().WalkingLeft();
            sidewalk = true;
        }



        if (verticalInput == 0 & horizontalInput == 0)
        {
            model.GetComponent<PlayerAnimController>().Still();
            walking = false;
            sidewalk = false;
        }


        if (attacking == false & aiming == false & switching == false)
        {
            if (sprint == true & walking == true)
            {
                moveSpeed = 4;
                model.GetComponent<PlayerAnimController>().Sprint();
            }
            if (sidewalk == true & sprint == false)
            {
                moveSpeed = 1;
            }

            if (sidewalk == false & sprint == false)
 
            {
                NormalSpeed();
            }
        }
        else
        {
            moveSpeed = 0;
        }


    }



    public void Aiming()
    {
        aiming = true;
    }
    public void NotAiming()
    {
        aiming = false;
    }
    public void Attacking()
    {
        attacking = true;
    }
    public void NotAttacking()
    {
        attacking = false;
    }
    public void Switching()
    {
        switching = true;
    }
    public void NotSwitching()
    {
        switching = false;
    }

    public void NormalSpeed()
    {
        moveSpeed = 1.5f;
    }



    private void FixedUpdate()
    {
        MovePlayer();
    }

    private void MyInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        sprint = Input.GetKey(KeyCode.LeftShift);
    }

    private void MovePlayer()
    {
        moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;

        //Para saber si est� subiendo o bajando una escalera

        if (OnSlope())
        {
            rb.AddForce(GetSlopeMoveDirection() * moveSpeed * 20f, ForceMode.Force);

            if (rb.linearVelocity.y > 0)
                rb.AddForce(Vector3.down * 80f, ForceMode.Force);
        }

        //Caminar normal :)
        else
            {
            rb.AddForce(moveDirection.normalized * moveSpeed * 10f, ForceMode.Force);
            }
        //Para no salir volando bajando una escalera
        rb.useGravity = !OnSlope();
    }

    private void SpeedControl()
    {
        //limita la velocidad en pendientes
        if (OnSlope())
        {
            if (rb.linearVelocity.magnitude > moveSpeed)
                rb.linearVelocity = rb.linearVelocity.normalized * moveSpeed;
        }

        Vector3 flatVelocity = new Vector3(rb.linearVelocity.x, 0f, rb.linearVelocity.z);

        //Limita la velocidad si se aplican varias fuerzas

        if (flatVelocity.magnitude > moveSpeed)
        {
            Vector3 limitedVelocity = flatVelocity.normalized * moveSpeed;
            rb.linearVelocity = new Vector3(limitedVelocity.x, rb.linearVelocity.y, limitedVelocity.z);
        }
    }

    private bool OnSlope()
    {
        if (Physics.Raycast(transform.position, Vector3.down, out slopeHit, playerHeight * 0.5f + 0.3f))
        {
            float angle = Vector3.Angle(Vector3.up, slopeHit.normal);
            return angle < maxSlopeAngle && angle != 0;
        }

        return false;
    }

    private Vector3 GetSlopeMoveDirection()
    {
        return Vector3.ProjectOnPlane(moveDirection, slopeHit.normal).normalized;
    }

}
