using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Taruga_IA : MonoBehaviour
{
    public Transform rayCast;
    public LayerMask rayCastMask;
    public float rayCastLength;
    public float attackDistance;
    public float moveSpeed;
    public float startDazedTime;
    public int knockBack;
    public int knockUp;
    public int downForce;

    private bool attackMode;
    private RaycastHit2D hit;
    private GameObject target;
    private float distance;
    private bool inRange;
    private Animator anim;
    private bool facingLeft;
    private float stunTimer;
    private PlayerCombat player;
    private Rigidbody2D rig;
    private bool isStuned;
    


    private void Awake()
    {
        anim = GetComponent<Animator>();
        attackMode = false;
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerCombat>();
        rig = GetComponent<Rigidbody2D>();
        isStuned = false;
    }

    void Update()
    {
        if (attackMode)
        {
            Debug.Log("Puto");
            Move();
        }
         Stun();

    }
      void OnTriggerStay2D(Collider2D trig)
     {
        if (trig.gameObject.tag == "Player")
        {
            target = trig.gameObject;
            attackMode = true; 
        }
     }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {

           if (isStuned == false)
            {
                stunTimer = startDazedTime;
                KnockBack();
            }
        }
       
        if (collision.gameObject.tag == "Wall")
        {
            stunTimer = startDazedTime;
            Flip();
        }
        if (collision.gameObject.tag == "Ground")
        {
            return;
        }
        
    }
    
    void Move()
    {
        anim.SetTrigger("Rolando");
        Vector2 targetPosition = new Vector2(target.transform.position.x, transform.position.y);
        transform.position = Vector2.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
        
    }

    
    public void Flip()
    {
        if (!facingLeft)
        {
            transform.localScale = new Vector2(transform.localScale.x * -1, transform.localScale.y);
            facingLeft = true;
        }
        else if (facingLeft)
        {
            transform.localScale = new Vector2(transform.localScale.x * -1, transform.localScale.y);
            facingLeft = false;
        }
    }

    void Stun()
    {
        if(stunTimer <= 0)
        {
            isStuned = false;
            anim.SetBool("Stun", false);
            moveSpeed = 4;
        }
        else
        {
            isStuned = true;
            anim.SetBool("Stun", true);
            attackMode = false;
            moveSpeed = 0;
            stunTimer -= Time.deltaTime;
        }
    }

    void KnockBack()
    {
        rig.AddForce(Vector2.up * knockUp);

        if (!facingLeft)
        {
            rig.AddForce(Vector2.right * knockBack);

        }
        else if (facingLeft)
        {
            rig.AddForce(Vector2.left * knockBack);
        }
        if (rig.velocity.y < 0)
        {
            rig.AddForce(Vector2.down * downForce);
        }
    }
}

