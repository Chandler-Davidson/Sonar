using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerManager : MonoBehaviour
{
    public GameObject[] towers = new GameObject[3];
    public GameObject wall;

    bool[] towersOnline;

	// Use this for initialization
	void Start ()
    {
        towersOnline = new bool[towers.Length];
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

}
