using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSampler : MonoBehaviour
{
    
int RNGCondition;
int RNGVector;
public float waitTime;

    void RNGesus()
    {
        RNGCondition = Random.Range(0,6);
        RNGVector = Random.Range(0,2);

    }
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.Return))
        {
            SceneManager.LoadScene("FullFlow");
        }
    }
}
