using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collidor : MonoBehaviour
{
    
    void OnTriggerEnter(Collider collision)
    {
        Destroy(collision.gameObject);
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
