using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    public int playerHealth;
    public float playerSpeed;

	// Use this for initialization
	void Start () {
		
	}

    // Update is called once per frame
    void FixedUpdate()
    {
        // Collect input, normalize
        Vector2 dir = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        dir.Normalize();

        // Update player location
        transform.position += new Vector3(dir.x, dir.y, 0.0f) * playerSpeed * Time.deltaTime;
    }

    private void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.tag == "Enemy") {
            playerHealth--;

            if (playerHealth <= 0) {
                
            }
        }
    }

    private void Die() {
        this.gameObject.GetComponent<SpriteRenderer>().enabled = true;
    }
}
