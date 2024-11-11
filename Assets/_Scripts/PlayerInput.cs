using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    [HideInInspector] public float horizontalInput;
    [HideInInspector] public float verticalInput;

    public bool attackInput;

    private void Update()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        if(!attackInput && Time.timeScale != 0)
        {
            attackInput = Input.GetMouseButtonDown(0);
        }
    }

    private void OnDisable()
    {
        horizontalInput = 0;
        verticalInput = 0;
        attackInput = false;
    }
}
