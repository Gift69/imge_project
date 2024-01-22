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


    // noise
    private Gradient colorRamp = new Gradient();
    private float noiseScale = 0.15f;
    private float noiseXOffset = 80;
    private float noiseYOffset = 150;

    void Awake()
    {
        // noise:
        // Blend color from red at 0% to blue at 100%
        var colors = new GradientColorKey[4];
        colors[0] = new GradientColorKey(new Color(0f, 0.95f, 1f), 0.0f);
        colors[1] = new GradientColorKey(new Color(0.03f, 1f, 0f), 0.526f);
        colors[2] = new GradientColorKey(new Color(0.16f, 1f, 0f), 0.7f);
        colors[3] = new GradientColorKey(new Color(1f, 1f, 0f), 1.0f);

        // Blend alpha from opaque at 0% to transparent at 100
        var alphas = new GradientAlphaKey[2];
        alphas[0] = new GradientAlphaKey(1.0f, 0.0f);
        alphas[1] = new GradientAlphaKey(0.0f, 1.0f);

        colorRamp.SetKeys(colors, alphas);
        colorRamp.mode = GradientMode.PerceptualBlend;

        shellMaterial = new Material(shellShader);

        shells = new GameObject[shellCount];

        for (int i = 0; i < shellCount; ++i)
        {
            // noise
            float rand = Mathf.PerlinNoise(noiseScale * transform.position.x + noiseXOffset,
                noiseScale * transform.position.z + noiseYOffset);
            shellColor = colorRamp.Evaluate(rand);

            // copied stuff
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