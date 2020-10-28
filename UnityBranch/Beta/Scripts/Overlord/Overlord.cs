using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Overlord : MonoBehaviour
{
    public int sceneIndex;
    public string conditionName;
    public float probeStartLocation; // is always on the Z axis so Y=0
    public float probeEndLocationX;
    public float probeEndLocationY;
    public float probeAngle;
    public float probeVelX;
    public float probeVelY;
    public float apertureDegree;
    public float absoluteTilt;
    public float relativeTilt;
    public float probeEccentricity;
    public float radialFlowAtStart;
    public float radialFlowAtEnd;
    public float averageRadialFlow;
    public float reactionTime;
    public float stimulationTime;
    
    
    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    
    }
}
