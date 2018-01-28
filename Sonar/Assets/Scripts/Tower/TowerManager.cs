using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerManager : MonoBehaviour
{
    public GameObject[] towers = new GameObject[5];
    public GameObject wall;

    public static int towerId = 0;

    bool[] towersOnline;

    public GameObject winScreen;
    bool win = false;
    float winTime = 5.0f;

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
		if (win)
        {
            winTime -= Time.deltaTime;
            if (winTime < 0.0f)
            {
                Application.Quit();
            }
        }
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

    public void TriggerWin()
    {
        win = true;
        winScreen.SetActive(true);
    }
}
