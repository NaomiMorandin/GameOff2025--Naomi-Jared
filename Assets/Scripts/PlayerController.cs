using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    public float walkSpeed = 3f;
    public float flightSpeed = 5f;
    public float ascendSpeed = 2f;
    public float glideFallSpeed = 1f;
    public float fastFallSpeed = 8f;

    [Header("Stats")]
    public float maxHP = 50f;
    public float maxSP = 100f;

    public float hp;   // runtime
    public float sp;

    [Header("HP Rates")]
    public float fireDamageRate = 5f;   // HP lost per second
    public float healRate = 5f;         // HP gained per second

    [Header("SP Rates")]
    public float flightDrainRate = 2f;  // per second
    public float glideDrainRate = 1f;   // per second
    public float groundedRegenRate = 5f;// per second
    private bool inFireZone = false;
    private bool inHQZone = false;

    private bool isGrounded = false;
    private bool canFly => sp > 0;
    private bool staminaLock => sp <= 0;

    private Animator animator;
    private Rigidbody2D rb;

    private float moveInput;
    private bool upHeld;
    private bool downHeld;

    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();

        hp = maxHP;
        sp = maxSP;
    }

    void Update()
    {
        moveInput = Input.GetAxisRaw("Horizontal");
        upHeld = Input.GetKey(KeyCode.W);
        downHeld = Input.GetKey(KeyCode.S);

        animator.SetFloat("Speed", Mathf.Abs(moveInput));

        HandleHP();
        HandleSP();
    }

    void FixedUpdate()
    {
        HandleMovement();
    }

    // -------------------------
    //        HEALTH
    // -------------------------
    void HandleHP()
    {
        if (inFireZone)
            hp -= fireDamageRate * Time.deltaTime;

        if (inHQZone)
            hp += healRate * Time.deltaTime;

        hp = Mathf.Clamp(hp, 0, maxHP);
    }

    // -------------------------
    //        STAMINA
    // -------------------------
    void HandleSP()
    {
        if (!isGrounded) // airborne
        {
            if (upHeld && !staminaLock)
                sp -= flightDrainRate * Time.deltaTime;

            else if (!downHeld) // gliding
                sp -= glideDrainRate * Time.deltaTime;
        }
        else // grounded
        {
            sp += groundedRegenRate * Time.deltaTime;
        }

        sp = Mathf.Clamp(sp, 0, maxSP);
    }

    // -------------------------
    //        MOVEMENT
    // -------------------------
    void HandleMovement()
    {
        float horizontalSpeed = isGrounded ? walkSpeed : flightSpeed;

        // Basic left/right
        rb.linearVelocity = new Vector2(moveInput * horizontalSpeed, rb.linearVelocity.y);

        // Flying / Ascending
        if (upHeld && !staminaLock)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, ascendSpeed);
        }
        // Gliding
        else if (!isGrounded && !downHeld)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, -glideFallSpeed);
        }
        // Fast fall
        else if (downHeld)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, -fastFallSpeed);
        }

        // Flip sprite
        if (moveInput > 0)
            transform.localScale = new Vector3(1, 1, 1);
        else if (moveInput < 0)
            transform.localScale = new Vector3(-1, 1, 1);
    }

    // -------------------------
    // DETECT GROUND
    // -------------------------
    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.collider.CompareTag("Ground"))
            isGrounded = true;
    }

    void OnCollisionExit2D(Collision2D col)
    {
        if (col.collider.CompareTag("Ground"))
            isGrounded = false;
    }

    // -------------------------
    // FIRE & HQ ZONES
    // -------------------------
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Fire"))
            inFireZone = true;

        if (col.CompareTag("HQ"))
            inHQZone = true;
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (col.CompareTag("Fire"))
            inFireZone = false;

        if (col.CompareTag("HQ"))
            inHQZone = false;
    }


}
