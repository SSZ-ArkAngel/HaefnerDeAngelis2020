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

    int apertureScaleIndex;
    int eccentricityScaleIndex;
    // Conditions:
    // Aperture Size: 0 = 0.4; 1 = 0.5; 2 = 0.6; 3 = 0. 7;
    // Eccentricity: 0 = 1; 1 = 1.5;
    // Motion: 0 = -15; 1 = 0; 2 = 15;

    void RNGesus()
    {

        apertureScaleIndex = Random.Range(0,4);
        eccentricityScaleIndex = Random.Range(0,2);

        if(publicOverride==false)
        {
            if(Exp1 == true)
            {
                //Eccentricity Set Constant @ (1,0,2)
                // probeTransform = (1.0f, 0.0f, 2.0f);
                probeTransform.x = 1.0f;
                probeTransform.y = 0.0f;
                probeTransform.z = 2.0f;

                if(apertureScaleIndex==0)
                {
                    // apertureScale = (0.4f, 0.4f, 0.01f);
                    apertureScale.x = 0.4f;
                    apertureScale.y = 0.4f;
                    apertureScale.z = 0.01f;
                }

                if(apertureScaleIndex==1)
                {
                    // apertureScale = (0.5f, 0.5f, 0.01f);
                    apertureScale.x = 0.5f;
                    apertureScale.y = 0.5f;
                    apertureScale.z = 0.01f;
                }

                if(apertureScaleIndex==2)
                {
                    // apertureScale = (0.6f, 0.6f, 0.01f);
                    apertureScale.x = 0.6f;
                    apertureScale.y = 0.6f;
                    apertureScale.z = 0.01f;
                }

                if(apertureScaleIndex==3)
                {
                    // apertureScale = (0.7f,0.7f, 0.01f);
                    apertureScale.x = 0.8f;
                    apertureScale.y = 0.8f;
                    apertureScale.z = 0.01f;
                }
                
            }

            if(Exp1==false)
            {
                // Aperture is constat @ (0.0f, 0.0f, 0.0f)
                apertureScale.x = 0.0f;
                apertureScale.y = 0.0f;
                apertureScale.z = 0.0f;

                if(eccentricityScaleIndex==0)
                {
                    // transform = (1, 0, 2)
                    probeTransform.x = 1.0f;
                    probeTransform.y = 0.0f;
                    probeTransform.z = 2.0f;
                }

                if(eccentricityScaleIndex==1)
                {
                    // transform = (1.5, 0, 2)
                    probeTransform.x = 1.5f;
                    probeTransform.y = 0.0f;
                    probeTransform.z = 2.0f;
                }
            }
        }
    }
    
    void Awake()
    {
        
        probeAngleIndex = Random.Range(0,3);
        RNGesus();

        // Instantiate a probe at the presented coordinates
        if(spawnProbe == true)
        {
        Transform probe = Instantiate(probePrefab);
        probe.localPosition = probeTransform;
        probe.transform.parent = motionController.transform; 
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
