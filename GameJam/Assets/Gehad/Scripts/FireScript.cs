using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireScript : MonoBehaviour
{

    public float intensity;
    public Material mat;
    float r;
    float g;
    float b;
    public float speed;
    // Start is called before the first frame update
    void Start()
    {
        intensity = 1;
        mat.SetColor("_Color", new Color(1, 1, 1, 1));
        r = mat.color.r;
        g = mat.color.g;
        b = mat.color.b;
    }

    // Update is called once per frame
    void Update()
    {
        if(intensity > 0)
        {
            intensity -= Time.deltaTime * speed;
            r = intensity;
            g = intensity;
            b = intensity;
            mat.SetColor("_Color", new Color(r, g, b, 1));
        }
    }
}
