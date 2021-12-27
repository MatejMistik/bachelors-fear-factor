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
            if (PlayerModel.playerLife >= 0)
            {

                //PlayerModel.playerLife--;
                //LifeCounter.lifeValue--;
                player.transform.position = respawnPoint.transform.position;
                Physics.SyncTransforms();
                
            }
            // TODO - Game Over
        }
    }

    public static void ResetInputAxes()
    {

    }
}
