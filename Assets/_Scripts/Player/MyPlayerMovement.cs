using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyPlayerMovement : MonoBehaviour
{
    public CharacterController controller;
    public PlayerInput playerInput;
    public Transform cam;

    public float speed = 6f;
    public float moveValue;

    public float turnSmoothTime = 0.1f;
    float turnSmoothVerlocity;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.visible = true;
        }

        Run();
        Dash();
    }

    public void CaculateMovement()
    {

        Vector3 direction = new Vector3(playerInput.horizontalInput, 0, playerInput.verticalInput).normalized;

        moveValue = direction.magnitude;
            
        if (direction.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVerlocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;

            controller.Move(moveDir.normalized * speed * Time.deltaTime);
        }
    }

    void Run()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            speed = 12f;
        }
        else
        {
            speed = 6f;
        }
    }

    void Dash()
    {
        if(Input.GetMouseButton(1))
        {
            speed = 50f;
            Invoke(nameof(EndDash), 0.2f);
        }
    }

    void EndDash()
    {
        speed = 6f;
    }
}
