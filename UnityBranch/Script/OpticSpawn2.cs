﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpticSpawn2 : SpawnZone
{
    // The following parameters define the limits of the spawn zone, a set of concentric cylinders where the center is a no-spawn zone
    public float innerRadius;
    public float outerRadius;
    public float nearEdge;
    public float farEdge;

    // The following parameters designate the prefab and the number of instantiations
    public Transform prefab;
    public int cloudParticleCount;

    // The following vector is used to instantiate each transform
    Vector3 p = Vector3.zero;

    
    //PlayerScript egoMotion = cameraController.GetComponent<EgoMotion>();
    public float apertureSize;

    //Vector3 probeTransformation = cameraController.GetComponent<EgoMotion>().probeTransform;

    float apertureFloorX = 0.0f;
    float apertureCeilingX = 0.0f;
    float apertureFloorY = 0.0f;
    float apertureCeilingY = 0.0f;

    Vector3 probeTransformation = Vector3.zero;

    // The following function will attempt to find a valid spawn point for the particle
    public override Vector3 SpawnPoint
    {
        get
        {
            // Reinitializes the position vector
            p = Vector3.zero;

            // Attempt to return a transform within the outer cylinder
            p.x = Random.Range(-outerRadius, outerRadius);
            p.y = Random.Range(-outerRadius, outerRadius);

            // If the coordinates lie within the innerRadius, reroll for them
            while( (p.x <= innerRadius && p.x >= -innerRadius) && (p.y <=innerRadius && p.y >= -innerRadius) && !(p.x <= apertureCeilingX && p.x >= apertureFloorX) && !(p.y <= apertureCeilingY && p.y >= apertureFloorY) )
            //while( (p.x <= innerRadius && p.x >= -innerRadius) && (p.y <=innerRadius && p.y >= -innerRadius) )
            {
                p.x = Random.Range(-outerRadius, outerRadius);
                p.y = Random.Range(-outerRadius, outerRadius);
            }

            p.z = Random.Range(nearEdge, farEdge);

            return transform.TransformPoint(p);
            

        }
    }

    private void Awake()
    {
        // Reading variables from the CameraController script
        GameObject cameraController = GameObject.Find("CameraController");

        Vector3 probeTransformation = cameraController.GetComponent<EgoMotion>().probeTransform;

        apertureFloorX = probeTransformation.x - apertureSize;
        apertureCeilingX = probeTransformation.x + apertureSize;
        apertureFloorY = probeTransformation.y - apertureSize;
        apertureCeilingY = probeTransformation.x + apertureSize;
        
        // This code will attempt to instantiate the prefabs
        for (int i = 1; i <= cloudParticleCount; i++)
        {
            InstantiateFlow();
        }
    }

    // This is the code used to instantiate the prefabs
    void InstantiateFlow()
    {
        Transform t = Instantiate(prefab);
        t.localPosition = SpawnPoint;
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