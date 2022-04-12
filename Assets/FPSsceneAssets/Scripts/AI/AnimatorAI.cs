using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using TMPro;

public class AnimatorAI : MonoBehaviour
{

    [SerializeField] private float startingHealth;
    [SerializeField] private float lowHealthThreshold;
    [SerializeField] private float healthRestoreRate;

    SkinnedMeshRenderer skinnedMeshRenderer;
    [SerializeField] private Transform playerTransform;
    private NavMeshAgent agent;
    Animator animator;
    [SerializeField] float maxDistance;
    public TextMeshProUGUI nodeStateText;

    private Transform bestCoverSpot;

    private Node topNode;
    public float currentHealth;
    private NewEnemyHealth health;



    //[SerializeField] float maxTime = 1.0f;
    [SerializeField] float timer;
    [SerializeField] private float chasingRange;
    [SerializeField] private float shootingRange;
    [SerializeField] private Cover[] availableCovers;

    public int behaviorTreeLevelNumber;
    public bool weaponEquipped;



    // Start is called before the first frame update
    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        skinnedMeshRenderer = GetComponentInChildren<SkinnedMeshRenderer>();
        health = GetComponent<NewEnemyHealth>();
    }

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
            DebugMessage("Failure state");
            agent.isStopped = true;
        }

        // showing NodeState
        nodeStateText.transform.LookAt(playerTransform);
        nodeStateText.transform.rotation = Quaternion.LookRotation(nodeStateText.transform.position - playerTransform.position);



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

    private void ConstructBehaviorTreeByNumber(int levelNumber)
    {
        switch (levelNumber)
        {
            case 1:
                ConstructBehaviorTree1();
                break;
            case 2:
                ConstructBehaviorTree2();
                break;
            case 3:
                ConstructBehaviorTree3();
                break;
            default:
                break;
        }
    }

    

    // Classic tree with chasing, shooting, covering
    private void ConstructBehaviorTree1()
    {



        IsCoverAvailable coverAvaliableNode = new IsCoverAvailable(availableCovers, playerTransform, this);
        GoToCoverNode goToCoverNode = new GoToCoverNode(agent, this);
        HealthNode healthNode = new HealthNode(health);
        IsCoveredNode isCoveredNode = new IsCoveredNode(playerTransform, transform);
        ChaseNode chaseNode = new ChaseNode(playerTransform, agent, this);
        RangeNode chasingRangeNode = new RangeNode(chasingRange, playerTransform, transform);
        RangeNode shootingRangeNode = new RangeNode(shootingRange, playerTransform, transform);
        ShootingNode shootNode = new ShootingNode(agent, this, playerTransform);


        Sequence chaseSequence = new Sequence(new List<Node> { chasingRangeNode, chaseNode });
        Sequence shootSequence = new Sequence(new List<Node> { shootingRangeNode, shootNode });


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
        HealthNode healthNode = new(health);
        IsCoveredNode isCoveredNode = new(playerTransform, transform);
        IsCoveredNode isCoveredForHeal = new(playerTransform, transform);
        HealMeNode healMeNode = new(health, agent);
        ChaseNode chaseNode = new(playerTransform, agent, this);
        RangeNode chasingRangeNode = new(chasingRange, playerTransform, transform);
        RangeNode shootingRangeNode = new(shootingRange, playerTransform, transform);
        ShootingNode shootNode = new(agent, this, playerTransform);

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
        HealthNode healthNode = new HealthNode(health);
        IsCoveredNode isCoveredNode = new IsCoveredNode(playerTransform, transform);
        ChaseNode chaseNode = new ChaseNode(playerTransform, agent, this);
        RangeNode chasingRangeNode = new RangeNode(chasingRange, playerTransform, transform);
        RangeNode shootingRangeNode = new RangeNode(shootingRange, playerTransform, transform);
        ShootingNode shootNode = new ShootingNode(agent, this, playerTransform);


        Sequence chaseSequence = new Sequence(new List<Node> { chasingRangeNode, chaseNode });
        Sequence shootSequence = new Sequence(new List<Node> { shootingRangeNode, shootNode });


        Sequence goToCoverSequence = new Sequence(new List<Node> { coverAvaliableNode, goToCoverNode });
        Selector findCoverSelector = new Selector(new List<Node> { goToCoverSequence, chaseSequence });
        Selector tryToTakeCoverSelector = new Selector(new List<Node> { isCoveredNode, findCoverSelector });
        Sequence mainCoverSequence = new Sequence(new List<Node> { healthNode, tryToTakeCoverSelector });

        topNode = new Selector(new List<Node> { mainCoverSequence, shootSequence, chaseSequence });


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
