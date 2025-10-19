using System;
using UnityEngine;

public class PlayerAnimController : MonoBehaviour
{

    [SerializeField] private Animator anim;
    [SerializeField] private GameObject player;


    private void Start()
    {
        anim = GetComponent<Animator>();
    }


    public void Walking()
    {
        anim.SetBool("isWalking", true);
        anim.SetBool("isRunning", false);
    }
    public void WalkingBack()
    {
        anim.SetBool("isWalkingBack", true);
    }
    public void Sprint()
    {
        anim.SetBool("isRunning", true);
    }
    public void WalkingRight()
    {
        anim.SetBool("Right", true);
    }
    public void WalkingLeft()
    {
        anim.SetBool("Left", true);
    }


    public void Still()
    {
        anim.SetBool("isWalking", false);
        anim.SetBool("isWalkingBack", false);
        anim.SetBool("isRunning", false);
        anim.SetBool("Right", false);
        anim.SetBool("Left", false);
    }


    public void Aiming()
    {
        anim.SetBool("Aiming", true);
    }
    public void StopAiming()
    {
        anim.SetBool("Aiming", false);
    }


    public void Shooting()
    {
        anim.SetBool("Shooting", true);
    }
    public void StopShooting()
    {
        anim.SetBool("Shooting", false);
    }


    public void Attack()
    {
        anim.SetBool("Attack", true);
        player.GetComponent<PlayerMovement>().Attacking();
    }
    public void StopAttack()
    {
        anim.SetBool("Attack", false);
        player.GetComponent<PlayerMovement>().NotAttacking();
    }


    public void Switch()
    {
        anim.SetBool("Switch", true);
        Invoke("SwitchFalse", 1f);
    }
    public void SwitchFalse()
    {
        anim.SetBool("Switch", false);
    }
}
