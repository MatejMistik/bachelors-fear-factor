using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using TMPro;
using SensorToolkit;

/*
***************************************************************************************
*
*	Title: AI in Unity Turorial. Behavior Trees.
*	Author: GameDevChef
*   Date: 22.05., 2022
*	Code version: 1.0
*	Availability: https://github.com/GameDevChef/BehaviourTrees
*	Functions used : ConstructBehaviorTree1(), SetBestCoverSpot(Transform bestCoverSpot), GetBestCoverSpot() 
*	
***************************************************************************************/

public class AiTreeConstructor : MonoBehaviour
{
    public bool weaponPicked;
    [SerializeField] private float startingHealth;
    [SerializeField] private float lowHealthThreshold;
    [SerializeField] private float healthRestoreRate;
    public LayerMask whatIsGround;

    SkinnedMeshRenderer skinnedMeshRenderer;
    [SerializeField] private Transform playerTransform;
    private NavMeshAgent agent;
    Animator animator;
    public TextMeshProUGUI nodeStateText;
    public RayCastWeapon weapon;
    public Sensor sensor;

    private Transform bestCoverSpot;

    private Node topNode;

    public float currentHealth;
    private AiHealth health;
    FearFactorAI fearFactorAI;

    WeaponPickup weaponPickup;
    AlliesAround alliesAround;


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
    public int walkPointRange;
    NavigationPathForAI navigationPathForAI;
    ElevatorCheck elevatorCheck;


