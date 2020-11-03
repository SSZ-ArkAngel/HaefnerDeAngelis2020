using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaddleTransform : MonoBehaviour
{
    public Vector3 paddleTransform;
    public Vector3 sampleTransform;

    Quaternion paddleRotation;
    GameObject overlord;
    GameObject paddleArm;

    public bool isSample = false;
    
    // void samplePoint()
    // {
    //     if(isSample == true)
    //     {
    //         paddleTransform.x = 0.0f;
    //         paddleTransform.y = -2.0f;
    //         paddleTransform.z = 2.0f;
    //     }
    // }

    // Start is called before the first frame update
    void Start()
    {
        //samplePoint();
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

        //samplePoint();

        overlord.GetComponent<Overlord>().paddleTransform = paddleTransform;
        paddleArm.GetComponent<PaddleRotationV2>().point = paddleTransform;

        transform.localPosition = paddleTransform;
        paddleArm.GetComponent<Transform>().localRotation = paddleRotation;

        // if(isSample == true)
        // {
        //     paddleArm.GetComponent<PaddleRotationV2>().point = sampleTransform;
        //     paddleArm.GetComponent<PaddleRotationV2>().isSample = true;
        // }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
