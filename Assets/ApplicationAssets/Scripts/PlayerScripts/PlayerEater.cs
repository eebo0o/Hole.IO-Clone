using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEater : MonoBehaviour {
    
    PlayerManager playerManager;
    void Start()
    {
        playerManager = transform.parent.parent.GetComponent<PlayerManager>();
    }
    void OnTriggerEnter(Collider trigger)
    {
        if (trigger.gameObject.layer == 13)
        {
            Debug.Log("Detected");
            PlayerManager opponentManager = trigger.GetComponent<PlayerEater>().playerManager;
            if (playerManager.playerLevel > opponentManager.playerLevel)
            {
                opponentManager.Die();
                playerManager.AddScore(opponentManager.playerLevel * 10);
            }
        }
    }

}
