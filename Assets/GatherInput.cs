using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GatherInput : MonoBehaviour
{
    public Control controls;
    public float valueX = 0;
    public bool jumpInput;

    private Rigidbody2D rb;

    public void Awake()
    {
        controls = new Control();
        rb = GetComponent<Rigidbody2D>();  // ดึง Rigidbody2D ของตัวละคร
    }

    private void OnEnable()
    {
        controls.Player.Move.performed += StartMove;
        controls.Player.Move.canceled += StopMove;
        controls.Player.Jump.performed += JumpStart;
        controls.Player.Jump.canceled += JumpStop;
        controls.Player.Enable();
    }

    private void OnDisable()
    {
        controls.Player.Move.performed -= StartMove;
        controls.Player.Move.canceled -= StopMove;
        controls.Player.Jump.performed -= JumpStart;
        controls.Player.Jump.canceled -= JumpStop;
        controls.Player.Disable();
    }

    public void DisableControls()
    {
        // ปิดการควบคุมทั้งหมด
        controls.Player.Move.performed -= StartMove;
        controls.Player.Move.canceled -= StopMove;
        controls.Player.Jump.performed -= JumpStart;
        controls.Player.Jump.canceled -= JumpStop;
        controls.Player.Disable();

        // รีเซ็ตค่า input ทั้งหมด
        valueX = 0;
        jumpInput = false;

        // หยุดการเคลื่อนไหวของ Rigidbody2D
        if (rb != null)
        {
            rb.velocity = Vector2.zero;  // รีเซ็ตความเร็ว
            rb.constraints = RigidbodyConstraints2D.FreezeAll;  // ล็อคการเคลื่อนไหวทุกอย่าง
        }
    }

    private void StartMove(InputAction.CallbackContext ctx)
    {
        valueX = ctx.ReadValue<float>();
    }

    private void StopMove(InputAction.CallbackContext ctx)
    {
        valueX = 0;
    }

    private void JumpStart(InputAction.CallbackContext ctx)
    {
        jumpInput = true;
    }

    private void JumpStop(InputAction.CallbackContext ctx)
    {
        jumpInput = false;
    }
}


