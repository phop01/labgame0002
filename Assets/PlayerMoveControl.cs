using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveControl : MonoBehaviour
{
    public float speed = 5f;
    private float direction = 1;
    public float jumpForce;
    public GatherInput gatherInput;
    public new Rigidbody2D rigidbody2D;

    public Animator animator;

    public float rayLength = 1;
    public LayerMask groundLayer;
    public Transform leftPoint;
    private bool grounded = false;
    private bool canDoubleJump = false;

    // Start is called before the first frame update
    void Start()
    {
        gatherInput = GetComponent<GatherInput>();
        rigidbody2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    private void SetAnimatorValue()
    {
        animator.SetFloat("Speed", Mathf.Abs(rigidbody2D.velocity.x));
        animator.SetFloat("vSpeed", rigidbody2D.velocity.y);
        animator.SetBool("Grounded", grounded);
    }

    private void CheckStatus()
    {
        RaycastHit2D leftCheckHit = Physics2D.Raycast(leftPoint.position, Vector2.down, rayLength, groundLayer);
        grounded = leftCheckHit;

        if (grounded)
        {
            canDoubleJump = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        SetAnimatorValue();
        rigidbody2D.velocity = new Vector2
            (
            speed * gatherInput.valueX,
            rigidbody2D.velocity.y
            );
    }

    private void FixedUpdate()
    {
        CheckStatus();
        Move();
        JumpPlayer();
    }

    private void Move()
    {
        Flip();
        rigidbody2D.velocity = new Vector2
            (
            speed * gatherInput.valueX,
            rigidbody2D.velocity.y
            );
    }

    private void Flip()
    {
        if (gatherInput.valueX * direction < 0)
        {
            transform.localScale = new Vector3(-transform.localScale.x, 1, 1);
            direction *= -1;
        }
    }

    private void JumpPlayer()
    {
        if (gatherInput.jumpInput && (grounded || canDoubleJump))
        {
            rigidbody2D.velocity = new Vector2
                (
                gatherInput.valueX * speed, jumpForce
                );

            if (!grounded)
            {
                canDoubleJump = false;
            }
        }
        gatherInput.jumpInput = false;
    }
}
