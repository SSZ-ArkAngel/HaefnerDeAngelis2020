using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaddleTransform : MonoBehaviour
{
    public Vector3 paddleTransform;
    GameObject overlord;
    // Start is called before the first frame update
    void Start()
    {
        overlord = GameObject.Find("ExperimentController");
        paddleTransform = overlord.GetComponent<Overlord>().paddleTransform;
        transform.localPosition = paddleTransform;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
