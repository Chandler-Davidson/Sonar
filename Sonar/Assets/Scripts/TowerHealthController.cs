using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerHealthController : MonoBehaviour {
    public int towerDataLeft;
    private bool towerAlive;

	// Use this for initialization
	void Start () {
        towerAlive = true;
	}

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter (Collider collision) {
        if (towerAlive && collision.gameObject.tag == "Sonar") {
            towerDataLeft--;

            if (towerDataLeft <= 0) {
                TowerComplete();
            }
        }
    }

    private void TowerComplete() {
        towerAlive = false;
        print("TOWER COMPLETE");
    }
}
