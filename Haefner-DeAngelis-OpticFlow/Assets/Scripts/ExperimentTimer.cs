// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// using UnityEngine.UI;

// public class ExperimentProgressDisplay : MonoBehaviour
// {

//     public float timeElapsed;
//     public int trialNumber;
//     public Text timer;
//     public Text counter;
//     void Awake()
//     {
//         GameObject overlord = GameObject.Find("ExperimentController");
//         timeElapsed = overlord.GetComponent<Overlord>().timeElapsed;
//         trialNumber = overlord.GetComponent<Overlord>().trialNumber;
//     }
    
//     // Start is called before the first frame update
//     void Start()
//     {
//         timer.text = "Time Elapsed: " +timeElapsed;
//         counter.text = "Trial Progress: " + trialNumber + "/100";
//     }

//     // Update is called once per frame
//     void Update()
//     {
//         timeElapsed = overlord.GetComponent<Overlord>().timeElapsed;
//         timer.text = "Time Elapsed: " + timeElapsed + " Seconds";
//     }
// }
