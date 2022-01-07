using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemyhealth : MonoBehaviour
{
    // Start is called before the first frame update
    private float maxHealth;
    public static bool needHealing = true;
    private bool LowHp = false;
    private bool healAfterInterrupt = false;
    public static float enemyCurrentHealth;
    public static float NTLBasedOnHealthProbability;

    public GameObject HealhtBarUI;
    public Slider slider;
    public Transform player;

    void Start()
    {
        maxHealth = EnemyAI.health;
    }

    // Update is called once per frame
    void Update()
    {
        slider.value = CalculateHealth();
        enemyCurrentHealth = slider.value;
        slider.transform.LookAt(player);
        //NTLBasedOnHealthProbability = 1/(enemyCurrentHealth); // function 1/x returns on max probability 1 and its getting higher when hp is getting lower. We need this values {1.0 , +inf}
        

        if (enemyCurrentHealth <= (0.5f * maxHealth)/100 && LowHp == false)
        {
            LowHp = true;
            Debug.Log("InLow HP ");
        }
        if ( (LowHp && needHealing) || (healAfterInterrupt) )
        {
            Debug.Log("Invoking HP ");
            needHealing = false;
            healAfterInterrupt = false;
            Invoke(nameof(HealEnemy), 10f);
        }

    }
    float CalculateHealth()
    {
        return EnemyAI.health / maxHealth;
    }

    void HealEnemy()
    {
        if (EnemyAI.takingDamage) 
        {
            healAfterInterrupt = true;
            return;
        }
        
        Debug.Log("HealEnemy() is called ");
        if(EnemyAI.health <= maxHealth  && needHealing == false)
        {
            EnemyAI.health += 0.1f * maxHealth;
            Invoke(nameof(HealEnemy), 1f);
            return;
        }else
        {
            needHealing = true;
            LowHp = false;
        }
        


    }

}
