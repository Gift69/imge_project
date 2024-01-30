using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mage_shot : MonoBehaviour
{
    private float time = 0;
    private float end;
    private void Awake()
    {
        time = 0;
        end = gameObject.transform.GetChild(0).transform.GetChild(0).GetComponent<Animator>().runtimeAnimatorController.animationClips[0].length;
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        if (time > end)
        {
            Destroy(gameObject);
        }
    }
}
