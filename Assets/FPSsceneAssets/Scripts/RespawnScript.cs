using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnScript : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private Transform respawnPoint;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (LifeScript.Lives >= 0)
            {

                //PlayerMovement.Lives--;
                player.transform.position = respawnPoint.transform.position;
                Physics.SyncTransforms();
                
            }
            // TODO - Game Over
        }
    }
    private void Update()
    {
        if (LifeScript.health <= 0)
        {
            player.transform.position = respawnPoint.transform.position;
            Physics.SyncTransforms();
            LifeScript.health = 100;
            Debug.Log(LifeScript.health);
        }
    }
}
