using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class CharacterMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    private Animator animator;
    private Rigidbody2D rb;
    private float moveInput;

    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // Get A/D input (mapped to Horizontal axis)
        moveInput = Input.GetAxisRaw("Horizontal");

        // Update animator based on speed (absolute value)
        animator.SetFloat("Speed", Mathf.Abs(moveInput));
    }

    void FixedUpdate()
    {
        // Move character left/right
        rb.linearVelocity = new Vector2(moveInput * moveSpeed, rb.linearVelocity.y);

        // Flip sprite based on direction
        if (moveInput > 0)
            transform.localScale = new Vector3(1, 1, 1); // facing right
        else if (moveInput < 0)
            transform.localScale = new Vector3(-1, 1, 1); // facing left
    }
}
