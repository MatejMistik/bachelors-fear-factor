using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    public NavMeshAgent agent;

    public Transform player;

    public LayerMask whatIsGround, whatIsPlayer;

    public GameObject Capsule;
    private Renderer EnemyRender;
    private Color color;
    public static string enemyState;

    [SerializeField] float setHealth;
    public static float health ;
    public static float maxHealth;
    public float projectileSpeed;
    public static bool flee;
    public static bool NTL;

    //Patroling
    public Vector3 walkPoint;
    bool walkPointSet;
    public float walkPointRange;

    //Attacking
    public float timeBetweenAttacks;
    bool alreadyAttacked;
    public GameObject projectile;
    public static bool takingDamage;
    public static bool frenzyActive;
    public static float DistanceOfEnemy;

    //States
    public float sightRange, attackRange;
    public bool playerInSightRange, playerInAttackRange;
    Vector3 hidePoint;

    private void Awake()
    {
        health = setHealth;
        maxHealth = health;
        player = GameObject.Find("FirstPersonPlayer").transform;
        agent = GetComponent<NavMeshAgent>();
        EnemyRender = Capsule.GetComponent<Renderer>();
    }

    private void Update()
    {
        //Check for sight and attack range
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);

        DistanceOfEnemy = Vector3.Distance(player.transform.position, transform.position);

        if (NTL)
        {
            NothingToLoose();
            
        }
        if (health > 0.95 * maxHealth)
        {
            flee = false;
            NTL = false;           
        }
        if (health <= 0.5*maxHealth && playerInSightRange && NTL !=true)
        {
            flee = true;
            RunAwayFromPlayer();
        }
        if (health < 0.75 * maxHealth && flee != true && NTL !=true) {
            FrenzyAttack();
            timeBetweenAttacks = 0.4f;
            projectileSpeed = 50f;
            EnemyRender.material.SetColor("_Color",Color.yellow);
            frenzyActive = true;  
        }else
        {
            frenzyActive = false;
        }
        if (health > 0.7 * maxHealth && flee != true && NTL != true)
        {
            ClassicState();
        }
        
        
    }

    private void ClassicState()
    {
        timeBetweenAttacks = 2f;
        EnemyRender.material.SetColor("_Color", Color.white);
        projectileSpeed = 20f;
        agent.speed = 12f;
        if (!playerInSightRange && !playerInAttackRange) Patroling();

        if (playerInSightRange && !playerInAttackRange) ChasePlayer();
        if (playerInAttackRange && playerInSightRange) AttackPlayer();
    }

    private void NothingToLoose()
    {
        EnemyRender.material.SetColor("_Color", Color.red);
        enemyState = "NotHingToLoose";
        agent.speed = 50f;
        projectileSpeed = 80f;
        flee = false;
        if (!playerInAttackRange) ChasePlayer();
        if (playerInAttackRange && playerInSightRange) AttackPlayer();
    }

    private void RunAwayFromPlayer()
    {
        EnemyRender.material.SetColor("_Color", Color.cyan);
        enemyState = "Fleeing";
        Vector3 dirToPlayer = transform.position - player.transform.position;
        Vector3 newPos = transform.position + dirToPlayer;

        agent.SetDestination(newPos);

    }

    private void FrenzyAttack()
    {
        enemyState = "Frenzy";
        //hidePoint = new Vector3(player.position.x,player.position.y,player.position.z-10);
        agent.SetDestination(transform.position);

        transform.LookAt(player);

        if (!alreadyAttacked)
        {
            ///Attack code here
            ///


            GameObject fireBallClone = Instantiate(projectile, transform.position, Quaternion.identity);
            Rigidbody rb = fireBallClone.GetComponent<Rigidbody>();
            rb.AddForce(transform.forward * projectileSpeed, ForceMode.Impulse);
            Destroy(fireBallClone, 4);
            ///End of attack code

            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }
    }

    private void Patroling()
    {
        EnemyRender.material.SetColor("_Color", Color.white);
        enemyState = "Patroling";
        if (!walkPointSet) SearchWalkPoint();

        if (walkPointSet)
            agent.SetDestination(walkPoint);

        Vector3 distanceToWalkPoint = transform.position - walkPoint;

        //Walkpoint reached
        if (distanceToWalkPoint.magnitude < 1f)
            walkPointSet = false;
    }
    private void SearchWalkPoint()
    {
        //Calculate random point in range
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);

        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        if (Physics.Raycast(walkPoint, -transform.up, 2f, whatIsGround))
            walkPointSet = true;
    }

    private void ChasePlayer()
    {
        EnemyRender.material.SetColor("_Color", Color.blue);
        enemyState = "Chasing";
        agent.SetDestination(player.position);
    }

    private void AttackPlayer()
    {
        EnemyRender.material.SetColor("_Color", Color.red);
        enemyState = "Attacking";
        //Make sure enemy doesn't move
        agent.SetDestination(transform.position);

        transform.LookAt(player);

        if (!alreadyAttacked)
        {
            ///Attack code here
            ///


            GameObject fireBallClone = Instantiate(projectile, transform.position, Quaternion.identity);
            Rigidbody rb = fireBallClone.GetComponent<Rigidbody>();
            rb.AddForce(transform.forward * projectileSpeed, ForceMode.Impulse);
            Destroy(fireBallClone, 4);
            ///End of attack code

            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }
    }


    private void ResetAttack()
    {
        alreadyAttacked = false;
    }

    public void TakeDamage(float damage)
    {
        takingDamage = true;
       // Debug.Log("TakeDamage() takingDamage = " + takingDamage);
        health -= damage;
        Invoke(nameof(TimeTillHealAgain), 0.5f);
        if (health <= 0) Destroy(gameObject, 0.1f); 
    }
    private void TimeTillHealAgain()
    {
        takingDamage = false;
        //Debug.Log("TimeTillHealAgain() takingDamage = "+ takingDamage);
    }

}
