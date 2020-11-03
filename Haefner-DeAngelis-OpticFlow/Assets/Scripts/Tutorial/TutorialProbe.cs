using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialProbe : MonoBehaviour
{
    float xMotion = -0.1f;
    float yMotion = -0.2f;
    
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
