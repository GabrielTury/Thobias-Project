using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewControls : MonoBehaviour
{

    Rigidbody2D rig;
    #region Move Variables
    private float verticalInput;
    private float moveInput;
    [HideInInspector] public float moveForce;
    public float maxSpeed;
    public float initialMoveForce;
    Vector2 scale;
    public float breakForce;
    #endregion

    #region Stuff
    public Animator anima;
    public ParticleSystem fire;
    //Collider2D m_Collider;
    #endregion

    #region Check Variables
    [SerializeField] Transform groundCheckPoint;
    [SerializeField] Transform frontCheck;
    [SerializeField] Vector2 frontCheckSize;
    [SerializeField] Vector2 groundCheckSize;
    [SerializeField] LayerMask groundLayer;
    private float lastOnGroundTime;
    public float coyoteTime = 0.3f;
    private bool isShooting;

    #endregion

    #region Jump Variables
    public float jumpForce;
    private bool isJumping;

    #endregion

    #region Dash Variables
    private bool canDash = true;
    private bool isDashing;
    private float dashingPower = 6f;
    private float dashingTime = 0.2f;
    private float dashingCooldown = 1f;
    private float dashingPowerUp = 8f;
    #endregion

    #region WallJump Variables
    bool isTouchingFront;
    bool wallSliding;
    public float wallSlidingSpeed;
    bool wallJumping;
    public float xWallForce;
    public float yWallForce;
    public float wallJumpTime;

    #endregion

    // Start is called before the first frame update
    void Start()
    {
        rig = GetComponent<Rigidbody2D>();
        //m_Collider = GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        /*if(isDashing)
        {
            m_Collider.enabled = !m_Collider.enabled;
        }*/
        
        if (isDashing)
        {
            return;
        }

        moveInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        #region JUMP INPUTS
        if (Input.GetButtonDown("Jump"))
        {
            Jump();
        }
        if (IsGrounded()) 
        { 
            lastOnGroundTime = coyoteTime;
        }
        else
        {
            lastOnGroundTime -= Time.deltaTime;
        }
        #endregion

        #region SHOOT
        anima.SetBool("Fire", false);

        if (Input.GetButtonDown("Fire1") && !isShooting)
        {
            StartCoroutine(Shoot());
        }
        #endregion

        #region Dash Inputs
        if (Input.GetKeyDown(KeyCode.LeftShift) && canDash)
        {
            StartCoroutine(Dash());
        }
        #endregion

        #region WALLSLIDE

        /*isTouchingFront = Physics2D.OverlapBox(frontCheck.position, frontCheckSize, groundLayer);

        if (isTouchingFront == true && IsGrounded() == false && moveInput !=0)
        {
            wallSliding = true;
        }
        else
        {
            wallSliding = false;
        }

        if (wallSliding)
        {
            rig.velocity = new Vector2(rig.velocity.x, Mathf.Clamp(rig.velocity.y, -wallSlidingSpeed, float.MaxValue));
        }

        if (Input.GetKeyDown(KeyCode.Space) && wallSliding == true)
        {
            wallJumping = true;
            Invoke("SetWallJumpingToFalse", wallJumpTime);
        }

        if (wallJumping == true)
        {
            rig.velocity = new Vector2(xWallForce * -moveInput, yWallForce);
        }*/
        #endregion
    }

    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheckPoint.position, 0.2f, groundLayer);
    }

    void SetWallJumpingToFalse ()
    {
        wallJumping = false;
    }

    private void FixedUpdate()
    {
        if(isDashing)
        {
            return;
        }
        
        anima.SetFloat("Velocity", Mathf.Abs(moveInput));

        if (moveInput != 0)
            Run();
        else if (Mathf.Abs(rig.velocity.x) != 0)
            Break();       
    }

    private void Break()
    {
        rig.AddForce(breakForce * Vector2.right * Mathf.Sign(-rig.velocity.x), ForceMode2D.Force);
    }

    #region RUN
    void Run()
    {
        if (Mathf.Sign(moveInput) != Mathf.Sign(scale.x))
        {
            ChangeDirection();
        }

        rig.AddForce(moveForce * Vector2.right * Mathf.Sign(moveInput), ForceMode2D.Force);

        if (Mathf.Abs(rig.velocity.x) > maxSpeed)
        {
            moveForce = 0;
        }
        else
        {
            moveForce = initialMoveForce;
        }
    }
    #endregion

    #region JUMP
    void Jump()
    {
        if (lastOnGroundTime > 0f && !isJumping)
        {
            rig.AddForce(jumpForce * Vector2.up, ForceMode2D.Impulse);
            lastOnGroundTime = 0f;
            StartCoroutine(JumpCooldown());
        }
    }

    IEnumerator JumpCooldown()
    {
        isJumping = true;
        yield return new WaitForSeconds(0.35f);
        isJumping = false;

    }
    #endregion
    void ChangeDirection()
    {
        scale = transform.localScale;
        scale.x = 1.5f*moveInput;
        transform.localScale = scale;
        float actualVelocity;
        actualVelocity = rig.velocity.x;
        print(actualVelocity);
        actualVelocity *= -0.3f;
        print(actualVelocity);
        rig.velocity = new Vector2(actualVelocity, rig.velocity.y);
    }

    IEnumerator Shoot()
    {
        isShooting = true;
        while (Input.GetButton("Fire1"))
        {
            fire.Emit(1);
            anima.SetBool("Fire", true);
            yield return new WaitForSeconds(1);
        }
        isShooting = false;
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawSphere(groundCheckPoint.position, 0.2f);
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(frontCheck.position, frontCheckSize);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Damage") || collision.collider.CompareTag("Enemy"))
        {
            LevelManager.instance.LowDamage();
            SimpleFollow.instance.HitAnimation();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("RedGreenLight") && isDashing)//depois, trocar o isDashing pela animação [ anima.GetCurrentAnimatorStateInfo(0).IsName("Thobias Dashing ou algo assim") ]
        {
            collision.gameObject.GetComponent<RedGreenLight>().ItWasDashed();
        }
    }

    private void OnParticleCollision(GameObject other)
    {
        if (other.gameObject.CompareTag("EnemyShot"))
        {
            LevelManager.instance.LowDamage();
        }
    }

    #region DASH
    private IEnumerator Dash()
    {
        canDash = false;
        isDashing = true;
        float originalGravity = rig.gravityScale;
        rig.gravityScale = 0f;
        rig.velocity = Vector3.zero;
        if (moveInput == 0 && verticalInput == 1)
        {
            rig.AddForce(new Vector2(0, 1) * dashingPowerUp, ForceMode2D.Impulse);
        }
        else if (moveInput == 1 && verticalInput == 1)
        {
            rig.AddForce(new Vector2(1, 1) * dashingPowerUp, ForceMode2D.Impulse);
        }
        else if (moveInput == -1 && verticalInput == 1)
        {
            rig.AddForce(new Vector2(-1, 1) * dashingPowerUp, ForceMode2D.Impulse);
        }
        else if(moveInput !=0)
        {
            rig.velocity = new Vector2(transform.localScale.x * dashingPower, 0f);
        }
        yield return new WaitForSeconds(dashingTime);
        rig.AddForce(breakForce * Vector2.right * Mathf.Sign(-rig.velocity.x), ForceMode2D.Force);
        rig.gravityScale = originalGravity;
        isDashing = false;
        //Não Ignorar o collider
        yield return new WaitForSeconds(dashingCooldown);
        canDash = true;
    }
    #endregion
} 
