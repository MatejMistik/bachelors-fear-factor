using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using TMPro;
using SensorToolkit;

public class AnimatorAI : MonoBehaviour
{

    [SerializeField] private float startingHealth;
    [SerializeField] private float lowHealthThreshold;
    [SerializeField] private float healthRestoreRate;
    public LayerMask whatIsGround;

    SkinnedMeshRenderer skinnedMeshRenderer;
    [SerializeField] private Transform playerTransform;
    private NavMeshAgent agent;
    Animator animator;
    [SerializeField] float maxDistance;
    public TextMeshProUGUI nodeStateText;
    public RayCastWeapon weapon;
    public Sensor sensor;

    private Transform bestCoverSpot;

    private Node topNode;

    public float currentHealth;
    private NewEnemyHealth health;
    FearFactorAI fearFactorAI;

    WeaponPickup weaponPickup;


    //[SerializeField] float maxTime = 1.0f;
    [SerializeField] float timer;
    [SerializeField] private float chasingRange;

    public void EquipWeapon()
    {
        weapon = GetComponentInChildren<RayCastWeapon>();
        Debug.Log(weapon);  
    }

    [SerializeField] private float shootingRange;
    [SerializeField] private Cover[] availableCovers;

    public int behaviorTreeLevelNumber;
    public bool weaponEquipped;
    private bool walkPointSet;
    private Vector3 walkPoint;
    public int walkPointRange;
    NavigationPathForAI navigationPathForAI;



    // Start is called before the first frame update
    private void Awake()
    {
        fearFactorAI = GetComponent<FearFactorAI>();
        agent = GetComponent<NavMeshAgent>();
        skinnedMeshRenderer = GetComponentInChildren<SkinnedMeshRenderer>();
        health = GetComponent<NewEnemyHealth>();
        sensor = GetComponent<Sensor>();
        navigationPathForAI = GetComponent<NavigationPathForAI>();

    }

    void Start()
    {
        currentHealth = startingHealth;
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        weaponPickup = GameObject.Find("WeaponPickup").GetComponent<WeaponPickup>();
        ConstructBehaviorTreeByNumber(behaviorTreeLevelNumber);
        
    }

    void Update()
    {
        topNode.Evaluate();
        if (topNode.nodeState == NodeState.FAILURE)
        {
            DebugMessage("Failure state");
            agent.isStopped = true;
        }
        Debug.Log(topNode.nodeState);

        // showing NodeState
        nodeStateText.transform.LookAt(playerTransform);
        nodeStateText.transform.rotation = Quaternion.LookRotation(nodeStateText.transform.position - playerTransform.position);
        //Debug.Log(weapon);
        

        /*
         * 
         * chasing
         * 
        
        
        timer -= Time.deltaTime;
        if( timer < 0f)
        {
            float sqDistance = (PlayerPos.position - agent.destination).sqrMagnitude;
            if(sqDistance > maxDistance * maxDistance)
            {
                agent.destination = PlayerPos.position;
                
            }
            timer = 1.0f;
        }
        */

        animator.SetFloat("Speed", agent.velocity.magnitude);
        // Debug.Log("AI Velocity" + agent.velocity.magnitude);
    }

    public void ConstructBehaviorTreeByNumber(int levelNumber)
    {
        switch (levelNumber)
        {
            case 0:
                ConstructBehaviorTreeTest();
                break;
            case 1:
                ConstructBehaviorTree1();
                break;
            case 2:
                ConstructBehaviorTree2();
                break;
            case 3:
                ConstructBehaviorTree3();
                break;
            case 4:
                ConstructBehaviorTree4();
                break;
            case 5:
                ConstructBehaviorTreeTailGating();
                break;
            default:
                break;
        }
    }

  
    

    // Classic tree with chasing, covering
    private void ConstructBehaviorTree1()
    {



        IsCoverAvailable coverAvaliableNode = new IsCoverAvailable(availableCovers, playerTransform, this);
        GoToCoverNode goToCoverNode = new GoToCoverNode(agent, this);
        HealthNode healthNode = new HealthNode(health,fearFactorAI);
        IsCoveredNode isCoveredNode = new IsCoveredNode(playerTransform, transform);
        ChaseNode chaseNode = new ChaseNode(playerTransform, agent, this);
        RangeNode chasingRangeNode = new RangeNode(chasingRange, playerTransform, transform);
        WeaponEuipped weaponEuippedNode = new WeaponEuipped(this);
        RangeNode shootingRangeNode = new(shootingRange, playerTransform, transform);
        ShootingNode shootNode = new(agent, this, playerTransform, weapon);

        Sequence chaseSequence = new Sequence(new List<Node> { chasingRangeNode, chaseNode });
        Sequence shootSequence = new(new List<Node> { weaponEuippedNode ,shootingRangeNode, shootNode});

        Sequence goToCoverSequence = new Sequence(new List<Node> { coverAvaliableNode, goToCoverNode });
        Selector findCoverSelector = new Selector(new List<Node> { goToCoverSequence, chaseSequence });
        Selector tryToTakeCoverSelector = new Selector(new List<Node> { isCoveredNode, findCoverSelector });
        Sequence mainCoverSequence = new Sequence(new List<Node> { healthNode, tryToTakeCoverSelector });

        topNode = new Selector(new List<Node> { mainCoverSequence, shootSequence, chaseSequence });


    }

