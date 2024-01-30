using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class explosion : MonoBehaviour
{
    public GameObject bang;

    private void Awake()
    {
        Instantiate(bang, transform.position + Vector3.up * 1.1f, Quaternion.identity);
    }
    
    private Animator anim;
    private float time = 0;

    private void Start()
    {
        anim = bang.gameObject.GetComponent<Animator>();
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
