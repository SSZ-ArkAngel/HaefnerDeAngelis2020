using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Overlord : MonoBehaviour
{
    public int sceneIndex;
    public string conditionName;
    public float probeStartLocationX; // is always on the Z axis so Y=0
    public float probeStartLocationY;
    // Start Location for Exp1 and Ecc1 = 1 unit, Ecc2 = 1.5 units
    public float probeEndLocationX; // can be pseudo-standardized 
    // Probe Velocity is 0.2
    // When angle = 0, x = -0 per second, y = -0.2 per second
    // When angle = -15, x = -0.2*sin(15d) per second, y = -0.2*cos(15d) per second
    // When angle = 15, x = 0.2*sin(15d) per second, y = -0.2*cos(15d) per second  
    public float probeEndLocationY;
    public float probeAngle;
    public float probeVelX;
    // X vel = Velocity * sin(angle)
    public float probeVelY;
    // Y vel = Velocity * cos(angle)
    public float apertureDegree;
    public float absoluteTilt;
    public float relativeTilt;
    public float paddleRotation;
    public float probeEccentricity;
    // public float radialFlowAtStart;
    // public float radialFlowAtEnd;
    // public float averageRadialFlow;
    public float reactionTime;
    public float stimulusTime;

    public Vector3 paddleTransform;
    public int trialNumber = 1;

    public float timeElapsed = 0.0f;

    public float dotsPerInch;
    public float dotsPerCM;
    public float screenWidthPixels;
    public float screenWidth;

    public float unitsPerDegree;
    public float unitsPerCM;

    float nearClipPlane = 2f;
    float horizontalFOV = 90f;
    float unitsPerScreen;

    public float oneDegree;
    public float oneCM;

    public float viewPortScale;

    
    public void ScreenReader()
    {
        dotsPerInch = Screen.dpi;
        dotsPerCM = dotsPerInch/2.54f;
        screenWidthPixels = Screen.width;
        screenWidth = screenWidthPixels/dotsPerCM;

        

        // Here we will convert to all quantities

        // Units per degree is overwritten
        //unitsPerDegree = 0.045f;

        //UnitScreenConversions
        UnityUnits();

        //Screen = 90 degrees
        unitsPerCM = unitsPerScreen/screenWidth;
        unitsPerDegree = unitsPerCM;

        oneDegree = unitsPerDegree;
        oneCM = unitsPerCM;

        //degrees per cm = unitsPerDegree/unitsPerCM

    }

    public void ViewPortCalculation()
    {
        // For the viewport 1.275 unit = 1/3 of the screen
        // 1 screen = 3.825 units (almost 4) hmm...
        // To get the calculations done
        // Read screen size, convert 30 degrees to cm, then get a ration

        //At 56 cm away 1 deg = 1 cm use this

        float thirtyDegreesInCM;

        //ratio with screensize

        float apertureScreenRatio = 30f/screenWidth;

        viewPortScale = 3.825f * apertureScreenRatio;

    }

    public void UnityUnits()
    {
        // Override as the FOV is fixed at 90H
        unitsPerScreen = nearClipPlane*2f;
    }

    // All setup variables are stored here in degrees

    // Order
    // Condition, Aperture, Eccentricity, Relative Tilt, Misc Raw Data    
    // void ExportDataToCSV()
    // {
    //     var obj = new List<MyObject>()
    //     {
    //         new MyObject("Trial", 1)
    //     };
    //     Sinbad.CsvUtil.SaveObject(obj, "subjectData.csv");
    // }

    string[] volatileData = new string[17];
    int varCount = 17;

    public void InitializeData()
    {
        volatileData[0] = conditionName;
        volatileData[1] = ""+apertureDegree;
        volatileData[2] = ""+probeEccentricity;
        volatileData[3] = ""+relativeTilt;
        volatileData[4] = ""+trialNumber;
        volatileData[5] = ""+probeAngle;
        volatileData[6] = ""+absoluteTilt;
        volatileData[7] = ""+reactionTime;
        volatileData[8] = ""+stimulusTime;
        volatileData[9] = ""+probeVelX;
        volatileData[10] = ""+probeVelY;
        volatileData[11] = ""+probeStartLocationX;
        volatileData[12] = ""+probeStartLocationY;
        volatileData[13] = ""+probeEndLocationX;
        volatileData[14] = ""+probeEndLocationY;
        volatileData[15] = ""+sceneIndex;
        volatileData[16] = ""+paddleRotation;
    }

    public void WriteData()
    {
        
        InitializeData();

        using (StreamWriter sw = File.AppendText(Application.dataPath + "SubjectData.csv"))
        {
            for(int i = 0; i<varCount; i++)
            {
                sw.Write(volatileData[i]);
                sw.Write(",");
            }
            sw.Write("\n");
            sw.Close();
        }

        trialNumber += 1;
    }

    void WriteHeader()
    {
        using (StreamWriter sw = File.AppendText(Application.dataPath + "SubjectData.csv"))
        {
            sw.Write("conditionName,apertureDegree,probeEccentricity,relativeTilt,trialNumber,probeAngle,absoluteTilt,reactionTime,stimulusTime,probeVelX,probeVelY,probeStartLocationX,probeStartLocationY,probeEndLocationX,probeEndLocationY,sceneIndex,paddleRotation");
            sw.Write("\n");
            sw.Close();
        }
        
    }
    
    void Awake()
    {
        DontDestroyOnLoad(gameObject);
        WriteHeader();
        ScreenReader();
        ViewPortCalculation();
        UnityUnits();
    }
    void Start()
    {
        //InitializeData();

        Debug.Log("Screen Size: " + screenWidth);
        Debug.Log("Conversions: CM " + oneCM + " Degrees " + oneDegree + "port " + viewPortScale);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.Return))
        {
            SceneManager.LoadScene("Fixation");
        }

        timeElapsed += Time.deltaTime;
    }

    private string getPath()
    {
        #if UNITY_EDITOR
        return Application.dataPath +"/CSV/"+"Saved_data.csv";
        #elif UNITY_ANDROID
        return Application.persistentDataPath+"Saved_data.csv";
        #elif UNITY_IPHONE
        return Application.persistentDataPath+"/"+"Saved_data.csv";
        #else
        return Application.dataPath +"/"+"Saved_data.csv";
        #endif
    }

}
