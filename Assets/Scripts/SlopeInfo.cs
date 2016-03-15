using UnityEngine;
using System.Collections;

public class SlopeInfo : MonoBehaviour {

    public float slopeDegree;
    public Vector3 forwardRotation;

    void Start()
    {
        slopeDegree = transform.eulerAngles.x;
        forwardRotation = transform.eulerAngles;
    }

}
