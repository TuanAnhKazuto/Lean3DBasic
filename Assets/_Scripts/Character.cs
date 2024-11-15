using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static EnemyAI;

public class Character : MonoBehaviour
{
    public CharacterController characterController; 
    public float speed = 2f;
    public Vector3 movementVelocty;
    public PlayerInput playerInput;
    public Animator animator;

    public GameObject swordCollider;

    // Hp
    public float curHp;
    private float maxHp = 100f;
    public bool onDead = false;

    //weapon
    public GameObject sword01;
    public GameObject sword02;  

    // state machine
    [HideInInspector] public enum CharacterState
    {Normal, Attack, Die}

    public CharacterState curState; // trạng thái hiện tại

    private void Start()
    {
        curHp = maxHp;
        
    }

    void FixedUpdate()
    {
        switch (curState)
        {
            case CharacterState.Normal:
                CaculateMovement();
                break;
            case CharacterState.Attack:
                break;
        }
        
        characterController.Move(movementVelocty);

        OnDead();
    }

    void CaculateMovement()
    {
        if (playerInput.attackInput)
        {
            ChangeState(CharacterState.Attack);
            swordCollider.SetActive(true);
            return;
        }

        movementVelocty.Set(playerInput.horizontalInput, 0, playerInput.verticalInput);
        movementVelocty.Normalize();
        movementVelocty = Quaternion.Euler(0, -45, 0) * movementVelocty;
        movementVelocty *= speed * Time.deltaTime;
        animator.SetFloat("Speed", movementVelocty.magnitude);
        if (movementVelocty != Vector3.zero )
        {
            transform.rotation = Quaternion.LookRotation(movementVelocty);
        }
    }

    // chuyển đổi trạng thái
    private void ChangeState(CharacterState newState)
    {
        // xóa cache
        playerInput.attackInput = false;

        // B1: exit curState
        switch (curState)
        {
            case CharacterState.Normal:
                break;
            case CharacterState.Attack:
                break;
            case CharacterState.Die:
                break;
        }

        // B2: enter newState
        switch (newState)
        {
            case CharacterState.Normal:
                break;
            case CharacterState.Attack:
                animator.SetTrigger("Attack");
                break;
            case CharacterState.Die:
                onDead = true;
                speed = 0;
                // Weapon drop
                sword01.transform.SetParent(null);
                sword02.transform.SetParent(null);

                // Weapon fell

                sword01.GetComponent<Rigidbody>().isKinematic = false;
                sword02.GetComponent<Rigidbody>().isKinematic = false;

                animator.SetBool("Die",true);
                break;
        }

        // B3: Update state
        curState = newState;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "EnemyAtk")
        {
            curHp -= 10f;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        
    }

    public void OnDead()
    {
        if (curHp <= 0)
        {
            curHp = 0;
            ChangeState(CharacterState.Die);
        }
    }

    public void OnAttack01End()
    {
        ChangeState(CharacterState.Normal);
        swordCollider.SetActive(false);
    }
}
