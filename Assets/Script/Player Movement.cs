using UnityEngine;

public class Player : MonoBehaviour
{

    [SerializeField] private float moveSpeed = 1f;
    [SerializeField] private float jumpForce = 1f;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundCheckRadius = 0.1f;
    [SerializeField] private float inDashTime = 1f;
    [SerializeField] private float dashSpeed = 1f;

    private bool dash = true;
    private bool isGrounded;
    private bool isDashing;
    private float dashTimer;
    private int moveInput;
    private int verticalInput;
    private Vector2 dashDirection;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (!isDashing)
        {
            moveInput = (int)Input.GetAxisRaw("Horizontal");
            verticalInput = (int)Input.GetAxisRaw("Vertical");
        }
        rb.linearVelocity = new Vector2(moveInput * moveSpeed, rb.linearVelocity.y);
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
            dash = true;
        }
        else
        {
            if (Input.GetButtonDown("Jump") && dash)
            {
                Vector2 inputDir = new Vector2(moveInput, verticalInput);
                if (inputDir != Vector2.zero)
                {
                    dashDirection = inputDir.normalized;
                    dashStart();
                }
            }
        }
    }

    void FixedUpdate()
    {
        if (isDashing)
        {
            rb.linearVelocity = dashDirection * dashSpeed;
            dashTimer -= Time.deltaTime;
            if(dashTimer <= 0f)
            {
                dashEnd();
            }
        }
    }

    void dashStart()
    {
        isDashing = true;
        dashTimer = inDashTime;
        dash = false;
    }

    void dashEnd()
    {
        isDashing = false;
        rb.linearVelocity = Vector2.zero;
    }

    void OnTriggerEnter2D()
    {
        dash = true;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        dash = false;
        if (isDashing)
        {
            dashEnd();
        }
    }

}
