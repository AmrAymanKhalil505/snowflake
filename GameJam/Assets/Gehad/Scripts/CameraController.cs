using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    Vector3 target;
    public float followSpeed;
    // Start is called before the first frame update
    void Start()
    {
    
    }

    // Update is called once per frame
    void LateUpdate()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform.position;
        transform.position = Vector3.Lerp(transform.position, target + new Vector3(-2.5f,5,-4), followSpeed * Time.deltaTime);
    }
}
