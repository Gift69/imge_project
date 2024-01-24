using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour
{
    private Animator animator;
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            // Play the animation once
            PlayAnimationOnce(animator);
        }
    }

    void PlayAnimationOnce(Animator animator)
    {
        animator.Play("Scale_Highlight");
    }
}
