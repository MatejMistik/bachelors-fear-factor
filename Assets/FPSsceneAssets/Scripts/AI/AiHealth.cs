using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

public class AiHealth : MonoBehaviour
{

    public static bool firstkilled;
    public static int numberOfAgentsKilled;

    public GameObject HealhtBarUI;
    public Slider slider;
    public Transform player;
    Ragdoll ragdoll;
    SkinnedMeshRenderer skinnedMeshRenderer;
    private NavMeshAgent agent;
    



    // Start is called before the first frame update
    public float maxHealth;
    [SerializeField] private float healthRestorerate;
    [HideInInspector]
    public float newEnemycurrentHealth;
    public float treshold;

    public float blinkDuration;
    public float blinkIntensity;
    float blinkTimer;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        ragdoll = GetComponent<Ragdoll>();
        newEnemycurrentHealth = maxHealth;
        skinnedMeshRenderer = GetComponentInChildren<SkinnedMeshRenderer>();

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
        skinnedMeshRenderer.material.color = Color.white * intensity;

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

    private void Die()
    {
        firstkilled = true;
        ragdoll.ActivateRagDoll();
        this.GetComponent<AnimatorAI>().enabled = false;
        agent.isStopped = true;
        HealhtBarUI.SetActive(false);
        skinnedMeshRenderer.material.color = Color.white;
        this.GetComponent<AiHealth>().enabled = false;
        numberOfAgentsKilled++;
    }

    public void Restore(float amount)
    {

        newEnemycurrentHealth += amount;
    }



}
