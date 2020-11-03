using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExperimentProgressDisplay : MonoBehaviour
{

    public float timeElapsed;
    public int trialNumber;
    public Text timer;
    public Text counter;

    float minutes;
    float seconds;

    //GameObject overlord = GameObject.Find("ExperimentController");
   
   void timeConvertor()
   {
       GameObject overlord = GameObject.Find("ExperimentController");
       timeElapsed = overlord.GetComponent<Overlord>().timeElapsed;

        minutes = Mathf.FloorToInt(timeElapsed / 60);
        seconds = Mathf.FloorToInt(timeElapsed % 60);

   }
    void Awake()
    {
        GameObject overlord = GameObject.Find("ExperimentController");
        timeElapsed = overlord.GetComponent<Overlord>().timeElapsed;
        trialNumber = overlord.GetComponent<Overlord>().trialNumber;
    }
    
    // Start is called before the first frame update
    void Start()
    {
        GameObject overlord = GameObject.Find("ExperimentController");
        timer.text = "Time Elapsed: " +timeElapsed;
        counter.text = "Trial Progress: " + trialNumber + "/100";
    }

    // Update is called once per frame
    void Update()
    {
        //GameObject overlord = GameObject.Find("ExperimentController");

        timeConvertor();
        //timer.text = "Time Elapsed: " + minutes + ":" + seconds;

        if(minutes < 10)
        {
            if(seconds<10)
            {
                timer.text = "Time Elapsed: 0" + minutes + ":0" + seconds;
            }
            else
            {
                timer.text = "Time Elapsed: 0" + minutes + ":" + seconds;
            }
        }
        else
        {
            if(seconds <10)
            {
                timer.text = "Time Elapsed: 0" + minutes + ":0" + seconds;
            }
            else
            {
                timer.text = "Time Elapsed: " + minutes + ":" + seconds;
            }
        }
        

    }
}
