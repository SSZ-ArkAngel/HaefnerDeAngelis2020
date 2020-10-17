using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProbeMotion : MonoBehaviour
{
    public float xMotion;
    public float yMotion;
    public float egoVelocity;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Vector3 velocity = new Vector3(xMotion, yMotion, 0.0f);
        Vector3 displacement = velocity * Time.deltaTime;
        transform.localPosition += displacement;
    }
}