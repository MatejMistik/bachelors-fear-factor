using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// Brackeys Grenade/Bomb in Unity tutorial
public class Bomb : MonoBehaviour
{
    public float bombTimer;
    public GameObject explosionEffect;

    private float countDown;
    private bool hasExploded = false;
    public float radius;
    public float explosionForce;
    NewEnemyHealth enemyHealth;
    Rigidbody[] rigidbodies;
    BombTimer bombTimerScript;
    
    private float explosionDamage;

    // Start is called before the first frame update
    void Start()
    {
        countDown = bombTimer;
        enemyHealth = GameObject.Find("NewEnemy").GetComponent<NewEnemyHealth>();
        rigidbodies = GameObject.Find("NewEnemy").GetComponentsInChildren<Rigidbody>();
        bombTimerScript = GameObject.Find("BombTimer").GetComponent<BombTimer>();
        bombTimerScript.StartTimer(countDown);


    }

    // Update is called once per frame
    void Update()
    {
        countDown -= Time.deltaTime;
        if(countDown <= 0f && !hasExploded)
        {
            Explode();
            hasExploded = true;
        }
    }

     void Explode()
    {
        Instantiate(explosionEffect, transform.position, transform.rotation);
        Debug.Log("boom");

        foreach(Rigidbody rigidbody in rigidbodies)
        {

        rigidbody.AddExplosionForce(explosionForce, transform.position, radius);
        explosionDamage = 100 * Mathf.Abs(1f / (transform.position.magnitude - rigidbody.transform.position.magnitude)) / rigidbodies.Length;
        Debug.Log("Explosion" + explosionDamage);
        Debug.Log(rigidbody.name);
        enemyHealth.TakeDamage(explosionDamage);
        
        
        }

        Destroy(gameObject);

        
    }
}
