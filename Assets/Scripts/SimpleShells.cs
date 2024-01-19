using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class SimpleShell : MonoBehaviour
{
    public Mesh shellMesh;
    public Shader shellShader;

    // private bool updateStatics = false;

    // These variables and what they do are explained on the shader code side of things
    // You can see below (line 70) which shader uniforms match up with these variables
    [Range(1, 256)] private int shellCount = 20;

    [Range(0.0f, 1.0f)] private float shellLength = 0.08f;

    [Range(0.01f, 3.0f)] private float distanceAttenuation = 1.0f;

    [Range(1.0f, 1000.0f)] private float density = 70.0f;

    [Range(0.0f, 1.0f)] private float noiseMin = 0.0f;

    [Range(0.0f, 1.0f)] private float noiseMax = 1.0f;

    private Color shellColor;

    [Range(0.0f, 5.0f)] private float occlusionAttenuation = 0.95f;

    [Range(0.0f, 1.0f)] private float occlusionBias = 0.14f;

    private Material shellMaterial;
    private GameObject[] shells;

    void Awake()
    {
        /*  if (shellMesh.uv2[0].x != 3f)
          {
              Debug.Log("hallo ich bin hier");
              Vector2[] newuv2 = new Vector2[shellMesh.uv2.Length];
              newuv2[0] = new Vector2(2.0f, 0);
              shellMesh.uv2 = newuv2;
              Vector2[] rotateduvs = shellMesh.uv; //Store the existing UV's
              Vector2 min = Vector2.zero;
  
              for (var i = 0; i < rotateduvs.Length; i++)
              {
                  //Go through the array
                  var rot = Quaternion.Euler(0, 0, 60);
                  rotateduvs[i] = rot * rotateduvs[i];
                  min = new Vector2(Math.Min(min.x, rotateduvs[i].x), Math.Min(min.y, rotateduvs[i].y));
              }
  
              min = new Vector2(Math.Min(min.x, 0), Math.Min(min.y, 0));
              for (var i = 0; i < rotateduvs.Length; i++)
              {
                  rotateduvs[i] -= min;
              }
  
              shellMesh.uv = rotateduvs; //re-apply the adjusted uvs
          }*/


        shellMaterial = new Material(shellShader);
        float hue = Random.Range(100.0f, 141.0f) / 360.0f;
        shellColor = Color.HSVToRGB(hue, 1, 1);

        shells = new GameObject[shellCount];

        for (int i = 0; i < shellCount; ++i)
        {
            shells[i] = new GameObject("Shell " + i.ToString());
            shells[i].AddComponent<MeshFilter>();
            shells[i].AddComponent<MeshRenderer>();

            shells[i].GetComponent<MeshFilter>().mesh = shellMesh;
            shells[i].GetComponent<MeshRenderer>().material = shellMaterial;
            shells[i].transform.SetParent(this.transform, false);

            // In order to tell the GPU what its uniform variable values should be, we use these "Set" functions which will set the
            // values over on the GPU. 
            shells[i].GetComponent<MeshRenderer>().material.SetInt("_ShellCount", shellCount);
            shells[i].GetComponent<MeshRenderer>().material.SetInt("_ShellIndex", i);
            shells[i].GetComponent<MeshRenderer>().material.SetFloat("_ShellLength", shellLength);
            shells[i].GetComponent<MeshRenderer>().material.SetFloat("_Density", density);
            shells[i].GetComponent<MeshRenderer>().material.SetFloat("_Attenuation", occlusionAttenuation);
            shells[i].GetComponent<MeshRenderer>().material.SetFloat("_ShellDistanceAttenuation", distanceAttenuation);
            shells[i].GetComponent<MeshRenderer>().material.SetFloat("_OcclusionBias", occlusionBias);
            shells[i].GetComponent<MeshRenderer>().material.SetFloat("_NoiseMin", noiseMin);
            shells[i].GetComponent<MeshRenderer>().material.SetFloat("_NoiseMax", noiseMax);
            shells[i].GetComponent<MeshRenderer>().material.SetVector("_ShellColor", shellColor);
            shells[i].GetComponent<MeshRenderer>().material.SetInt("_Random_Seed", gameObject.GetHashCode());
        }

        float rock_hue = Random.Range(227.0f, 247.0f) / 360.0f;
        float rock_value = Random.Range(20.0f, 35.0f) / 100.0f;
        float rock_saturation = Random.Range(0.0f, 10.0f) / 100.0f;
        GameObject child = this.gameObject.transform.GetChild(0).gameObject;
        child.GetComponent<Renderer>().material
            .SetColor("_Color", Color.HSVToRGB(rock_hue, rock_saturation, rock_value));
    }

    void Update()
    {
        // Generally it is bad practice to update statics that do not need to be updated every frame
        // You can see the performance difference between updating 256 shells of statics by disabling the updateStatics parameter in the script
        // So it obviously matters at the extreme ends, but something above like setting the directional vector each frame is not going to make an insane diff
        // You will see in my other shaders and scripts that I do not always do this, because I'm lazy, but it's best practice to not update what doesn't need to be
        // updated.
        /*if (updateStatics) {
            for (int i = 0; i < shellCount; ++i) {
                shells[i].GetComponent<MeshRenderer>().material.SetInt("_ShellCount", shellCount);
                shells[i].GetComponent<MeshRenderer>().material.SetInt("_ShellIndex", i);
                shells[i].GetComponent<MeshRenderer>().material.SetFloat("_ShellLength", shellLength);
                shells[i].GetComponent<MeshRenderer>().material.SetFloat("_Density", density);
                shells[i].GetComponent<MeshRenderer>().material.SetFloat("_Attenuation", occlusionAttenuation);
                shells[i].GetComponent<MeshRenderer>().material.SetFloat("_ShellDistanceAttenuation", distanceAttenuation);
                shells[i].GetComponent<MeshRenderer>().material.SetFloat("_OcclusionBias", occlusionBias);
                shells[i].GetComponent<MeshRenderer>().material.SetFloat("_NoiseMin", noiseMin);
                shells[i].GetComponent<MeshRenderer>().material.SetFloat("_NoiseMax", noiseMax);
                shells[i].GetComponent<MeshRenderer>().material.SetVector("_ShellColor", shellColor);
            }
        }*/
    }

    void OnDisable()
    {
        for (int i = 0; i < shells.Length; ++i)
        {
            Destroy(shells[i]);
        }

        shells = null;
    }
}