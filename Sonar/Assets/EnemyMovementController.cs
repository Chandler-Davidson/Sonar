using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovementController : MonoBehaviour {
    public int health;

    public GameObject NodeFolder;
    private Transform[] walkingNodes;
    private Transform currentNode;
    private bool atNode;

    public int moveSpeed;

	// Use this for initialization
	void Start () {
        // Define end points from folder
        walkingNodes = NodeFolder.GetComponentsInChildren<Transform>();

        Walk();
    }

    void Walk() {
        // Choose a rand node
        currentNode = walkingNodes[Random.Range(0, walkingNodes.Length)];
        // Move to new position
        transform.position = Vector3.MoveTowards(transform.position, currentNode.position, moveSpeed * Time.deltaTime);
    }
	
	// Update is called once per frame
	void Update () {
        
		// If at current node ish, update
        if (Vector2.Distance(transform.position, currentNode.position) < 2)
		{
            Walk();
		}
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Bullet") {
            health--;
        } else if (other.gameObject.tag == "Player") {
            // Hurt player
            //other.gameObject.GetComponent();
        }
    }
}
