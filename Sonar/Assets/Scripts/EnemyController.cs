using System.Collections;
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

    public void SendSonar(Vector2 playerPos) {
        foreach (EnemyMovementController enemy in enemies) {
            // Imperfect positioning
            playerPos.x += Random.Range(-5, 5);
			playerPos.y += Random.Range(-5, 5);

            // Send destination
            enemy.SetNewDestination(playerPos);
        }
    }
}
