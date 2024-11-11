using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public CharacterController characterController; 
    public float speed = 2f;
    public Vector3 movementVelocty;
    public PlayerInput playerInput;
    public Animator animator;

    // state machine
    public enum CharacterState
    {Normal, Attack}

    public CharacterState curState; // trạng thái hiện tại

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
    }

    void CaculateMovement()
    {
        if (playerInput.attackInput)
        {
            ChangeState(CharacterState.Attack);
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
        }

        // B2: enter newState
        switch (newState)
        {
            case CharacterState.Normal:
                break;
            case CharacterState.Attack:
                animator.SetTrigger("Attack");
                break;
        }

        // B3: Update state
        curState = newState;
    }

    public void OnAttack01End()
    {
        ChangeState(CharacterState.Normal);
    }
}
