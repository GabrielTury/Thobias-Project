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
    #endregion

    #region Check Variables
    [SerializeField] Transform groundCheckPoint;
    [SerializeField] Vector2 groundCheckSize;
    [SerializeField] LayerMask groundLayer;
    private float lastOnGroundTime;

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

    // Start is called before the first frame update
    void Start()
    {
        rig = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isDashing)
        {
            return;
        }

        lastOnGroundTime -= Time.deltaTime;
        moveInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        #region JUMP INPUTS
        if (Input.GetButtonDown("Jump"))
        {
            Jump();
        }
        if (Physics2D.OverlapBox(groundCheckPoint.position, groundCheckSize, 0, groundLayer)) //checks if set box overlaps with ground
        {
            lastOnGroundTime = 0;
        }
        if (lastOnGroundTime > -0.3f)
        {
            isJumping = false;
        }
        else if (lastOnGroundTime < -0.3f)
        {
            isJumping = true;
        }
        #endregion

        #region SHOOT
        anima.SetBool("Fire", false);

        if (Input.GetButtonDown("Fire1"))
        {
            StartCoroutine(Shoot());
        }
        #endregion

        if(Input.GetKeyDown(KeyCode.LeftShift) &&canDash)
        {
            StartCoroutine(Dash());
        }
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

    void Jump()
    {
        if (!isJumping)
        {
            rig.AddForce(jumpForce * Vector2.up, ForceMode2D.Impulse);
            isJumping = true;
        }
    }
    void ChangeDirection()
    {
        scale = transform.localScale;
        scale.x = 1.5f*moveInput;
        transform.localScale = scale;
    }

    IEnumerator Shoot()
    {
        while (Input.GetButton("Fire1"))
        {
            fire.Emit(1);
            anima.SetBool("Fire", true);
            yield return new WaitForSeconds(1);
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(groundCheckPoint.position, groundCheckSize);
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
        if (collision.CompareTag("RedGreenLight") && anima.GetCurrentAnimatorStateInfo(0).IsName("Thobias Run")) // no caso, trocar o "Thobias Run" para "dando dash"
        {
            RedGreenLight.activateItWasDashedRGLFunction = true;
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
        if (moveInput == 1 && verticalInput == 1)
        {
            rig.AddForce(new Vector2(1, 1) * dashingPowerUp, ForceMode2D.Impulse);
        }
        if (moveInput == -1 && verticalInput == 1)
        {
            rig.AddForce(new Vector2(-1, 1) * dashingPowerUp, ForceMode2D.Impulse);
        }
        else
        {
            rig.velocity = new Vector2(transform.localScale.x * dashingPower, 0f);
        }
        yield return new WaitForSeconds(dashingTime);
        rig.AddForce(breakForce * Vector2.right * Mathf.Sign(-rig.velocity.x), ForceMode2D.Force);
        rig.gravityScale = originalGravity;
        isDashing = false;
        yield return new WaitForSeconds(dashingCooldown);
        canDash = true;
    }
    #endregion
} 
