using UnityEngine;

public class Player : MonoBehaviour
{

    [SerializeField] private float moveSpeed = 1f;
    [SerializeField] private float jumpForce = 1f;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform firstSpawn;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundCheckRadius = 0.1f;
    [SerializeField] private float inDashTime = 1f;
    [SerializeField] private float dashSpeed = 1f;
    public bool canMove;
    public EndLevelLogic endLevelLogic;

    private Vector3 respawnPoint;
    [SerializeField] private bool dash = true;
    [SerializeField] private bool isGrounded;
    private bool isDashing;
    private float dashTimer;
    private int moveInput;
    private int verticalInput;
    private Vector2 dashDirection;
    private SpriteRenderer sprite;

    void Start()
    {
        canMove = true;
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        respawnPoint = firstSpawn.position;
        Respawn();
    }

    void Update()
    {
        if (!canMove)
        {
            moveInput = 0;
            verticalInput = 0;
            return;
        }
        if (!isDashing)
        {
            moveInput = (int)Input.GetAxisRaw("Horizontal");
            verticalInput = (int)Input.GetAxisRaw("Vertical");
        }
        if (moveInput > 0)
            sprite.flipX = false;
        else if (moveInput < 0)
            sprite.flipX = true;
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

    void Die()
    {
        endLevelLogic.AddDeath();
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

    private void Respawn()
    {
        transform.position = respawnPoint;
        rb.linearVelocity = Vector3.zero;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Refile")) 
        {
            dash = true;
        }

        if (other.CompareTag("Checkpoint"))
        {
            respawnPoint = other.transform.position;
        }

        if (other.CompareTag("Death"))
        {
            Die();
            Respawn();
        }
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
