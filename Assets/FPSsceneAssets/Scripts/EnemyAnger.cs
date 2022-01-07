using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EnemyAnger : MonoBehaviour
{
    public Transform player;
    public TextMeshProUGUI text;

    // Update is called once per frame
    void Update()
    {
        
        text.transform.LookAt(player);
        text.transform.rotation = Quaternion.LookRotation(text.transform.position - player.position);
        if (EnemyAI.NTL) {
            text.SetText("NTL");
            return;
 
        }
        text.SetText(EnemyAI.enemyState);
        
    }
}
