using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SpawnZone : MonoBehaviour
{

    //[SerializeField]
    //bool surfaceOnly;

    public abstract Vector3 SpawnPoint { get; }
    //	get {
    //		return transform.TransformPoint(
    //			surfaceOnly ? Random.onUnitSphere : Random.insideUnitSphere
    //		);
    //	}
    //}

    //void OnDrawGizmos () {
    //	…
    //}
}
