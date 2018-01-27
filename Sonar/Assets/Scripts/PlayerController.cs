using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public int playerHealth;
    public float playerSpeed;

    void Start()
    {

    }

    void FixedUpdate()
    {
        // Collect input, normalize
        Vector2 dir = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        dir.Normalize();

        // Update player location
        transform.position += new Vector3(dir.x, dir.y, 0.0f) * playerSpeed * Time.deltaTime;
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
}