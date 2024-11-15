using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Experimental.AI;
using static Character;
using static UnityEditor.Rendering.InspectorCurveEditor;

public class EnemyAI : MonoBehaviour
{
    public Character character;
    public NavMeshAgent navMeshAgent;
    public Transform target;

    public float radius = 10f;
    public Vector3 originalPos;
    public float maxDistace = 50f;

    public Animator animator;

    public float curHp;
    private float maxHp = 100f;

    public GameObject atkCollider;

    // state machine
    public enum EnemyState
    { Normal, Attack, Die }

    public EnemyState curState; // trạng thái hiện tại


    private void Start()
    {
        originalPos = transform.position;
        curHp = maxHp;
        atkCollider.SetActive(false);
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
            if(distance <= 2f && !character.onDead)
            {
                // Tấn công
                ChangeState(EnemyState.Attack);
            }
        }

        if(distance > radius || distanceToOriginal > maxDistace || character.onDead)
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

        OnDead();
    }

    public void StartAttack()
    {
        atkCollider.SetActive(true);
    }

    public void EndAttack()
    {
        atkCollider.SetActive(false);
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
            case EnemyState.Die:
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
            case EnemyState.Die:
                animator.SetTrigger("Die");
                break;
        }

        // B3: Update state
        curState = newState;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Sword")
        {
            curHp -= 30f;
        }
    }

    public void OnDead()
    {
        if(curHp <= 0)
        {
            curHp = 0;
            ChangeState(EnemyState.Die);
        }
    }

    public void Destroy()
    {
        Destroy(gameObject, 1f);
    }
}
