using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerManager : MonoBehaviour
{
    public GameObject[] towers = new GameObject[3];
    public GameObject wall;

    public static int towerId = 0;

    bool[] towersOnline;

	// Use this for initialization
	void Start ()
    {
        towersOnline = new bool[towers.Length];
        for (int i = 0; i < towersOnline.Length; ++i)
        {
            towersOnline[i] = false;
        }
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    public void TowerOnline(int id)
    {
        towersOnline[id] = true;

        // tell wall or UI the tower is online
        wall.GetComponent<GateBehavior>().activateIndicator(id);

        if (allOnline())
        {
            // tell the wall to open
            wall.GetComponent<GateBehavior>().openGate();
        }
    }

    bool allOnline()
    {
        foreach (bool b in towersOnline)
        {
            if (!b)
            {
                return false;
            }
        }
        return true;
    }
}
