using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Noise_Visualizer : MonoBehaviour
{
    public Gradient colorRamp;
    public int amount;
    public float distance;
    public float scale = 1.0f;
    public GameObject instance;

    private GameObject[] objs;

    // Start is called before the first frame update
    void Start()
    {
        objs = new GameObject[amount * amount];

        Vector3 startOffset = new Vector3(-amount / 2.0f * distance, 0, -amount / 2.0f * distance);
        for (int i = 0; i < amount; i++)
        {
            for (int j = 0; j < amount; j++)
            {
                Vector3 pos = new Vector3(i * distance, 0, j * distance) + startOffset;
                objs[i * amount + j] = Instantiate(instance, pos, Quaternion.identity, this.transform);
                //objs[i * amount + j];
                float rand = Mathf.PerlinNoise(scale * pos.x + 1000, scale * pos.z + 1000);
                objs[i * amount + j].GetComponent<Renderer>().material
                    .SetColor("_Color", colorRamp.Evaluate(rand));
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 startOffset = new Vector3(-amount / 2.0f * distance, 0, -amount / 2.0f * distance);
        for (int i = 0; i < amount; i++)
        {
            for (int j = 0; j < amount; j++)
            {
                Vector3 pos = new Vector3(i * distance, 0, j * distance) + startOffset;
                //objs[i * amount + j];
                float rand = Mathf.PerlinNoise(scale * pos.x + 1000, scale * pos.z + 1000);
                objs[i * amount + j].GetComponent<Renderer>().material
                    .SetColor("_Color", colorRamp.Evaluate(rand));
            }
        }
    }
}