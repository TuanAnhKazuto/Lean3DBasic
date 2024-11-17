using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static EnemyAI;

public class Character : MonoBehaviour
{
    public MyPlayerMovement playerMovement;
    public CharacterController characterController; 
    public float speed = 2f;
    public Vector3 movementVelocty;
    public PlayerInput playerInput;
    public Animator animator;

    public GameObject swordCollider;

    // Hp
    public int curHp;
    public int maxHp = 100;
    public bool onDead = false;
    public HealthBar healthBar;

    //weapon
    public BoxCollider boxSword;
    public BoxCollider boxShiend;
    public GameObject sword;
    public GameObject shiend;  

    // state machine
    [HideInInspector] public enum CharacterState
    {Normal, Attack, Die}

    public CharacterState curState; // trạng thái hiện tại

    private void Start()
    {
        curHp = maxHp;
        boxSword.enabled = false;
        boxShiend.enabled = false;
    }

    void FixedUpdate()
    {
        switch (curState)
        {
            case CharacterState.Normal:
                playerMovement.CaculateMovement();
                CaculateMovement();
                
                break;
            case CharacterState.Attack:
                break;
        }
        
        //characterController.Move(movementVelocty);

        if(curHp <= 0)
        {
            OnDead();
        }
    }

    void CaculateMovement()
    {
        if(onDead) return;
        if (playerInput.attackInput)
        {
            ChangeState(CharacterState.Attack);
            swordCollider.SetActive(true);
            return;
        }

        //movementVelocty.Set(playerInput.horizontalInput, 0, playerInput.verticalInput);
        //movementVelocty.Normalize();
        //movementVelocty = Quaternion.Euler(0, -45, 0) * movementVelocty;
        //movementVelocty *= speed * Time.deltaTime;
        animator.SetFloat("Speed", playerMovement.moveValue);
        //if (movementVelocty != Vector3.zero)
        //{
        //    transform.rotation = Quaternion.LookRotation(movementVelocty);
        //}
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
                animator.SetBool("Die", true);
                speed = 0;
                // Weapon drop
                sword.transform.SetParent(null);
                shiend.transform.SetParent(null);
                boxSword.enabled = true;
                boxShiend.enabled = true;

                // Weapon fell

                sword.GetComponent<Rigidbody>().isKinematic = false;
                shiend.GetComponent<Rigidbody>().isKinematic = false;
                break;
        }

        // B3: Update state
        curState = newState;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "EnemyAtk")
        {
            curHp -= 10;
            healthBar.UpdateBar(maxHp, curHp);
        }
    }

    public void OnDead()
    {
        curHp = 0;
        ChangeState(CharacterState.Die);
    }

    public void OnAttack01End()
    {
        ChangeState(CharacterState.Normal);
        swordCollider.SetActive(false);
    }
}
