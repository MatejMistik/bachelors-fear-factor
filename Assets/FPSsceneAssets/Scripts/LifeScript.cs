using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LifeScript : MonoBehaviour
{
    private float maxHealth;
    public static bool needHealing = true;
    private bool LowHp = false;
    private bool healAfterInterrupt = false;
    public static float health = 0f;
    public static bool takingDamage = false;

    public Slider slider;
    [SerializeField] float setHealth = 100;
    [SerializeField] int LivesCount;
    public static int Lives = 0;
    public TextMeshProUGUI LiveText;


    private void Start()
    {
        health = setHealth;
        maxHealth = health;
        Lives = LivesCount;
    }

    // Update is called once per frame
    void Update()
    {
        
        LiveText.SetText("HP" + health +"/" + maxHealth );
        slider.value = CalculateHealth();

        if (slider.value <= (0.45 * maxHealth) / 100 && LowHp == false)
        {
            LowHp = true;
            //Debug.Log("Player InLow HP ");
        }
        if ((LowHp && needHealing) || (healAfterInterrupt))
        {
           // Debug.Log("Player Invoking HP ");
            needHealing = false;
            healAfterInterrupt = false;
            Invoke(nameof(HealPlayer), 3f);
        }

    }
    float CalculateHealth()
    {
        return health / maxHealth;
    }

    void HealPlayer()
    {
        if (takingDamage)
        {
            healAfterInterrupt = true;
            Invoke(nameof(NotTakingDmgReadyToHeal), 0.5f);
            return;
        }
        //Debug.Log("Lifescrpt HP" + health);
        //Debug.Log("HealPlayer() is called ");
        if (health < maxHealth && needHealing == false)
        {
            health += 0.1f * maxHealth;
            Invoke(nameof(HealPlayer), 0.1f);
            return;
        }
        else
        {
            needHealing = true;
            LowHp = false;
        }



    }

    void NotTakingDmgReadyToHeal()
    {
        takingDamage = false;
    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.layer == 11)
        {
            takingDamage = true;
            health -= 10;
            //Debug.Log(LifeScript.health);
            Destroy(other.gameObject);
        }




    }

}
