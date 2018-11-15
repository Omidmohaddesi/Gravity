using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShockwaveScript : MonoBehaviour {

    Renderer rend;
    float magnitude;
    public float shifting_speed = 0.05f;
    void Start () {
        rend = GetComponent<Renderer>();

        // Use the Specular shader on the material
        rend.material.shader = Shader.Find("Effects/WaveEffect");
    }
	
	
	void Update ()
    {
        magnitude += shifting_speed;
        if (magnitude >= 5)
            magnitude = 0;
        rend.material.SetFloat("_Magnitude", magnitude);
    }
}
    