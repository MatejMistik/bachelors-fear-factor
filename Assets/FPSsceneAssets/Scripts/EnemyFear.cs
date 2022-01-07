using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyFear : MonoBehaviour
{
    // Start is called before the first frame update
    private int counterNTLProbability;
    private float maxFear;
    private float fear;
    public static bool needHealing = true;
    public bool CalculatingNTL;
    [SerializeField] float SetMaxForFear;
    private float probabilityNTL;
    private float randomNumberforNTL;

    public GameObject HealhtBarUI;
    public Slider slider;
    public Transform player;

    void Start()
    {
        randomNumberforNTL = Random.Range(0.0f, 1.0f);
        maxFear = SetMaxForFear;
        CalculatingNTL = false;
    }

    // Update is called once per frame
    void Update()
    {
        slider.value = CalculateFear();
        slider.transform.LookAt(player);

        if (EnemyAI.flee && !CalculatingNTL)
        {
            WillNTLBeTurnedOn();
            CalculatingNTL = true;
        }

    }
    float CalculateFear()
    {
        return fear / maxFear;
    }

    void WillNTLBeTurnedOn()
    {
        if (EnemyAI.NTL)
        {
            return;
        }
        Debug.Log("WillNTL is called");
        if (!EnemyAI.NTL && EnemyAI.DistanceOfEnemy <= 20 && EnemyAI.flee) 
        {
            counterNTLProbability++;
            fear += 10;
        }else
        {
            counterNTLProbability--;
            fear -= 10;
        }
        
        Debug.Log(" Before propability was P :" + probabilityNTL);
        probabilityNTL = 0.1f * counterNTLProbability;


        if ( randomNumberforNTL < probabilityNTL)
        {
            Debug.Log("propability was P :" + probabilityNTL);
            CalculNTLSetToFalse();
            return;
        }
        Invoke("WillNTLBeTurnedOn", 1f);

        
    }

    void CalculNTLSetToFalse()
    {
        CalculatingNTL = false;
        counterNTLProbability = 0;
        probabilityNTL = 0;
        fear = 0;
        randomNumberforNTL = Random.Range(0.0f, 1.0f);
        EnemyAI.NTL = true;
    }
}
