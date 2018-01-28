using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovementController : MonoBehaviour
{
    // Health
    public int health;

    // Movement destinations
    public GameObject NodeFolder;
    private Transform[] walkingNodes;
    private Vector3 destinationNode;

    // Move speeds
    public int moveSpeedMin;
    public int moveSpeedMax;
    private int actualMoveSpeed;

    // Attack attributes
    private bool enraged;
    public int enrageDistance;
    public GameObject player;

    void Start()
    {
        enraged = false;

        // Define end points from folder
        walkingNodes = NodeFolder.GetComponentsInChildren<Transform>();

        SetNewDestination();
    }

    void Update()
    {
        // If player is alive and enemy< enraged distance from player, attack
        if (player != null)
            enraged = (Vector3.Distance(transform.position, player.transform.position) < enrageDistance);
        else
            enraged = false;

        // TODO: Maybe if prev enraged, and now not assign new point

        // If at current node ish, update
        if (!enraged && Vector3.Distance(transform.position, destinationNode) < 5)
        {
            SetNewDestination();
        }

        if (enraged)
        {
            FollowPlayer();
        }
        else
        {
            Walk(destinationNode);
        }
    }

    // Update position each fram
    void Walk(Vector3 destination)
    {
        // Move to new position
        float step = actualMoveSpeed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, destination, step);
    }

    void FollowPlayer()
    {
        // Move to player at max speed
        float step = moveSpeedMax * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, player.transform.position, step);
    }

    // Update destination
    void SetNewDestination()
    {
        // Choose a rand node
        destinationNode = walkingNodes[Random.Range(0, walkingNodes.Length)].position;

        // Determine speed
        actualMoveSpeed = Random.Range(moveSpeedMin, moveSpeedMax);

        //transform.LookAt(transform.position - destinationNode);
        //transform.
	}

    public void SetNewDestination(Vector3 pos) {
        destinationNode = pos;

        actualMoveSpeed = moveSpeedMax;

        transform.LookAt(transform.position - pos);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Bullet") {
	        // Hurt self
	        health--;

	        // Dead
	        if (health <= 0)
	        {
	            // TODO: Animation
	            Destroy(this.gameObject);
	        }

            Destroy(other.gameObject);
        }
    }
}
