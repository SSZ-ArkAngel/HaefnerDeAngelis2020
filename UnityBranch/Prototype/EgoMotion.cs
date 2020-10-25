using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EgoMotion : MonoBehaviour
{
    public float egoVelocity;

    // Fetch the coordinates for the probe's location
    public Vector3 probeTransform;
    public Vector3 apertureScale;

    public Transform probePrefab;
    public Transform aperturePrefab;
    // Start is called before the first frame update

    public GameObject motionController;

    public bool spawnProbe;
    public bool spawnAperture;
    
    void Awake()
    {
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

    }
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 velocity = new Vector3(0.0f, 0.0f, egoVelocity);
        Vector3 displacement = velocity * Time.deltaTime;
        transform.localPosition += displacement;
    }
}
