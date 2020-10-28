using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{

    public Transform prefab;
    public int cloudParticleCount;
    public OpticSpawnZone FieldDefine;

    //instantiate objects across the field using valid positions
    private void Awake()
    {
        for(int i = 1; i<=1500; i++)
        {
            InstantiateFlow();
        }
    }

    void InstantiateFlow()
    {
        Transform t = Instantiate(prefab);
        t.localPosition = FieldDefine.SpawnPoint;
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
