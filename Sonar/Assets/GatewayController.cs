using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GatewayController : MonoBehaviour {

	// Use this for initialization
	void Start () {
        OpenGate();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void OpenGate() {
        gameObject.GetComponent<Collider>().isTrigger = true;
        gameObject.GetComponent<Renderer>().enabled = false;
    }
}
