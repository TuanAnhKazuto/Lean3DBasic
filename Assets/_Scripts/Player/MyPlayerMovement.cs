using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MyPlayerMovement : MonoBehaviour
{
    public CharacterController controller;
    public PlayerInput playerInput;
    public Character character;
    public Transform cam;

    public float speed = 6f;
    public float moveValue;

    public float turnSmoothTime = 0.1f;
    float turnSmoothVerlocity;

    // Stamina
    public Slider staminaSlider;
    float curStamina;
    [HideInInspector] public float maxStammina = 100f;
    bool isRuning = false;
    bool isDashing = false;

    //Dash
    public float dashTime;
    private float _dashTime;


    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        curStamina = maxStammina;
        staminaSlider.value = curStamina;
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.visible = true;
        }

        Run();
        Dash();
        Stamina();
    }

    public void CaculateMovement()
    {
        if(character.onDead) return;
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

    public void Stamina()
    {
        if(curStamina < maxStammina && !isRuning && !isDashing)
        {
            curStamina += 3f * Time.deltaTime;
            staminaSlider.value = curStamina;
        }
    }

    void Run()
    {
        if (Input.GetKey(KeyCode.LeftShift) && curStamina > 5f)
        {
            curStamina -= 5f * Time.deltaTime;
            staminaSlider.value = curStamina;
            speed = 13f;
            isRuning = true;
        }
        else
        {
            speed = 6f;
            isRuning = false;   
        }
    }

    void Dash()
    {
        // Kiểm tra điều kiện dash
        if (Input.GetMouseButtonDown(1) && !isDashing && _dashTime <= 0 && curStamina >= 15f)
        {
            curStamina -= 15f; // Trừ stamina
            staminaSlider.value = curStamina;
            speed = 50f; // Tăng tốc độ dash
            isDashing = true; // Đánh dấu trạng thái đang dash
            _dashTime = dashTime; // Reset thời gian dash
        }

        // Quản lý thời gian dash
        if (_dashTime > 0)
        {
            _dashTime -= Time.deltaTime; // Giảm dần thời gian dash
        }
        else if (_dashTime <= 0)
        {
            speed = 6f;
            isDashing = false;
        }
    }


}
