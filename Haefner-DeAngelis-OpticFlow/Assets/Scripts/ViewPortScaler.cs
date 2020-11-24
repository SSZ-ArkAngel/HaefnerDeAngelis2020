using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViewPortScaler : MonoBehaviour
{
    float viewPortScale = 1.275f;
    GameObject overlord;
    GameObject viewport;
    
    // Start is called before the first frame update
    void Start()
    {
        viewport = GameObject.Find("Viewport");
        overlord = GameObject.Find("ExperimentController");
        viewPortScale = overlord.GetComponent<Overlord>().viewPortScale;
        
        Vector3 scale = viewport.GetComponent<Transform>().localScale;

        scale.x = 2f;
        scale.y = 0.01f;
        scale.z = 2f;

        viewport.GetComponent<Transform>().localScale = scale;
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
