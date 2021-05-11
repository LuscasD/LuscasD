using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    public Transform attackPoint;
    public float attackRange = 0.5f;
    public LayerMask enemyLayer;
    public int attackDamage = 40;

    public float attackRate = 2f;
    float nextAtrackTime = 0f;

    public Animator animator;

    public GameObject[] hearts;
    private int life;
    public bool dead;
    public bool invencivel;
    float directionX;
    public bool isParrying;
    private bool isBlinking;
    


    private void Start()
    {
        life = hearts.Length;
        dead = false;
        invencivel = false;
        isParrying = false;
        isBlinking = false;

       
    }

    void Update()
    {
       if (Input.GetAxisRaw("Horizontal") == -1 || Input.GetAxisRaw("Horizontal") == 1)
        directionX = Input.GetAxisRaw("Horizontal");

        if (Time.time >= nextAtrackTime)
        {
            if (Input.GetKeyDown(KeyCode.J))
            {
                
                Attack();
                nextAtrackTime = Time.time + 1f / attackRate;
            }
            if (Input.GetKeyDown(KeyCode.K))
            {
                if (isBlinking == false)
                {
                    animator.SetTrigger("Parry");
                    nextAtrackTime = Time.time + 1f / attackRate;
                }
            }
            
        }

        /*if (life < 1)
        {
            hearts[0].SetActive(false);
        }
        else if(life < 2)
        {
            hearts[1].SetActive(false);
        }
        else if (life < 3)
        {
            hearts[2].SetActive(false);
        }*/

        

    }

    void Attack()
    {
        FindObjectOfType<AudioManager>().Play("Orc_ataque");
        animator.SetTrigger("Attack");
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayer); 

        foreach(Collider2D enemy in hitEnemies)
        {
            Debug.Log("Acertou o " + enemy.name);
            enemy.GetComponent<Enemy>().Damage(attackDamage);
            KnockBack();
           
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;

        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }


  
    
    public void TakeDamage(int d)
    {
        if (invencivel == false)
        {
            if (life >= 1)
            {
                life -= d;
                animator.SetTrigger("Dano");
                FindObjectOfType<AudioManager>().Play("Orc_Dano");
                hearts[life].SetActive(false);
                if (life < 1)
                {
                    Morto();
                }
            }
        }
    }
    public void Heal(int d)
    {
        if (!(life == hearts.Length))
        {
            hearts[life].SetActive(true);
            life += d;
        }
    }

    public  void InvencivelOn()
    {
        invencivel = true;
    }
    public void InvencivelOff()
    {
        invencivel = false;
    }
    public void Morto()
    {
        dead = true;

        Destroy(hearts[0].gameObject);
        Destroy(hearts[1].gameObject);
        Destroy(hearts[2].gameObject);

        Debug.Log("Morreu");
    }

    public void Parry()
    {
        isParrying = !isParrying;
    }

    public void KnockBack()
    {
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayer);

        foreach (Collider2D enemy in hitEnemies)
        {
            enemy.GetComponent<Enemy>().KnockBack(directionX);
        }
        ;
    }

    public void Blink()
    {
        isBlinking = !isBlinking;
    }
}