    // Classic Tree with healing added
    private void ConstructBehaviorTree2()
    {
        

        IsCoverAvailable coverAvaliableNode = new(availableCovers, playerTransform, this);
        GoToCoverNode goToCoverNode = new(agent, this);
        HealthNode healthNode = new(health,fearFactorAI);
        IsCoveredNode isCoveredNode = new(playerTransform, transform);
        IsCoveredNode isCoveredForHeal = new(playerTransform, transform);
        HealMeNode healMeNode = new(health, agent, fearFactorAI);
        ChaseNode chaseNode = new(playerTransform, agent, this);
        RangeNode chasingRangeNode = new(chasingRange, playerTransform, transform);
        RangeNode shootingRangeNode = new(shootingRange, playerTransform, transform);
        ShootingNode shootNode = new(agent, this, playerTransform, weapon);

        Sequence healAiSequence = new(new List<Node> { isCoveredForHeal, healMeNode });
        Sequence chaseSequence = new(new List<Node> { chasingRangeNode, chaseNode });
        Sequence shootSequence = new(new List<Node> { shootingRangeNode, shootNode });
        

        Sequence goToCoverSequence = new(new List<Node> { coverAvaliableNode, goToCoverNode});
        Selector findCoverSelector = new(new List<Node> { goToCoverSequence, chaseSequence });
        Selector tryToTakeCoverSelector = new(new List<Node> { isCoveredNode, findCoverSelector });
        Sequence mainCoverSequence = new(new List<Node> { healthNode, tryToTakeCoverSelector });

        topNode = new Selector(new List<Node> { healAiSequence, mainCoverSequence, shootSequence, chaseSequence  });


    }

    private void ConstructBehaviorTree3()
    {


        IsCoverAvailable coverAvaliableNode = new IsCoverAvailable(availableCovers, playerTransform, this);
        GoToCoverNode goToCoverNode = new GoToCoverNode(agent, this);
        HealthNode healthNode = new HealthNode(health, fearFactorAI);
        IsCoveredNode isCoveredNode = new IsCoveredNode(playerTransform, transform);
        ChaseNode chaseNode = new ChaseNode(playerTransform, agent, this);
        RangeNode chasingRangeNode = new RangeNode(chasingRange, playerTransform, transform);
        RangeNode shootingRangeNode = new RangeNode(shootingRange, playerTransform, transform);
        ShootingNode shootNode = new ShootingNode(agent, this, playerTransform, weapon);


        Sequence chaseSequence = new Sequence(new List<Node> { chasingRangeNode, chaseNode });
        Sequence shootSequence = new Sequence(new List<Node> { shootingRangeNode, shootNode });


        Sequence goToCoverSequence = new Sequence(new List<Node> { coverAvaliableNode, goToCoverNode });
        Selector findCoverSelector = new Selector(new List<Node> { goToCoverSequence, chaseSequence });
        Selector tryToTakeCoverSelector = new Selector(new List<Node> { isCoveredNode, findCoverSelector });
        Sequence mainCoverSequence = new Sequence(new List<Node> { healthNode, tryToTakeCoverSelector });

        topNode = new Selector(new List<Node> { mainCoverSequence, shootSequence, chaseSequence });


    }

