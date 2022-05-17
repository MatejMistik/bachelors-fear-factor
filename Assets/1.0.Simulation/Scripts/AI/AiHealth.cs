using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

public class AiHealth : MonoBehaviour
{
    AiKilled aiKilled;
    public static bool firstKilled;
    public static int numberOfAgentsKilled;

    //public CapsuleCollider colliderForSensor;
    public GameObject HealhtBarUI;
    public Slider slider;
    public Transform player;
    Ragdoll ragdoll;
    SkinnedMeshRenderer[] skinnedMeshRenderer;
    private NavMeshAgent agent;
    public bool agentIsDead = false;
    

    // Start is called before the first frame update
    public float maxHealth;
    [SerializeField] private float healthRestorerate;
    [HideInInspector]
    public float newEnemycurrentHealth;
    public float treshold;

    public float blinkDuration;
    public float blinkIntensity;
    float blinkTimer;
    public bool healthRestored;
    FearFactorAI fearFactorAI;
    AiTreeConstructor aiTreeConstructor;

    [SerializeField] float timeToResetHealing ;

    private float _elevatorHealthTreshold = 99f;

    public float elevatorHealthTreshold
    {
        get { return _elevatorHealthTreshold; }
        set { _elevatorHealthTreshold = value; }
    }

    void Start()
    {
        aiTreeConstructor = GetComponent<AiTreeConstructor>();
        fearFactorAI = GetComponent<FearFactorAI>();
        agent = GetComponent<NavMeshAgent>();
        ragdoll = GetComponent<Ragdoll>();
        newEnemycurrentHealth = maxHealth;
        skinnedMeshRenderer = GetComponentsInChildren<SkinnedMeshRenderer>();
        aiKilled = GetComponent<AiKilled>();

        var rigidBodies = GetComponentsInChildren<Rigidbody>();
        foreach (var rigidbody in rigidBodies)
        {
            Hitbox hitbox = rigidbody.gameObject.AddComponent<Hitbox>();
            hitbox.aiHealth = this;
        }
    }

    void Update()
    {

        slider.value = newEnemycurrentHealth / maxHealth;
        slider.transform.LookAt(player);
        

        // blinking
        blinkTimer -= Time.deltaTime;
        float lerp = Mathf.Clamp01(blinkTimer / blinkDuration);
        float intensity = (lerp * blinkIntensity) + 1.0f;
        foreach(SkinnedMeshRenderer partOfBody in skinnedMeshRenderer)
        {
            partOfBody.material.color = Color.white * intensity;
        }
        if (AiConstraintsConfig.male)
        {
            foreach (SkinnedMeshRenderer partOfBody in skinnedMeshRenderer)
            {
                partOfBody.material.color = Color.blue;
            }
        }
        else if(AiConstraintsConfig.female)
        {
            foreach (SkinnedMeshRenderer partOfBody in skinnedMeshRenderer)
            {
                partOfBody.material.color = Color.red;
            }
        } 


        /*
        if (KillsInTimeManager.bonusActivated && AlliesAround.deadEnemies.Length !=0)
        {
            Debug.Log("All Killed");
            Die();
        }
        */

    }


    public void TakeDamage(float amount)
    {
        newEnemycurrentHealth -= amount;
        slider.value = this.newEnemycurrentHealth;
        if (newEnemycurrentHealth <= 0.0f)
        {
            Die();
        }
        //Debug.Log("health" + NewEnemycurrentHealth);
        blinkTimer = blinkDuration;
    }


    public void Die()
    {
        //colliderForSensor.enabled = false;
        if (aiKilled)
        {
            aiKilled.aiKilled = true;
        }
        else { 
            Debug.Log(aiKilled + "Not attached to Enemy GameObject"); 
        }

        gameObject.tag = "DeadEnemy";
        agentIsDead = true;
        firstKilled = true;
        ragdoll.ActivateRagDoll();
        aiTreeConstructor.enabled = false;
        agent.isStopped = true;
        HealhtBarUI.SetActive(false);
        fearFactorAI.enabled = false;
        if (AiConstraintsConfig.male)
        {
            foreach (SkinnedMeshRenderer partOfBody in skinnedMeshRenderer)
            {
                partOfBody.material.color = Color.blue;
            }
        }
        else if (AiConstraintsConfig.female)
        {
            foreach (SkinnedMeshRenderer partOfBody in skinnedMeshRenderer)
            {
                partOfBody.material.color = Color.red;
            }
        }
        else
        {
            foreach (SkinnedMeshRenderer partOfBody in skinnedMeshRenderer)
            {
                partOfBody.material.color = Color.white;
            }
        }
        // disable this script
        enabled = false;
        numberOfAgentsKilled++;
        
    }

    public void Restore(float amount)
    {
        newEnemycurrentHealth += amount;
        if(newEnemycurrentHealth <= maxHealth)
        {
            
            Invoke(nameof(HealthRestoredReset),timeToResetHealing);
        }
        else
        {
            healthRestored = true;
        }

    }

    private void HealthRestoredReset()
    {
        healthRestored = false;
    }




}
