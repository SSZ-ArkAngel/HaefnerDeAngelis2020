using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EgoMotion : MonoBehaviour
{
    public float egoVelocity;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 velocity = new Vector3(0.0f, 0.0f, egoVelocity);
        Vector3 displacement = velocity * Time.deltaTime;
        transform.localPosition += displacement;
    }
}
