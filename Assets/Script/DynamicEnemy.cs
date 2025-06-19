using UnityEngine;

public class DynamicEnemy : MonoBehaviour
{
    public float moveSpeed = 2f;
    public float wallCheckDistance = 0.5f;
    public LayerMask groundLayer;

    private bool movingRight = true;

    void Update()
    {
        Vector3 dir = movingRight ? Vector3.right : Vector3.left;
        transform.Translate(dir * moveSpeed * Time.deltaTime);

        RaycastHit2D wallHit = Physics2D.Raycast(transform.position, dir, wallCheckDistance, groundLayer);

        if (wallHit.collider != null)
        {
            Flip();
        }
    }

    void Flip()
    {
        movingRight = !movingRight;

        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }
}
