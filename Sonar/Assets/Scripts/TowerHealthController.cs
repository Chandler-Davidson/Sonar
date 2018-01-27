﻿﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerHealthController : MonoBehaviour
{
    public int towerDataLeft;
    private bool towerAlive;

    public GameObject player;
    public GameObject Sonar;

    // sprite array for different power up levels
    public Sprite[] BeaconPhases = new Sprite[6];
    private SpriteRenderer spriteRenderer;
    private int spriteNumber = 0;

    // emit effect for when hit
    public GameObject HitEffect;
    public Transform EffectSpawnPoint;
    public AudioClip EffectSound;
    private AudioSource dj;

    GameObject towerManager;
    int id;

    public bool getAlive() { return towerAlive; }

	// Use this for initialization
	void Start ()
    {
        id = TowerManager.towerId++;
        towerAlive = true;

        //dj = FindObjectOfType<AudioSource>();

        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();

        towerManager = GameObject.Find("Tower Manager");

        if (HitEffect == null)
        {
            HitEffect = GameObject.Find("BeaconEffect");
        }
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

            //dj.PlayOneShot(EffectSound);

            GameObject killThis = Instantiate(HitEffect, EffectSpawnPoint.position, Quaternion.identity);
            Destroy(killThis, 0.5f);

            spriteRenderer.sprite = BeaconPhases[++spriteNumber];

            if (towerDataLeft <= 0) {
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
        spriteRenderer.sprite = BeaconPhases[BeaconPhases.Length - 1];

        towerManager.GetComponent<TowerManager>().TowerOnline(id);
        print("TOWER COMPLETE");
    }
}
