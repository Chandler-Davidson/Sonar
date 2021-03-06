﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {

    EnemyMovementController[] enemies;

	// Use this for initialization
	void Start () {
        enemies = GetComponentsInChildren<EnemyMovementController>();

	}
	
	// Update is called once per frame
	void Update () {
        
	}

    public void SendSonar(Vector3 playerPos) {
        foreach (EnemyMovementController enemy in enemies) {

            if (enemy != null)
            {
                // Imperfect positioning
                playerPos.x += Random.Range(-5, 5);
                playerPos.z += Random.Range(-5, 5);

                // Send destination
                enemy.SetNewDestination(playerPos);
            }
        }
    }
}
