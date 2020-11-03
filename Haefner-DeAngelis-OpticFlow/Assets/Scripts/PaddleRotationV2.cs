using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PaddleRotationV2 : MonoBehaviour
{
    // Start is called before the first frame update
    public Vector3 point;
    GameObject overlord;
    GameObject paddle;
    float absoluteTilt;
    float relativeTilt;
    float probeAngle;
    float reactionTime;

    float fineRotation = 5;
    float coarseRotation = 20;

    public bool isSample = false;

    public Vector3 sampleTransform;

    private Overlord volatileHunter;
    
    void SetSampleTransform()
    {
        sampleTransform.x = 0.0f;
        sampleTransform.y = -2.0f;
        sampleTransform.z = 2.0f;
    }

    void Start()
    {   
        SetSampleTransform();

        overlord = GameObject.Find("ExperimentController");
        probeAngle = overlord.GetComponent<Overlord>().probeAngle;
        //point = overlord.GetComponent<Overlord>().paddleTransform;
        volatileHunter = overlord.GetComponent<Overlord>();
        // Overlord controllerScript = overlord.GetComponent<Overlord>();
        // controllerScript.absoluteTilt = transform.localRotation.z;

        reactionTime = 0.0f;
    }

    // public string ToCSV()
    // {

    // }

    void ChangeSceneToCross()
    {
        SceneManager.LoadScene("Fixation");
    }

    // Update is called once per frame
    void Update()
    {
        
        if(isSample == true)
        {
            point = sampleTransform;
        }
        
        reactionTime += Time.deltaTime;

        if(Input.GetKey(KeyCode.RightArrow))
        {
            transform.RotateAround(point, Vector3.forward, fineRotation*Time.deltaTime);
            absoluteTilt += fineRotation*Time.deltaTime;
        }

        if(Input.GetKey(KeyCode.LeftArrow))
        {
            transform.RotateAround(point, Vector3.back, fineRotation*Time.deltaTime);
            absoluteTilt -= fineRotation*Time.deltaTime;
        }

        if(Input.GetKey(KeyCode.RightArrow) && Input.GetKey(KeyCode.RightShift))
        {
            transform.RotateAround(point, Vector3.forward, coarseRotation*Time.deltaTime);
            absoluteTilt += coarseRotation*Time.deltaTime;
        }

        if(Input.GetKey(KeyCode.LeftArrow) && Input.GetKey(KeyCode.RightShift))
        {
            transform.RotateAround(point, Vector3.back, coarseRotation*Time.deltaTime);
            absoluteTilt -= coarseRotation*Time.deltaTime;
        }

        if(Input.GetKey(KeyCode.Return))
        {
            // Start();
            //write angles
            //absoluteTilt = -transform.localRotation.z;
            relativeTilt = absoluteTilt - probeAngle;
            //export angles
            overlord.GetComponent<Overlord>().absoluteTilt = absoluteTilt;
            overlord.GetComponent<Overlord>().relativeTilt = relativeTilt;

            //write and export reaction time
            overlord.GetComponent<Overlord>().reactionTime = reactionTime;

            //Write Data
            volatileHunter.WriteData();

            //Change Scene
            ChangeSceneToCross();
        }

        if(Input.GetKey(KeyCode.Escape))
        {
            //volatileHunter.ExportData();
            Application.Quit();
        }
    }
}