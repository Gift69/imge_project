using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Highlight : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        transform.Rotate(new Vector3(0, Random.Range(0, 6) * 60, 0), Space.Self);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