    private void ConstructBehaviorTree4()
    {



        IsCoverAvailable coverAvaliableNode = new IsCoverAvailable(availableCovers, playerTransform, this);
        GoToCoverNode goToCoverNode = new GoToCoverNode(agent, this);
        HealthNode healthNode = new HealthNode(health, fearFactorAI);
        IsCoveredNode isCoveredNode = new IsCoveredNode(playerTransform, transform);
        ChaseNode chaseNode = new ChaseNode(playerTransform, agent, this);
        RangeNode chasingRangeNode = new RangeNode(chasingRange, playerTransform, transform);
        
        RangeNode shootingRangeNode = new(shootingRange, playerTransform, transform);
        ShootingNode shootNode = new(agent, this, playerTransform, weapon);
        WeaponEuipped weaponEuippedNode = new(this);
        FindWeaponsAvailableNode findWeaponNode = new(weaponPickup,agent,weaponPickup.transform,this);

        Sequence findWeaponSequence = new Sequence(new List<Node> { weaponEuippedNode, findWeaponNode });
        Sequence chaseSequence = new Sequence(new List<Node> { chasingRangeNode, chaseNode });
        Sequence shootSequence = new(new List<Node> { shootingRangeNode, shootNode });

        Sequence goToCoverSequence = new Sequence(new List<Node> { coverAvaliableNode, goToCoverNode });
        Selector findCoverSelector = new Selector(new List<Node> { goToCoverSequence, chaseSequence });
        Selector tryToTakeCoverSelector = new Selector(new List<Node> { isCoveredNode, findCoverSelector });
        Sequence mainCoverSequence = new Sequence(new List<Node> { healthNode, tryToTakeCoverSelector });

        topNode = new Selector(new List<Node> { findWeaponSequence, mainCoverSequence, shootSequence, chaseSequence });


    }

    private void ConstructBehaviorTreeTest()
    {

        WeaponEuipped weaponEuippedNode = new(this);
        FindWeaponsAvailableNode findWeaponNode = new(weaponPickup, agent, weaponPickup.transform, this);

        Sequence findWeaponSequence = new Sequence(new List<Node> { weaponEuippedNode, findWeaponNode });

        topNode = new Selector(new List<Node> { findWeaponSequence});


    }

    private void ConstructBehaviorTreeTailGating()
    {

        IsCoverAvailable coverAvaliableNode = new(availableCovers, playerTransform, this);
        GoToCoverNode goToCoverNode = new(agent, this);
        HealthNode healthNode = new(health,fearFactorAI);
        IsCoveredNode isCoveredNode = new(playerTransform, transform);
        IsCoveredNode isCoveredForHeal = new(playerTransform, transform);
        HealMeNode healMeNode = new(health, agent,fearFactorAI);

        EnemyInSigthNode enemyInSigthNode = new(sensor);
        ObserveWhatIsTheProblemNode observeWhatIsTheProblemNode = new(agent, playerTransform, fearFactorAI);
        RunAwayNode runAwayNode = new(this,agent);
        PatrollingNode patrollingNode = new(navigationPathForAI, agent);



        Sequence healAiSequence = new(new List<Node> { isCoveredForHeal, healMeNode });
        Sequence goToCoverSequence = new(new List<Node> { coverAvaliableNode, goToCoverNode });
        Selector findCoverSelector = new(new List<Node> { goToCoverSequence });
        Selector tryToTakeCoverSelector = new(new List<Node> { isCoveredNode, findCoverSelector });
        Sequence mainCoverSequence = new(new List<Node> { healthNode, tryToTakeCoverSelector });
        Sequence TailgatingSeqeunce = new Sequence(new List<Node> { enemyInSigthNode, observeWhatIsTheProblemNode, runAwayNode });

        topNode = new Selector(new List<Node> { healAiSequence, mainCoverSequence, TailgatingSeqeunce, patrollingNode  });
    }

    public void RunAwayFromPlayer()
    {
        
        
        Vector3 dirToPlayer = transform.position - playerTransform.transform.position;
        Vector3 newPos = transform.position + dirToPlayer;
        agent.SetDestination(newPos);

    }

    public void Patrolling()
    {
        
        if (!walkPointSet) SearchWalkPoint();

        if (walkPointSet)
            agent.SetDestination(walkPoint);

        Vector3 distanceToWalkPoint = transform.position - walkPoint;

        //Walkpoint reached
        if (distanceToWalkPoint.magnitude < 1f)
            walkPointSet = false;
    }
    public void SearchWalkPoint()
    {
        //Calculate random point in range
        float randomZ = UnityEngine.Random.Range(-walkPointRange, walkPointRange);
        float randomX = UnityEngine.Random.Range(-walkPointRange, walkPointRange);

        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        if (Physics.Raycast(walkPoint, -transform.up, 2f, whatIsGround))
            walkPointSet = true;
    }





    // Update is called once per frame

    public void DebugMessage(string message)
    {
        //Debug.Log("Node" + message);
        return;
    }
    public void SetBestCoverSpot(Transform bestCoverSpot)
    {
        this.bestCoverSpot = bestCoverSpot;
    }

    public Transform GetBestCoverSpot()
    {
        return bestCoverSpot;
    }


}
