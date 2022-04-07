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
    [SerializeField] float maxDistance = 1.0f;
    private Material material;
    public TextMeshProUGUI nodeStateText;

    private Transform bestCoverSpot;

    private Node topNode;
    public float currentHealth;
    private NewEnemyHealth health;



    //[SerializeField] float maxTime = 1.0f;
    [SerializeField] float timer = 0f;
    [SerializeField] private float chasingRange;
    [SerializeField] private float shootingRange;
    [SerializeField] private Cover[] availableCovers;
    

    
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
        ConstructBehaviorTree();
    }

    private void ConstructBehaviorTree()
    {
        IsCoverAvailable coverAvaliableNode = new IsCoverAvailable(availableCovers, playerTransform, this);
        GoToCoverNode goToCoverNode = new GoToCoverNode(agent, this);
        HealthNode healthNode = new HealthNode(health);
        IsCoveredNode isCoveredNode = new IsCoveredNode(playerTransform, transform);
        IsCoveredNode isCoveredForHeal = new IsCoveredNode(playerTransform, transform);
        HealMeNode healMeNode = new HealMeNode(health, agent);
        ChaseNode chaseNode = new ChaseNode(playerTransform, agent, this);
        RangeNode chasingRangeNode = new RangeNode(chasingRange, playerTransform, transform);
        RangeNode shootingRangeNode = new RangeNode(shootingRange, playerTransform, transform);
        ShootingNode shootNode = new ShootingNode(agent, this, playerTransform);

        Sequence healAiSequence = new Sequence(new List<Node> { isCoveredForHeal, healMeNode });
        Sequence chaseSequence = new Sequence(new List<Node> { chasingRangeNode, chaseNode });
        Sequence shootSequence = new Sequence(new List<Node> { shootingRangeNode, shootNode });
        

        Sequence goToCoverSequence = new Sequence(new List<Node> { coverAvaliableNode, goToCoverNode});
        Selector findCoverSelector = new Selector(new List<Node> { goToCoverSequence, chaseSequence });
        Selector tryToTakeCoverSelector = new Selector(new List<Node> { isCoveredNode, findCoverSelector });
        Sequence mainCoverSequence = new Sequence(new List<Node> { healthNode, tryToTakeCoverSelector });

        topNode = new Selector(new List<Node> { healAiSequence, mainCoverSequence, shootSequence, chaseSequence  });


    }

    // Update is called once per frame
    void Update()
    {
        topNode.Evaluate();
        if(topNode.nodeState == NodeState.FAILURE)
        {
            DebugMessage("Failure state");
            agent.isStopped = true;
        }

        // showing NodeState
        nodeStateText.transform.LookAt(playerTransform);
        nodeStateText.transform.rotation = Quaternion.LookRotation(nodeStateText.transform.position - playerTransform.position);



        /*
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
