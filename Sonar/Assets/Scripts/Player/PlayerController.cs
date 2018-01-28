using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public int playerHealth;
    public float playerSpeed;
    private Rigidbody playerRb;
    float origY;


    void Start()
    {
        origY = transform.position.y;
        playerRb = gameObject.GetComponent<Rigidbody>();
    }

    void Update()
    {
        // Collect input, normalize
        Vector2 dir = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        dir.Normalize();

        // Update player location
        transform.position += new Vector3(dir.x, 0.0f, dir.y) * playerSpeed * Time.deltaTime;
        transform.position = new Vector3(transform.position.x, origY, transform.position.z);
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