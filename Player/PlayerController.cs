using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerController : MonoBehaviour
{

    Rigidbody2D playerRig;
    SpriteRenderer playerSprite;
    public float speed = 10;
    float directionX;
    bool facingRight = true;

    public float jumpForce = 350;
    public float downForce = 150;
    public Transform groundCheck;
    public LayerMask whatIsGround;
    bool grounded;
    public float groundDistance = 0.4f;
    public Animator animator;
  
    [Header("Events")]
  
    [Space]
    
    private Rigidbody2D m_Rigidbody2D;
    public UnityEvent OnLandEvent;

   
    [System.Serializable]
    public class BoolEvent : UnityEvent<bool> { }

   
    private void Awake()
    {
        m_Rigidbody2D = GetComponent<Rigidbody2D>();

        if (OnLandEvent == null)
            OnLandEvent = new UnityEvent();

       
    }

    // Start eh chamado antes do primeiro update de frame
    void Start()
    {
        playerRig = GetComponent<Rigidbody2D>();
    }

    // Update eh chamado uma fez por frame
    void Update()
    {
        directionX = Input.GetAxisRaw("Horizontal");

        animator.SetFloat("Speed", Mathf.Abs(directionX));

        MovePlayer();
        Jump();
    }

    void FlipPlayer()
    {
        if(directionX > 0 && !facingRight)
        {
            transform.localScale = new Vector2(transform.localScale.x * -1, transform.localScale.y);
            facingRight = true;
        }
        else if (directionX < 0 && facingRight)
        {
            transform.localScale = new Vector2(transform.localScale.x * -1, transform.localScale.y);
            facingRight = false;
        }
    }

    void MovePlayer()
    {
        playerRig.velocity = new Vector2(directionX * speed, playerRig.velocity.y);
        FlipPlayer();

    }
 


    void Jump()
    {
        // verificar se o player está tocando o chão
        grounded = Physics2D.OverlapCircle(groundCheck.position, groundDistance, whatIsGround);

        // verificar se podemos pular
        if (grounded && Input.GetButtonDown("Jump"))
        {
            FindObjectOfType<AudioManager>().Play("Orc_Pulando");
            playerRig.AddForce(Vector2.up * jumpForce);
            animator.SetBool("IsJumping", true);          

        }
        else if (grounded)
        {
           animator.SetBool("IsJumping", false);
        }

        if (playerRig.velocity.y < 0)
        {
            playerRig.AddForce(Vector2.down * downForce);
        }

    }


    public void OnLanding ()
    {
        animator.SetBool("IsJumping", false);
    }
}
    