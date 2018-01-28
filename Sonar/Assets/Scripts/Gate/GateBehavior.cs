using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateBehavior : MonoBehaviour
{
    public GameObject[] BeaconIndicators = new GameObject[5];

    bool[] indicatorsOnline;

    
	// Use this for initialization
	void Start ()
    {
        indicatorsOnline = new bool[BeaconIndicators.Length];
	}

    void Update()
    {
        float emission = Mathf.PingPong(Time.time, 1.0f);
        Color baseColor = Color.cyan;

        Color finalColor = baseColor * Mathf.LinearToGammaSpace(emission);

        for (int i = 0; i < indicatorsOnline.Length; ++i)
        {
            if (indicatorsOnline[i])
            {
                Renderer renderer = BeaconIndicators[i].GetComponent<Renderer>();
                Material mat = renderer.material;

                mat.SetColor("_EmissionColor", finalColor);
            }
        }
        //Renderer renderer = GetComponent<Renderer>();
        //Material mat = renderer.material;
        //
        //mat.SetColor("_EmissionColor", finalColor);
    }

    public void activateIndicator(int index)
    {
        indicatorsOnline[index] = true;
    }

    public void openGate()
    {
        Debug.Log("Gate is open!");
        StartCoroutine("LowerGate", 2.0f);
    }

    IEnumerator LowerGate(float time)
    {
        float elapsed = 0;
        Vector3 start = transform.position;
        Vector3 end = new Vector3(start.x, start.y - 20, start.z);
        while (elapsed < time)
        {
            transform.position = Vector3.Lerp(start, end, (elapsed / time));
            elapsed += Time.deltaTime;
            yield return null;
        }
    }
}
