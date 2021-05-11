using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IA_Necromante : MonoBehaviour
{
    public float speed;
    public float lineOfSite;
    public float shotingRange;
    public float fireRate = 1f;

    private float nextFireTime;
    public GameObject bullet;
    public GameObject bulletParent;
    public GameObject cPlace;
    private GameObject teleport;
    private Transform player;
    public bool facingLeft;
    public float startCDTime;
    public float startAtkTime;
    private Animator anim;
    public BoxCollider2D col;
    Spawnar_tentaculo st;

    private bool canalizando;
    private float CD;
    private float atk;
    private Enemy vida;
    private Rigidbody2D enemyRig;
 




    void Start()
    {
        atk = 0.5f;
        CD = startCDTime;
        vida = GameObject.FindGameObjectWithTag("Necromante").GetComponent<Enemy>();
        canalizando = false;
        player = GameObject.FindGameObjectWithTag("Player").transform;
        cPlace = GameObject.FindGameObjectWithTag("cPlace");
        teleport = GameObject.FindGameObjectWithTag("Teleport");
        st = GameObject.FindGameObjectWithTag("Tentaculo").GetComponent<Spawnar_tentaculo>();
        facingLeft = true;
        anim = GetComponent<Animator>();

    }

    void Update()
    {
        if (player.position.x > transform.position.x)
        {
            //face right
            transform.localScale = new Vector3(0.176f, 0.1768f, 0);
        }
        else if (player.position.x < transform.position.x)
        {
            //face left
            transform.localScale = new Vector3(-0.176f, 0.1768f, 0);
        }

        if (canalizando == false)
        {
            float distanceFromPlayer = Vector2.Distance(player.position, transform.position);

            if (distanceFromPlayer <= shotingRange && nextFireTime < Time.time)
            {
                Instantiate(bullet, bulletParent.transform.position, Quaternion.identity);
                nextFireTime = Time.time + fireRate;
            }
        }
    
        if (canalizando == true)
        {
            Canalizando();
        }
       
        if (CD <= 0)
        {
            canalizando = true;
            if (atk == 0.5f)
               
            { atk = startAtkTime; }
        }
        else
        {
            CD -= Time.deltaTime;
        }Debug.Log(canalizando);
    }

    private void Especial()
    {
        anim.SetBool("Canalizando", false);
        CD = startCDTime;
        canalizando = false;
        st.Spawn();
        ResetPosition();
        
        
    }
    private void Canalizando()
    {
        anim.SetBool("Canalizando", true);
        if (atk <= 0)
        {
            atk = 0.5f;
            Especial();
        }
        else
        {
            transform.position = new Vector2(cPlace.transform.position.x, cPlace.transform.position.y);
            atk -= Time.deltaTime;
        }
        
    }

    public void SetAtackTimer()
    {
        atk = 0.5f;
          anim.SetBool("Canalizando", false);
        CD = startCDTime;
        canalizando = false;
        ResetPosition();
        
    }
   
    public void ResetPosition()
    {
        transform.position = new Vector2(teleport.transform.position.x, teleport.transform.position.y);
    }
    private void OnCol()
    {
        col.enabled = true;
    }
    private void OffCol()
    {
        col.enabled = false;
    }
  
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, lineOfSite);
        Gizmos.DrawWireSphere(transform.position, shotingRange);
    }


}
