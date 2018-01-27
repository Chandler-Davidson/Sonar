using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public int playerHealth;
    public float playerSpeed;
    private Rigidbody playerRb;

    void Start()
    {
        playerRb = gameObject.GetComponent<Rigidbody>();
    }

    void Update()
    {
        // Collect input, normalize
        Vector2 dir = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        dir.Normalize();

        // Update player location
        transform.position += new Vector3(dir.x, dir.y, 0.0f) * playerSpeed * Time.deltaTime;
        playerRb.velocity = Vector3.zero;
	}

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            // Hurt player, only counts collision once. Lets player escape
            playerHealth--;

            // Die
            if (playerHealth <= 0)
            {
                Destroy(this.gameObject);
            }
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        // Prevents any 'bounce back' from wall
		if (collision.gameObject.tag == "Wall") {
			playerRb.velocity = new Vector3(0, 0, 0);
		}
    }
}