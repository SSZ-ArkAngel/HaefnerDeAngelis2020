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
    public float probeEccentricity;
    // public float radialFlowAtStart;
    // public float radialFlowAtEnd;
    // public float averageRadialFlow;
    public float reactionTime;
    public float stimulusTime;

    public Vector3 paddleTransform;
    public int trialNumber = 1;

    public float timeElapsed = 0.0f;
    
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

    string[] volatileData = new string[16];
    int varCount = 16;

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
    
    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
    void Start()
    {
        //InitializeData();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.Home))
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
