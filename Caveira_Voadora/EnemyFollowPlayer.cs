using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFollowPlayer : MonoBehaviour
{
    public float speed;
    public float lineOfSite;
    
    
    public float shootingRange;
    public float fireRate = 1f;
    private float nextFireTime;
     public GameObject bullet;
    public GameObject bulletParent;

    public Rigidbody2D enemyRig;
    private Transform player;
    public Animator animator;


    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        float distanceFromPlayer = Vector2.Distance(player.position, transform.position);
        if (distanceFromPlayer < lineOfSite && distanceFromPlayer>shootingRange)
        {
            animator.SetBool("SePlayer", true);
            transform.position = Vector2.MoveTowards(this.transform.position, player.position, speed * Time.deltaTime);
        }
        else if (distanceFromPlayer <= shootingRange)
        {
            animator.SetBool("SePlayer", true);
            if (nextFireTime < Time.time)
            {
                Instantiate(bullet, bulletParent.transform.position, Quaternion.identity);
                nextFireTime = Time.time + fireRate;
            }
        }
        else 
        {
            animator.SetBool("SePlayer", false);

        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            StartCoroutine(MoonWalk());
        }
    }
    IEnumerator MoonWalk()
    {
        yield return new WaitForSeconds(2f);
        enemyRig.velocity = Vector2.zero;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, lineOfSite);
        Gizmos.DrawWireSphere(transform.position, shootingRange);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
       if (collision.gameObject.tag == "Player")
       {
           StartCoroutine(MoonWalk());
       }
        
        IEnumerator MoonWalk()
        {
            yield return new WaitForSeconds(2f);
            enemyRig.velocity = Vector2.zero;
        }
    }


}
