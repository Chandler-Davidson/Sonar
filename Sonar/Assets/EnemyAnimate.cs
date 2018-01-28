using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimate : MonoBehaviour
{
    Animator anim;
    public AnimationClip clip;

	// Use this for initialization
	void Start ()
    {
        anim = GetComponent<Animator>();
        anim.Play(clip.name);
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}
}
