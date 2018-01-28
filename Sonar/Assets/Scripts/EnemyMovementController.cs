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
    private Vector2 destinationNode;

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
            enraged = (Vector2.Distance(transform.position, player.transform.position) < enrageDistance);
        else
            enraged = false;

        // TODO: Maybe if prev enraged, and now not assign new point

        // If at current node ish, update
        if (!enraged && Vector2.Distance(transform.position, destinationNode) < 2)
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
    void Walk(Vector2 destination)
    {
        // Move to new position
        float step = actualMoveSpeed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, destination, step);
    }

    void FollowPlayer()
    {
        // Move to player at max speed
        float step = moveSpeedMax * Time.deltaTime;
        transform.position = Vector2.MoveTowards(transform.position, player.transform.position, step);
    }

    // Update destination
    void SetNewDestination()
    {
        // Choose a rand node
        destinationNode = walkingNodes[Random.Range(0, walkingNodes.Length)].position;

        // Determine speed
        actualMoveSpeed = Random.Range(moveSpeedMin, moveSpeedMax);
    }

    public void SetNewDestination(Vector2 pos) {
        destinationNode = pos;

        actualMoveSpeed = moveSpeedMax;
    }

    private void OnTriggerEnter(Collider other)
    {
        switch (other.gameObject.tag)
        {
            case "Bullet":
                // Hurt self
                health--;

                // Dead
                if (health <= 0)
                {
                    // TODO: Animation
                    Destroy(this.gameObject);
                }

                break;
            case "Player":
                // Player's health handled in PlayerController.cs
                break;
            case "Sonar":
                // Now covered in EnemyController

                // Get player's current position
                //destinationNode = player.transform.position;
                break;
        }
    }
}
