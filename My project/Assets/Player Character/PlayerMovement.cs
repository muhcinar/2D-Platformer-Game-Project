using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Traits")]
    public float speedFactor;
    public float jumpStrenght;
    public bool isGrounded;
    public bool isFacingRight;
    public bool canDoubleJump;

    private bool canDash = true;
    private bool isDashing = false;
    private float dashingPower = 6f;
    private float dashingTime = 0.2f;
    private float dashingCooldown = 0.5f;
    private float coyoteTime = 0.2f;
    private float coyoteTimeCounter;

    [Header("References")]
    public Rigidbody2D body;
    public Transform groundCheck;
    public PlayerInfo playerInfo;
    public PlayerAttack playerAttack;
    public LayerMask groundMask;
    public LayerMask hazardMask;
    [SerializeField] TrailRenderer trailRenderer;
    public AudioManager audioManager;
    public Animator animator;

    private float xInput;
    private float previousSpeed;
    private bool isMoving;
    private float rightScale;
    private float leftScale;

    void Start()
    {
        speedFactor = 2;
        jumpStrenght = 5;
        previousSpeed = 0;
        isFacingRight = true;
        isGrounded = true;
        rightScale = transform.localScale.x;
        leftScale = transform.localScale.x * -1;

        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
        animator = GameObject.FindGameObjectWithTag("PlayerAnimator").GetComponent<Animator>();
    }

    void Update()
    {
        if (playerInfo.isAlive)
        {
            GetInput();
            MoveWithInput();
        }
        else
        {
            body.linearVelocity = new Vector2(0f, 0f);
        }

    }

    void GetInput()
    {

        xInput = Input.GetAxis("Horizontal");
        if ((Mathf.Abs(xInput) - previousSpeed > 0) || (Mathf.Abs(xInput) == 1))
        {
            isMoving = true;
        }
        else
        {
            isMoving = false;
        }

        animator.SetBool("isMoving", isMoving);
        previousSpeed = Mathf.Abs(xInput);

    }

    void MoveWithInput()
    {

        if (isDashing)
        {
            return;
        }

        Jump();

        if (isMoving)
        {

            if(xInput > 0)
            {
                body.linearVelocity = new Vector2(speedFactor, body.linearVelocity.y);
            }
            else
            {
                body.linearVelocity = new Vector2(-speedFactor, body.linearVelocity.y);
            }

            TurnCharacter();

        }
        else
        {
            body.linearVelocity = new Vector2(0, body.linearVelocity.y);
        }

        if (Input.GetKeyDown(KeyCode.LeftShift) && canDash)
        {
            StartCoroutine(Dash());
        }

    }
    void Jump()
    {
        if (CheckGrounded())
        {
            coyoteTimeCounter = coyoteTime;
        }
        else
        {
            coyoteTimeCounter -= Time.deltaTime;
        }

        if (CheckGrounded() && !Input.GetButton("Jump"))
        {
            canDoubleJump = false;
        }

        if (Input.GetButtonDown("Jump"))
        {
            if (coyoteTimeCounter > 0 || canDoubleJump)
            {
                audioManager.PlaySFX(audioManager.jump);
                body.linearVelocity = new Vector2(body.linearVelocity.x, jumpStrenght);
                canDoubleJump = !canDoubleJump;
                coyoteTimeCounter = 0;
            }
        }
    }

    void TurnCharacter()
    {
        isFacingRight = Mathf.Sign(body.linearVelocity.x) > 0;

        if (isFacingRight)
        {
            transform.localScale = new Vector3(rightScale, transform.localScale.y, transform.localScale.z);
        }
        else
        {
            transform.localScale = new Vector3(leftScale, transform.localScale.y, transform.localScale.z);
        }

    }

    bool CheckGrounded()
    {

        isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.01f, groundMask);
        animator.SetBool("isGrounded", isGrounded);
        return isGrounded;
    }

    private IEnumerator Dash()
    {
        canDash = false;
        isDashing = true;
        animator.SetBool("isMoving", isDashing);

        float originalGravity = body.gravityScale;
        body.gravityScale = 0;

        if (isFacingRight)
        {
            body.linearVelocity = new Vector2(dashingPower, 0f);
        }
        else
        {
            body.linearVelocity = new Vector2(-dashingPower,0f);
        }

        audioManager.PlaySFX(audioManager.dash);
        trailRenderer.emitting = true;
        yield return new WaitForSeconds(dashingTime);
        trailRenderer.emitting = false;

        body.gravityScale = originalGravity;
        isDashing = false;
        animator.SetBool("isMoving", isDashing);
        yield return new WaitForSeconds(dashingCooldown);
        canDash = true;
    }

    public bool GetIsDashing()
    {
        return isDashing;
    }
}
