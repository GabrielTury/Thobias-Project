using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewControls : MonoBehaviour
{
    //Components
    public Rigidbody2D rig;

    //Parameters
    public bool isFacingRight { get; private set;}
    public float lastOnGroundTime { get; private set;}
    public float maxSpeed;
    public float acceleration, deceleration; // em quanto tempo quer que chege a velocidade máxima
    [Range(0.01f, 1)] public float airAcceleration,airDeceleration; // Controla a % em que a aceleração funciona no ar
    bool conserveMomentum;

    //Input
    private Vector2 moveInput;

    //Check Parameters
    [SerializeField] Transform groundCheckPoint;
    [SerializeField] Vector2 groundCheckSize = new Vector2(0.49f, 0.03f);

    //Layers & Tags
    [SerializeField] LayerMask groundLayer;

    // Start is called before the first frame update
    private void Awake()
    {
        rig = GetComponent<Rigidbody2D>();
    }
    void Start()
    {
        isFacingRight = true;
    }

    void Update()
    {
        lastOnGroundTime = Time.deltaTime;

        moveInput.x = Input.GetAxisRaw("Horizontal");

        if(moveInput.x != 0)
            CheckDirection(moveInput.x > 0);

        //Ground Check
        if (Physics2D.OverlapBox(groundCheckPoint.position, groundCheckSize, 0, groundLayer)) //checks if set box overlaps with ground
            lastOnGroundTime = 0.1f;

    }
    private void FixedUpdate()
    {
        Run();
    }
    void Run()
    {
        //Coloca o sinal que representa a direção na velocidade
        float targetSpeed = moveInput.x * maxSpeed;

        //Aceleração
        float accelRate;

        //Aceleração e freio com multiplicadores caso esteja no ar
        if (lastOnGroundTime > 0)
            accelRate = (Mathf.Abs(targetSpeed) > 0.01f) ? acceleration : deceleration;
        else
            accelRate = (Mathf.Abs(targetSpeed) > 0.01f) ? acceleration * airAcceleration : deceleration * airDeceleration;
  
        //Não desacelerar o jogador caso ele esteja acima do maxSpeed, mas sem o acelerar, apenas mantendo o momentum
        if (conserveMomentum && Mathf.Abs(rig.velocity.x) > Mathf.Abs(targetSpeed) && Mathf.Sign(rig.velocity.x) == Mathf.Sign(targetSpeed) && Mathf.Abs(targetSpeed) > 0.01f && lastOnGroundTime < 0)
        {
            //Conserva momentum
            accelRate = 0;
        }

        //Calcula a diferença entre a velocidade atual e a velocidade desejada (maxSpeed x direção)
        float speedDif = targetSpeed - rig.velocity.x;
        //Calcula a força que deve ser aplicada ao player usando a aceleração e a diferença de velocidade atual pra desejada

        float movement = speedDif * accelRate;

        //Converte o float para um vetor (com Vector2.right) e aplica no rigidbody
        rig.AddForce(movement * Vector2.right, ForceMode2D.Force);
    }
    void Turn()
    {
        Vector2 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;

        isFacingRight = !isFacingRight;

    }

    void CheckDirection(bool isMovingRight)
    {
        if (isMovingRight != isFacingRight)
            Turn();
        
    }
}
