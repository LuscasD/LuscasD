using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleporter : MonoBehaviour
{
    public GameObject portal, necromante;
    private Animator anim;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Necromante")
        {
            necromante = GameObject.FindGameObjectWithTag("Necromante");
            StartCoroutine(Teleport());
        }
    }

    IEnumerator Teleport()
    {
       
        yield return new WaitForSeconds(3);
        necromante.transform.position = new Vector2(portal.transform.position.x, portal.transform.position.y);
      


    }
}
