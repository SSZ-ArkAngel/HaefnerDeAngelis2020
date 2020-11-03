using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaddleTransform : MonoBehaviour
{
    public Vector3 paddleTransform;

    Quaternion paddleRotation;
    GameObject overlord;
    GameObject paddleArm;
    // Start is called before the first frame update
    void Start()
    {
        overlord = GameObject.Find("ExperimentController");
        paddleTransform = overlord.GetComponent<Overlord>().paddleTransform;

        paddleArm = GameObject.Find("Arm");
        paddleRotation = paddleArm.GetComponent<Transform>().localRotation;
        paddleRotation.z = Random.Range(-0.5f, 0.5f);

        //transform.localRotation.z = Random.Range(-90f, 90f);

        if(paddleTransform.x == 1.0f)
        {
           paddleTransform.x = 0.85f;
        }

        if(paddleTransform.x == 1.5f)
        {
           paddleTransform.x = 1.3f;
        }

        overlord.GetComponent<Overlord>().paddleTransform = paddleTransform;

        transform.localPosition = paddleTransform;
        paddleArm.GetComponent<Transform>().localRotation = paddleRotation;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
