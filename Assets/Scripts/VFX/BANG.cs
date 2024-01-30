using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BANG : MonoBehaviour
{
    private float time = 0;
    private Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        if (time > anim.runtimeAnimatorController.animationClips[0].length)
        {
            Destroy(this.gameObject);
        }
    }
}
