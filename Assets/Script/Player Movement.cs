using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    [SerializeField] private float movementSpeed = 1f;
    [SerializeField] private float jumpForce = 1f;
    [SerializeField] private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        float movementAmount = Input.GetAxis("Horizontal") * movementSpeed * Time.deltaTime;
        transform.Translate(movementAmount, 0, 0);
        if (Input.GetButtonDown("Jump"))
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
        }
    }
}
