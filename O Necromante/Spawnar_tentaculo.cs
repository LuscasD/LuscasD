using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawnar_tentaculo : MonoBehaviour
{
    IA_Necromante necro;
    public GameObject enemy;
    Vector2 whereToSpawn;
    public float spawnRat = 2f;
    float nextSpawn = 0.0f;
    float randX;
    void Update()
    {
        if (Time.time > nextSpawn)
        {
            nextSpawn = Time.time + spawnRat;
            randX = Random.Range(-8.4f, 8.4f);
            whereToSpawn = new Vector2(transform.position.x, transform.position.y);

        }
    }


    public void Spawn()
    {
        Instantiate(enemy, whereToSpawn, Quaternion.identity);
    }

}
