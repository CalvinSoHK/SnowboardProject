using UnityEngine;
using System.Collections;

public class SlopeInfo : MonoBehaviour {

    public float slopeDegree;

    void Start()
    {
        slopeDegree = transform.eulerAngles.x;
    }

}
