using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Experimental.AI;

public class EnemyAI : MonoBehaviour
{
    public NavMeshAgent navMeshAgent;
    public Transform target;

    public float radius = 10f;
    public Vector3 originalPos;
    public float maxDistace = 50f;

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
        if(distance <= radius)
        {
            // di chuyển đến mục tiêu
            navMeshAgent.SetDestination(target.position);
        }

        if(distance > radius || distanceToOriginal > maxDistace)
        {
            navMeshAgent.SetDestination(originalPos);
        }
    }

}
