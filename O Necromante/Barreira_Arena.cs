using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barreira_Arena : MonoBehaviour
{
    public GameObject Coisa1;
    public GameObject Coisa2;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {

            Coisa1.SetActive(true);
            Coisa2.SetActive(true);


        }
    }
}