using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerHealthController : MonoBehaviour
{
    public int towerDataLeft;
    private bool towerAlive;

    public GameObject player;
    public GameObject Sonar;

    // Use this for initialization
    void Start()
    {
        towerAlive = true;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider collision)
    {
        if (towerAlive && collision.gameObject.tag == "Sonar")
        {
            towerDataLeft--;

            if (towerDataLeft <= 0)
            {
                TowerComplete();
            }

            // Enable if we want towers to shoot back
            //Fire();

        }
    }

	// Shoot sonar
	void Fire()
	{
		// Spawn gameobject
		GameObject instance = (GameObject)Instantiate(
            Sonar,
            transform.position,
            transform.rotation);
		instance.name = "Sonar";
        instance.tag = "Untagged";

        // Remove collider to not collide with self
        instance.GetComponent<Collider>().enabled = false;

		// Calculate "forward" TODO: Find last dir moved
		Vector2 dir = (instance.transform.right);

		// Fire sonar back to player
		float step = 10 * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, player.transform.position, step);

		// Destroy in 3.0s
		Destroy(instance, 3.0f);
	}

    private void TowerComplete()
    {
        towerAlive = false;
        print("TOWER COMPLETE");
    }
}
