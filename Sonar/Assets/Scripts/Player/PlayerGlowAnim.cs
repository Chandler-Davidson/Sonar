using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGlowAnim : MonoBehaviour
{
    Animator anim;
    public AnimationClip idleClip;
    public AnimationClip fireClip;

    float delta = 0.0f;
    float duration;


	// Use this for initialization
	void Start ()
    {
        anim = GetComponent<Animator>();
        duration = fireClip.length;
        anim.Play(idleClip.name);
	}
	
	// Update is called once per frame
	void Update ()
    {
		if (delta > 0.0f)
        {
            delta -= Time.deltaTime;
            if (delta <= 0.0f)
            {
                anim.Play(idleClip.name);
            }
        }
	}

    public void Animate()
    {
        anim.Play(fireClip.name);
        delta = duration;
    }
}
