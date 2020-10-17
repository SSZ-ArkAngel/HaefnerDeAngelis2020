using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// The SpawnZone script seeks to define spatial coordinates within which objects may be instantiated
// While configurable replication suggests a 2M radius centered at the camera as a no-spawn-zone

public class OpticSpawnZone : SpawnZone
{

    // The inner and outer radii of the concentric cylinder in which objects will be instantiated
    public float FlowInnerLimit;
    public float FlowOuterLimit;
    // The length of the optic flow field
    public float FlowFieldLength;
    // The point at which optic flow can start to appear
    public float FlowStartPoint;

    public Transform prefab;
    public int cloudParticleCount;

    // Will attempt to return spatial coordinates as potential instantiation point candidates
    public override Vector3 SpawnPoint
    {
        get
        {
            Vector3 p = Vector3.zero;

            // The while loop will exit once the spawn point is within the defined bounds
            //bool inBounds = false;
            //while (inBounds = false)
            //{
            //    // The XY coordinates directly reflect the radius
            //    p.x = Random.Range(-FlowOuterLimit, FlowOuterLimit);
            //    p.y = Random.Range(-FlowOuterLimit, FlowOuterLimit);

            //    if((p.x > FlowInnerLimit || p.x < -FlowInnerLimit) && (p.y > FlowInnerLimit || p.y < -FlowInnerLimit))
            //    {
            //        inBounds = true;
            //    }
            //}


            p.x = Random.Range(-FlowOuterLimit, FlowOuterLimit);
            p.y = Random.Range(-FlowOuterLimit, FlowOuterLimit);

            //bool posneg = false;
            //int multiplier = 1;
            //float booltest = Random.Range(0.0f, 1.0f);
            //if (booltest >= 0.5f)
            //{
            //    posneg = true;
            //}
            //else posneg = false;

            //if(posneg == true)
            //{
            //    multiplier = 1;
            //}
            //if(posneg == false)
            //{
            //    multiplier = -1;
            //}

            //p.x = multiplier * Random.Range(FlowInnerLimit, FlowOuterLimit);
            //p.y = multiplier * Random.Range(FlowInnerLimit, FlowOuterLimit);

            //float xposneg = Random.Range(0.0f, 1.0f);
            //int xmult = 0;
            //int ymult = 0;
            //if(xposneg <= 0.5f)
            //{
            //    xmult = 1;
            //}
            //if(xposneg > 0.5f)
            //{
            //    xmult = -1;
            //}

            //float yposneg = Random.Range(0.0f, 1.0f);
            //if (yposneg <= 0.5f)
            //{
            //    ymult = 1;
            //}
            //if (yposneg > 0.5f)
            //{
            //    ymult = -1;
            //}

            //p.x = xmult * Random.Range(FlowInnerLimit, FlowOuterLimit);
            //p.y = ymult * Random.Range(FlowInnerLimit, FlowOuterLimit);

            // The Z coordinate is reflective of the length
            p.z = Random.Range(FlowStartPoint, FlowFieldLength);

            return transform.TransformPoint(p);

        }
    }

    private void Awake()
    {
        for (int i = 1; i <= cloudParticleCount; i++)
        {
            InstantiateFlow();
        }
    }

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
