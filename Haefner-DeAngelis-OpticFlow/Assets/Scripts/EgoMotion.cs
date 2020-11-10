using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EgoMotion : MonoBehaviour
{ 
    float delay = 2;
    public float egoVelocity;

    //Sync with reference in Fixation
    public bool Exp1;

    public bool publicOverride = false;
    public bool isTesting = false;
    
    public int probeAngleIndex;

    // Fetch the coordinates for the probe's location
    public Vector3 probeTransform;
    public Vector3 apertureScale;

    public Transform probePrefab;
    public Transform aperturePrefab;
    // Start is called before the first frame update

    public GameObject motionController;
    // public GameObject overlord;

    public bool spawnProbe;
    public bool spawnAperture;

    //Export Variables are defined here
    GameObject overlord;
    int sceneIndex;
    string conditionName;
    float stimulusTime;
    float probeEccentricity;
    float apertureDegree;
    float probeStartLocationX;
    float probeStartLocationY;
    float probeEndLocationX;
    float probeEndLocationY;
    public Vector3 paddleTransform;

    int apertureScaleIndex;
    int eccentricityScaleIndex;
    // Conditions:
    // Aperture Size: 0 = 0.4; 1 = 0.5; 2 = 0.6; 3 = 0. 7;
    // Eccentricity: 0 = 1; 1 = 1.5;
    // Motion: 0 = -15; 1 = 0; 2 = 15;

    float distanceMovedByProbeY;
    float distanceMovedByProbeX;
    void probeTrajectoryCalculator()//Scale for screen size
    {
        distanceMovedByProbeY = 0.09f;
        distanceMovedByProbeX = 0.0f;
        float probeTrajectoryCalculatorAngle = Mathf.PI;
        float probeSpeedForTrajectory = GameObject.Find("OpticProbe(Clone)").GetComponent<ProbeMotion>().probeSpeed;

        if(probeAngleIndex == 0)
        {
            probeTrajectoryCalculatorAngle = -15 * (Mathf.PI/180);
            distanceMovedByProbeY = probeSpeedForTrajectory * Mathf.Cos(probeTrajectoryCalculatorAngle); 
            distanceMovedByProbeX = probeSpeedForTrajectory * Mathf.Sin(probeTrajectoryCalculatorAngle);
        }
        if(probeAngleIndex == 1)
        {
            probeTrajectoryCalculatorAngle = 0 * (Mathf.PI/180);
            distanceMovedByProbeY = probeSpeedForTrajectory;

        }
        if(probeAngleIndex == 0)
        {
            probeTrajectoryCalculatorAngle = 15 * (Mathf.PI/180);
            distanceMovedByProbeY = probeSpeedForTrajectory * Mathf.Cos(probeTrajectoryCalculatorAngle);
            distanceMovedByProbeX = probeSpeedForTrajectory * Mathf.Sin(probeTrajectoryCalculatorAngle); 
        }
    }
    void dataExport()
    {

    }
    
    void RNGesus() //Scale to screen size
    {

        apertureScaleIndex = Random.Range(0,4);
        eccentricityScaleIndex = Random.Range(0,2);

        if(publicOverride==false)
        {
            if(Exp1 == true)
            {
                //Eccentricity Set Constant @ (1,0,2)
                // probeTransform = (1.0f, 0.0f, 2.0f);
                probeTransform.x = 0.18f;
                probeTransform.y = 0.0f;
                probeTransform.z = 2.0f;

                if(apertureScaleIndex==0)
                {
                    // apertureScale = (0.4f, 0.4f, 0.01f);
                    apertureScale.x = 0.18f;
                    apertureScale.y = 0.18f;
                    apertureScale.z = 0.01f;
                }

                if(apertureScaleIndex==1)
                {
                    // apertureScale = (0.5f, 0.5f, 0.01f);
                    apertureScale.x = 0.27f;
                    apertureScale.y = 0.27f;
                    apertureScale.z = 0.01f;
                }

                if(apertureScaleIndex==2)
                {
                    // apertureScale = (0.6f, 0.6f, 0.01f);
                    apertureScale.x = 0.315f;
                    apertureScale.y = 0.315f;
                    apertureScale.z = 0.01f;
                }

                if(apertureScaleIndex==3)
                {
                    // apertureScale = (0.7f,0.7f, 0.01f);
                    apertureScale.x = 0.36f;
                    apertureScale.y = 0.36f;
                    apertureScale.z = 0.01f;
                }
                
            }

            if(Exp1==false) //scale for
            {
                // Aperture is constat @ (0.0f, 0.0f, 0.0f)
                apertureScale.x = 0.0f;
                apertureScale.y = 0.0f;
                apertureScale.z = 0.0f;

                if(eccentricityScaleIndex==0)
                {
                    // transform = (1, 0, 2)
                    probeTransform.x = 0.18f;
                    probeTransform.y = 0.0f;
                    probeTransform.z = 2.0f;
                }

                if(eccentricityScaleIndex==1)
                {
                    // transform = (1.5, 0, 2)
                    probeTransform.x = 0.36f;
                    probeTransform.y = 0.0f;
                    probeTransform.z = 2.0f;
                }
            }
        }
    }
    
    void Awake()
    {
        
        // Override
        egoVelocity = 10.0f;
        
        overlord = GameObject.Find("ExperimentController");
        
        probeAngleIndex = Random.Range(0,3);
        RNGesus();

        // Instantiate a probe at the presented coordinates
        if(spawnProbe == true)
        {
        Transform probe = Instantiate(probePrefab);
        probe.localPosition = probeTransform;
        probe.transform.parent = motionController.transform; 
        paddleTransform = probeTransform;
        overlord.GetComponent<Overlord>().paddleTransform = paddleTransform;
        }
        if(spawnAperture == true)
        {
            Transform aperture = Instantiate(aperturePrefab);
            aperture.localPosition =  probeTransform;
            aperture.localScale = apertureScale;
            aperture.transform.parent = motionController.transform;
        }

        // Code to write the name of the scene to the variable for csv storage
        


    }
    
    void Start()
    {
        motionController = GameObject.Find("Thrusters");
        // Variables from Overlord are accessed in the following block
        // Syntax for access: varName = GameObject.Find("ExperimentController").GetComponent<Overlord>().varName;
        // Syntax for write = overlord.GetComponent<Overlord>().varName = VarToExport;
        // Game Object will be defined here:
        overlord = GameObject.Find("ExperimentController");

        //scene index write
        sceneIndex = SceneManager.GetActiveScene().buildIndex;
        conditionName = SceneManager.GetActiveScene().name;
        //scene index export
        overlord.GetComponent<Overlord>().sceneIndex = sceneIndex;
        overlord.GetComponent<Overlord>().conditionName = conditionName;

        //stimulus exposure time write
        stimulusTime = delay;
        //stimulus time export
        overlord.GetComponent<Overlord>().stimulusTime = stimulusTime;

        //eccentricity write
        probeEccentricity = eccentricityScaleIndex;
        //eccentricity export
        overlord.GetComponent<Overlord>().probeEccentricity = probeEccentricity;

        //aperture write
        apertureDegree = apertureScaleIndex;
        //aperture export
        overlord.GetComponent<Overlord>().apertureDegree = apertureDegree;

        //probe location
        probeTrajectoryCalculator();
        probeStartLocationX = probeTransform.x;
        probeStartLocationY = probeTransform.y;
        probeEndLocationX = probeStartLocationX + distanceMovedByProbeX;
        probeEndLocationY = probeStartLocationY + distanceMovedByProbeY;

        //probe location export
        overlord.GetComponent<Overlord>().probeStartLocationX = probeStartLocationX;
        overlord.GetComponent<Overlord>().probeEndLocationX = probeEndLocationX;
        overlord.GetComponent<Overlord>().probeStartLocationY = probeStartLocationY;
        overlord.GetComponent<Overlord>().probeEndLocationY = probeEndLocationY;

        //write probe transform to place paddle
        overlord.GetComponent<Overlord>().paddleTransform = paddleTransform;
        


        
        if(isTesting==true)
        {
            delay = 20;
        }
        StartCoroutine(LoadLevelAfterDelay(delay));

        // Overlord controllerScript = overlord.GetComponent<Overlord>();
        // controllerScript.sceneIndex = SceneManager.GetActiveScene().buildIndex;
        // controllerScript.probeStart =  probeTransform.x;
        // controllerScript.apertureDegree = apertureScale.x;

    }

    // Update is called once per frame
    void Update()
    {
        Vector3 velocity = new Vector3(0.0f, 0.0f, egoVelocity);
        Vector3 displacement = velocity * Time.deltaTime;
        transform.localPosition += displacement;
    }

    IEnumerator LoadLevelAfterDelay(float delay)
     {
         yield return new WaitForSeconds(delay);
         SceneManager.LoadScene("PaddleDisplay");
     }

}
