using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    #region zmienne
    public float speed = 1;
    public float jumpPower = 100;
    public float slideFactor = 0.2f;
    Rigidbody2D rb;
    Animator animator;
    [SerializeField] Transform groundCheckCollider;
    [SerializeField] LayerMask groundLayer;
    [SerializeField] Transform wallCheckCollider;
    [SerializeField] LayerMask wallLayer;
    [SerializeField] int totalJumps;
    int availableJumps;
    public GameObject objectToReveal1; 
    public GameObject objectToReveal2; 

    const float groundCheckRadius = 0.2f;
    const float wallCheckRadius = 0.2f;
    float horizontalValue;
    float runSpeedModifier = 1.5f;

    [SerializeField] bool isGrounded = false;
    bool isRunning;
    bool facingRight = true;
    bool multipleJump;
    bool isSliding;
    public bool isDead = false;

    #endregion

    void Awake()
    {
        availableJumps = totalJumps;
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Update()
{
    if (CanMoveOrInteract() == false)
        return;
    horizontalValue = Input.GetAxisRaw("Horizontal");

    if (Input.GetKeyDown(KeyCode.LeftShift) && isGrounded)
    {
        objectToReveal2.SetActive(true);
        isRunning = true;
        Invoke("DeactivateObjectToReveal2", 0.5f);
    }

    if (Input.GetKeyUp(KeyCode.LeftShift))
    {
        isRunning = false;
    }

    if (Input.GetButtonDown("Jump"))
    {
        Jump();
    }

    animator.SetFloat("yVelocity", rb.velocity.y);

    WallCheck();
}

    void DeactivateObjectToReveal2()
    {
        objectToReveal2.SetActive(false);
    }


    void FixedUpdate()
    {
        Move(horizontalValue);
        GroundCheck();
    }
    bool CanMoveOrInteract()
    {
        return !isDead;
    }

    void GroundCheck()
    {
        #region Ground check
        bool wasGrounded = isGrounded;
        isGrounded = false;
        Collider2D[] colliders = Physics2D.OverlapCircleAll(groundCheckCollider.position, groundCheckRadius, groundLayer);
        if (colliders.Length > 0)
        {
            isGrounded = true;
            if (!wasGrounded)
            {
                availableJumps = totalJumps;
                multipleJump = false;
                AudioManager.instance.PlaySFX("landing");
               
            }
        }

        animator.SetBool("Jump", !isGrounded);
        #endregion
    }

    void Move(float dir)
    {
        #region Move & Run
        if (!CanMoveOrInteract())
        {
            rb.velocity = Vector2.zero;
            return;
        }
        float xVal = dir * speed * 100 * Time.fixedDeltaTime;

        // Apply the running speed modifier before setting the velocity
        if (isRunning)
            xVal *= runSpeedModifier;

        Vector2 targetVelocity = new Vector2(xVal, rb.velocity.y);
        rb.velocity = targetVelocity;

        if (facingRight && dir < 0)
        {
            transform.localScale = new Vector3(-4, 4, 4);
            facingRight = false;
        }
        else if (!facingRight && dir > 0)
        {
            transform.localScale = new Vector3(4, 4, 4);
            facingRight = true;
        }

        animator.SetFloat("xVelocity", Mathf.Abs(rb.velocity.x));
        #endregion
    }

    void Jump()
{
    #region skok

    if (isGrounded)
    {
        multipleJump = true;
        availableJumps--;
        rb.velocity = Vector2.up * jumpPower;
        animator.SetBool("Jump", true);
        AudioManager.instance.PlaySFX("jump");
    }
    else
    {
        if (multipleJump && availableJumps > 0)
        {
            objectToReveal1.SetActive(true);
            AudioManager.instance.PlaySFX("jump");
            availableJumps--;
            rb.velocity = Vector2.up * jumpPower / 1.5f;
            animator.SetBool("Jump", true);
            Invoke("DeactivateObjectToReveal1", 0.5f); // Wywołanie metody "DeactivateObjectToReveal1" po 1 sekundzie
        }
    }
    #endregion
}

    void DeactivateObjectToReveal1()
    {
        objectToReveal1.SetActive(false);
    }


    void WallCheck()
    {
        #region Skok od sciany
        if (Physics2D.OverlapCircle(wallCheckCollider.position, wallCheckRadius, wallLayer)
            && Mathf.Abs(horizontalValue) > 0
            && rb.velocity.y < 0
            && !isGrounded)
        {
            if (!isSliding)
            {
                availableJumps = totalJumps;
                multipleJump = false;
            }
            Vector2 v = rb.velocity;
            v.y = -slideFactor;
            rb.velocity = v;
            isSliding = true;

            if (Input.GetButtonDown("Jump"))
            {
                availableJumps--;

                rb.velocity = Vector2.up * jumpPower;
                animator.SetBool("Jump", true);
            }
        }
        else
        {
            isSliding = false;
        }
        #endregion
    }
}
