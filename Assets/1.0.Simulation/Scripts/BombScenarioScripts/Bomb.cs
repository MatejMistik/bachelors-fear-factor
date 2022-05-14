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
    AiHealth enemyHealth;
    Rigidbody[] rigidbodies;
    BombTimer bombTimerScript;
    private float sumOfDamage;
    public float bombDamage;
    public float distanceDamageMultiplier;
    
    private float explosionDamage;

    // Start is called before the first frame update
    void Start()
    {
        countDown = bombTimer;
        enemyHealth = GameObject.Find("NewEnemy").GetComponent<AiHealth>();
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
            float distance = Vector3.Distance(transform.position, rigidbody.transform.position);
            explosionDamage = bombDamage * Mathf.Abs(10f / distance) / rigidbodies.Length;
            sumOfDamage += explosionDamage;
            Debug.Log("Explosion" + explosionDamage);
            Debug.Log(rigidbody.name);
            Debug.Log(distance);
            enemyHealth.TakeDamage(explosionDamage);
        
        
        }
        Debug.Log(sumOfDamage);
        Debug.Log(rigidbodies.Length);
        Destroy(gameObject);

        
    }
}
