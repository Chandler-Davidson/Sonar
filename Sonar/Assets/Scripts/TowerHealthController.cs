using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerHealthController : MonoBehaviour {
    public int towerDataLeft;
    private bool towerAlive;

    // sprite array for different power up levels
    public Sprite[] BeaconPhases = new Sprite[6];
    private SpriteRenderer spriteRenderer;
    private int spriteNumber = 0;

    // emit effect for when hit
    public GameObject HitEffect;
    public Transform EffectSpawnPoint;
    public AudioClip EffectSound;
    private AudioSource dj;

	// Use this for initialization
	void Start () {
        towerAlive = true;

        dj = FindObjectOfType<AudioSource>();

        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();

        if (HitEffect == null)
        {
            HitEffect = GameObject.Find("BeaconEffect");
        }
	}

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter (Collider collision) {
        if (towerAlive && collision.gameObject.tag == "Sonar") {
            towerDataLeft--;

            dj.PlayOneShot(EffectSound);

            GameObject killThis = Instantiate(HitEffect, EffectSpawnPoint.position, Quaternion.identity);
            Destroy(killThis, 0.5f);

            spriteRenderer.sprite = BeaconPhases[++spriteNumber];

            if (towerDataLeft <= 0) {
                TowerComplete();
            }
        }
    }

    private void TowerComplete() {
        towerAlive = false;
        spriteRenderer.sprite = BeaconPhases[BeaconPhases.Length - 1];
        print("TOWER COMPLETE");
    }
}
