using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialPaddleRotator : MonoBehaviour
{

    public Vector3 point;   
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        if(Input.GetKey(KeyCode.RightArrow))
        {
            transform.RotateAround(point, Vector3.forward, 20*Time.deltaTime);
        }

        if(Input.GetKey(KeyCode.LeftArrow))
        {
            transform.RotateAround(point, Vector3.back, 20*Time.deltaTime);
        }

        if(Input.GetKey(KeyCode.RightArrow) && Input.GetKey(KeyCode.RightShift))
        {
            transform.RotateAround(point, Vector3.forward, 80*Time.deltaTime);
        }

        if(Input.GetKey(KeyCode.LeftArrow) && Input.GetKey(KeyCode.RightShift))
        {
            transform.RotateAround(point, Vector3.back, 80*Time.deltaTime);
        }
    }
}
