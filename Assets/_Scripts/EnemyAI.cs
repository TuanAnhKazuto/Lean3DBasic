using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Experimental.AI;
using static Character;
using static UnityEditor.Rendering.InspectorCurveEditor;

public class EnemyAI : MonoBehaviour
{
    public NavMeshAgent navMeshAgent;
    public Transform target;

    public float radius = 10f;
    public Vector3 originalPos;
    public float maxDistace = 50f;

    public Animator animator;

    // state machine
    public enum EnemyState
    { Normal, Attack }

    public EnemyState curState; // trạng thái hiện tại


    private void Start()
    {
        originalPos = transform.position;
    }

    private void Update()
    {
        // Khoảng cách từ vị trí hiện tại đến vị trí ban đầu
        var distanceToOriginal = Vector3.Distance(originalPos,transform.position);
        // Khoảng cách từ vị trí hiện tại đến vị trí mục tiêu

        var distance = Vector3.Distance(target.position, transform.position);
        if(distance <= radius && distanceToOriginal <= maxDistace)
        {
            // di chuyển đến mục tiêu
            navMeshAgent.SetDestination(target.position);
            animator.SetFloat("Speed", navMeshAgent.velocity.magnitude);

            distance = Vector3.Distance(target.position, transform.position);
            if(distance < 2f)
            {
                // Tấn công
                ChangeState(EnemyState.Attack);
            }
        }

        if(distance > radius || distanceToOriginal > maxDistace)
        {
            // Quay về vị trí ban đầu
            navMeshAgent.SetDestination(originalPos);

            // Chuyển sang trạng thái đứng yên
            distance = Vector3.Distance(originalPos, transform.position);
            if(distance < 1f)
            {
                animator.SetFloat("Speed", 0);
            }

            // Bình thường
            ChangeState(EnemyState.Normal);
        }
    }

    private void ChangeState(EnemyState newState)
    {
        // B1: exit curState
        switch (curState)
        {
            case EnemyState.Normal:
                break;
            case EnemyState.Attack:
                break;
        }

        // B2: enter newState
        switch (newState)
        {
            case EnemyState.Normal:
                break;
            case EnemyState.Attack:
                animator.SetTrigger("Attack");
                break;
        }

        // B3: Update state
        curState = newState;
    }
}
