using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProbeMotion : MonoBehaviour
{
    
    float motionAngle = 0;
    float motionAngleRad;

    float probeSpeed = -0.2f;
    float xMotion;
    float yMotion;

    int probeAngleIndex;
    public float egoVelocity;
    // public GameObject overlord;
    // Start is called before the first frame update
    
    void RNGesus()
    {
        //ProbeAngleBloc
        if(probeAngleIndex==0)
        {
            motionAngle = -15;
        }
        if(probeAngleIndex==1)
        {
            motionAngle = 0;
        }
        if(probeAngleIndex==2)
        {
            motionAngle = 15;
        }
    }
    void Start()
    {
        
        probeAngleIndex = GameObject.Find("CameraController").GetComponent<EgoMotion>().probeAngleIndex;
        // probeAngleIndex = Random.Range(0,3);
        RNGesus();

        motionAngleRad = motionAngle * (Mathf.PI/180);
        xMotion = probeSpeed * Mathf.Sin(motionAngleRad);
        yMotion = probeSpeed * Mathf.Cos(motionAngleRad);
        // Overlord controllerScript = overlord.GetComponent<Overlord>();
        // controllerScript.probeVelX = xMotion;
        // controllerScript.probeVelY = yMotion;

    }

    // Update is called once per frame
    void Update()
    {
        Vector3 velocity = new Vector3(xMotion, yMotion, 0.0f);
        Vector3 displacement = velocity * Time.deltaTime;
        transform.localPosition += displacement;
    }
}