    private void Awake()
    {
        fearFactorAI = GetComponent<FearFactorAI>();
        agent = GetComponent<NavMeshAgent>();
        skinnedMeshRenderer = GetComponentInChildren<SkinnedMeshRenderer>();
        health = GetComponent<AiHealth>();
        sensor = GetComponent<Sensor>();
        navigationPathForAI = GetComponent<NavigationPathForAI>();
        elevatorCheck = GetComponent<ElevatorCheck>();
        alliesAround = GetComponent<AlliesAround>();

    }

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = startingHealth;
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>(); 
        ConstructBehaviorTreeByNumber(behaviorTreeLevelNumber);
        
    }

    void Update()
    {
        topNode.Evaluate();
        if (topNode.nodeState == NodeState.FAILURE)
        {
            agent.isStopped = true;
        }
        

        // showing NodeState
        nodeStateText.transform.LookAt(playerTransform);
        nodeStateText.transform.rotation = Quaternion.LookRotation(nodeStateText.transform.position - playerTransform.position);
        //Debug.Log(weapon);
        


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
                ConstructBehaviorTreePhone();
                break;
            case 5:
                ConstructBehaviorTreeTailGating();
                break;
            case 6:
                ConstructBehaviorTreeBomb();
                break;
            case 7:
                ConstructBehaviorTreeElevator();
                break;
            case 8:
                ConstructBehaviorTreeShotgun();
                break;
            case 9:
                ConstructBehaviorTreeBladeRunner();
                break;
            default:
                break;
        }
    }

    private void ConstructBehaviorTreeShotgun()
    {
        weaponPickup = GameObject.Find("WeaponPickup").GetComponent<WeaponPickup>();

        IsCoverAvailable coverAvaliableNode = new IsCoverAvailable(availableCovers, playerTransform, this);
        GoToCoverNode goToCoverNode = new GoToCoverNode(agent, this);
        HealthNode healthNode = new HealthNode(health, fearFactorAI);
        IsCoveredNode isCoveredNode = new IsCoveredNode(playerTransform, transform);
        ChaseNode chaseNode = new ChaseNode(playerTransform, agent);
        RangeNode chasingRangeNode = new RangeNode(chasingRange, playerTransform, transform);

        RangeNode shootingRangeNode = new(shootingRange, playerTransform, transform);
        ShootingNode shootNode = new(agent, this, playerTransform, weapon);
        WeaponEuipped weaponEuippedNode = new(this);
        FindWeaponsAvailableNode findWeaponNode = new(agent, weaponPickup.transform, this);

        Sequence findWeaponSequence = new Sequence(new List<Node> { weaponEuippedNode, findWeaponNode });
        Sequence chaseSequence = new Sequence(new List<Node> { chasingRangeNode, chaseNode });
        Sequence shootSequence = new(new List<Node> { shootingRangeNode, shootNode });

        Sequence goToCoverSequence = new Sequence(new List<Node> { coverAvaliableNode, goToCoverNode });
        Selector findCoverSelector = new Selector(new List<Node> { goToCoverSequence, chaseSequence });
        Selector tryToTakeCoverSelector = new Selector(new List<Node> { isCoveredNode, findCoverSelector });
        Sequence mainCoverSequence = new Sequence(new List<Node> { healthNode, tryToTakeCoverSelector });

        topNode = new Selector(new List<Node> { findWeaponSequence, mainCoverSequence, shootSequence, chaseSequence });
    }

    private void ConstructBehaviorTreeBladeRunner()
    {

        RangeNode rangeNode = new(100f, playerTransform, transform);
        ChaseNode chaseNode = new(playerTransform, agent);

        AreDeadAlliesNearby areDeadAlliesNearbyNode = new(sensor);
        WasCorpseChecked wasCorpseCheckedNode = new(alliesAround);
        CheckOnCorpseNode checkOnCorpseNode = new(agent, alliesAround);

        KilledNextToMeNode killedNextToMeNode = new(alliesAround, fearFactorAI, agent, this);


        Sequence chaseSequence = new Sequence(new List<Node> { rangeNode, chaseNode });
        Sequence CheckOnCorpse = new Sequence(new List<Node> { areDeadAlliesNearbyNode, wasCorpseCheckedNode, checkOnCorpseNode });

        topNode = new Selector(new List<Node> { killedNextToMeNode, CheckOnCorpse, chaseSequence });

    }

    private void ConstructBehaviorTreeTest()
    {


        RangeNode rangeNode = new(100f, playerTransform, transform);
        ChaseNode chaseNode = new(playerTransform, agent);
        
        AreDeadAlliesNearby areDeadAlliesNearbyNode = new(sensor);
        WasCorpseChecked wasCorpseCheckedNode = new(alliesAround);
        CheckOnCorpseNode checkOnCorpseNode = new(agent, alliesAround);

        KilledNextToMeNode killedNextToMeNode = new(alliesAround, fearFactorAI, agent,this);
        
        Sequence chaseSequence = new Sequence(new List<Node> { rangeNode, chaseNode });
        Sequence CheckOnCorpse = new Sequence(new List<Node> { areDeadAlliesNearbyNode, wasCorpseCheckedNode, checkOnCorpseNode });


        topNode = new Selector(new List<Node> { killedNextToMeNode ,CheckOnCorpse, chaseSequence });


    }

    // Classic tree with chasing, covering
    private void ConstructBehaviorTree1()
    {
        IsCoverAvailable coverAvaliableNode = new IsCoverAvailable(availableCovers, playerTransform, this);
        GoToCoverNode goToCoverNode = new GoToCoverNode(agent, this);
        HealthNode healthNode = new HealthNode(health,fearFactorAI);
        IsCoveredNode isCoveredNode = new IsCoveredNode(playerTransform, transform);
        ChaseNode chaseNode = new ChaseNode(playerTransform, agent);
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
        ChaseNode chaseNode = new(playerTransform, agent);
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
        ChaseNode chaseNode = new ChaseNode(playerTransform, agent);
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

    private void ConstructBehaviorTreePhone()
    {

        weaponPickup = GameObject.Find("WeaponPickup").GetComponent<WeaponPickup>();

        IsCoverAvailable coverAvaliableNode = new IsCoverAvailable(availableCovers, playerTransform, this);
        GoToCoverNode goToCoverNode = new GoToCoverNode(agent, this);
        HealthNode healthNode = new HealthNode(health, fearFactorAI);
        IsCoveredNode isCoveredNode = new IsCoveredNode(playerTransform, transform);
        ChaseNode chaseNode = new ChaseNode(playerTransform, agent);
        RangeNode chasingRangeNode = new RangeNode(chasingRange, playerTransform, transform);
        
        RangeNode shootingRangeNode = new(shootingRange, playerTransform, transform);
        ShootingNode shootNode = new(agent, this, playerTransform, weapon);
        WeaponEuipped weaponEuippedNode = new(this);
        FindWeaponsAvailableNode findWeaponNode = new(agent,weaponPickup.transform,this);

        Sequence findWeaponSequence = new Sequence(new List<Node> { weaponEuippedNode, findWeaponNode });
        Sequence chaseSequence = new Sequence(new List<Node> { chasingRangeNode, chaseNode });
        Sequence shootSequence = new(new List<Node> { shootingRangeNode, shootNode });

        Sequence goToCoverSequence = new Sequence(new List<Node> { coverAvaliableNode, goToCoverNode });
        Selector findCoverSelector = new Selector(new List<Node> { goToCoverSequence, chaseSequence });
        Selector tryToTakeCoverSelector = new Selector(new List<Node> { isCoveredNode, findCoverSelector });
        Sequence mainCoverSequence = new Sequence(new List<Node> { healthNode, tryToTakeCoverSelector });

        topNode = new Selector(new List<Node> { findWeaponSequence, mainCoverSequence, shootSequence, chaseSequence });

    }

    

    private void ConstructBehaviorTreeTailGating()
    {

        IsCoverAvailable coverAvaliableNode = new(availableCovers, playerTransform, this);
        GoToCoverNode goToCoverNode = new(agent, this);
        HealthNode healthNode = new(health,fearFactorAI);
        IsCoveredNode isCoveredNode = new(playerTransform, transform);
        IsCoveredNode isCoveredForHeal = new(playerTransform, transform);
        HealMeNode healMeNode = new(health, agent,fearFactorAI);

        EnemyInSigthNode enemyInSigthNode = new(sensor,agent, fearFactorAI);
        ObserveWhatIsTheProblemNode observeWhatIsTheProblemNode = new(agent, playerTransform, fearFactorAI, this);
        RunAwayNode runAwayNode = new(this,agent,sensor,fearFactorAI,playerTransform);
        CanPatrolNode canPatrolNode = new(this, sensor, agent);
        PatrollingNode patrollingNode = new(navigationPathForAI, agent,this,fearFactorAI);



        Sequence patrollingSeqeunce= new(new List<Node> { canPatrolNode, patrollingNode});
        Sequence healAiSequence = new(new List<Node> { isCoveredForHeal, healMeNode });
        Sequence goToCoverSequence = new(new List<Node> { coverAvaliableNode, goToCoverNode });
        Selector tryToTakeCoverSelector = new(new List<Node> { isCoveredNode, goToCoverSequence });
        Sequence mainCoverSequence = new(new List<Node> { healthNode, tryToTakeCoverSelector });
        Sequence TailgatingSeqeunce = new Sequence(new List<Node> { enemyInSigthNode, observeWhatIsTheProblemNode, runAwayNode });

        topNode = new Selector(new List<Node> { healAiSequence, mainCoverSequence, TailgatingSeqeunce, patrollingSeqeunce  });
    }

    private void ConstructBehaviorTreeBomb()
    {
        IsCoverAvailable coverAvaliableNode = new(availableCovers, playerTransform, this);
        GoToCoverNode goToCoverNode = new(agent, this);
        HealthNode healthNode = new(health, fearFactorAI);
        IsCoveredNode isCoveredNode = new(playerTransform, transform);
        IsCoveredNode isCoveredForHeal = new(playerTransform, transform);
        HealMeNode healMeNode = new(health, agent, fearFactorAI);

        EnemyInSigthNode enemyInSigthNode = new(sensor,agent,fearFactorAI);
        ObserveWhatIsTheProblemNode observeWhatIsTheProblemNode = new(agent, playerTransform, fearFactorAI, this);
        RunAwayNode runAwayNode = new(this, agent, sensor, fearFactorAI,playerTransform);
        PatrollingNode patrollingNode = new(navigationPathForAI, agent, this,fearFactorAI);



        Sequence healAiSequence = new(new List<Node> { isCoveredForHeal, healMeNode });
        Sequence goToCoverSequence = new(new List<Node> { coverAvaliableNode, goToCoverNode });
        Selector tryToTakeCoverSelector = new(new List<Node> { isCoveredNode, goToCoverSequence });
        Sequence mainCoverSequence = new(new List<Node> { healthNode, tryToTakeCoverSelector });
        Sequence TailgatingSeqeunce = new Sequence(new List<Node> { enemyInSigthNode, observeWhatIsTheProblemNode, runAwayNode });

        topNode = new Selector(new List<Node> { healAiSequence, mainCoverSequence, TailgatingSeqeunce, patrollingNode });
    }

    private void ConstructBehaviorTreeElevator()
    {
        IsCoverAvailable coverAvaliableNode = new(availableCovers, playerTransform, this);
        GoToCoverNode goToCoverNode = new(agent, this);
        HealthNode healthNode = new(health, fearFactorAI);
        IsCoveredNode isCoveredNode = new(playerTransform, transform);

        EnemyInSigthNode enemyInSigthNode = new(sensor, agent, fearFactorAI);
        ObserveWhatIsTheProblemNode observeWhatIsTheProblemNode = new(agent, playerTransform, fearFactorAI, this);
        RunAwayNode runAwayNode = new(this, agent, sensor, fearFactorAI, playerTransform);

        IsInElevatorNode isInElevatorNode = new(agent, elevatorCheck);
        IsElelevatorOpenedNode isElelevatorOpenedNode = new(elevatorCheck);
        GetToElelevatorNode getToElelevatorNode = new(agent, elevatorCheck);
        CloseTheDoorNode closeTheDoorNode = new(elevatorCheck);
        OpenTheDoorNode openTheDoorNode = new(elevatorCheck);

        Sequence closeTheDoorSequence = new(new List<Node> { isInElevatorNode, closeTheDoorNode});
        Sequence goToElevatorSeqeunce = new(new List<Node> { isElelevatorOpenedNode, getToElelevatorNode});
        Selector elevatorSelector = new(new List<Node> { closeTheDoorSequence, isInElevatorNode, goToElevatorSeqeunce });
        Sequence goToCoverSequence = new(new List<Node> { coverAvaliableNode, goToCoverNode });
        Selector tryToTakeCoverSelector = new(new List<Node> { isCoveredNode, goToCoverSequence });
        Sequence mainCoverSequence = new(new List<Node> { healthNode, tryToTakeCoverSelector });
        Sequence TailgatingSeqeunce = new Sequence(new List<Node> { enemyInSigthNode, observeWhatIsTheProblemNode, openTheDoorNode, runAwayNode });

        topNode = new Selector(new List<Node> { TailgatingSeqeunce, mainCoverSequence,  elevatorSelector });
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
