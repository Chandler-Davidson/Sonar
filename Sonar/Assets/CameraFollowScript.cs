using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ------- NOT SURE IF NEEED/WANTED -------

public class CameraFollowScript : MonoBehaviour {
    public Transform PlayerPosition;
    private Vector3 CameraOffest;

	// Use this for initialization
	void Start () {
        CameraOffest = transform.position - PlayerPosition.position;
	}
	
	// Update is called once per frame
	void LateUpdate () {
        transform.position = PlayerPosition.position + CameraOffest;
	}
}