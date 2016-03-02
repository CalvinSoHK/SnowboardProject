using UnityEngine;
using System.Collections;

public class SnowboardV2 : MonoBehaviour {

    CharacterController charCont;
    float speed = 20f;
    float turnSpeed = 10f;
    float slideSpeed = 5f;
    float tiltDegree = 20f;
    float maxTilt = 30f;
    float zRotation;

	// Use this for initialization
	void Start () {
        charCont = GetComponent<CharacterController>();
	}
	
	// Update is called once per frame
	void Update () {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        charCont.Move(new Vector3(0, -9.8f, 0) * Time.deltaTime);
        charCont.Move(((transform.forward * vertical * speed) + (transform.forward * slideSpeed)) * Time.deltaTime);
        transform.Rotate(new Vector3(0, horizontal * turnSpeed, 0) * Time.deltaTime);
        transform.GetChild(0).Rotate(new Vector3(0, 0, -horizontal * tiltDegree) * Time.deltaTime);
        //zRotation = transform.eulerAngles.z;
        //Mathf.Clamp(zRotation, -maxTilt, maxTilt);
        //transform.rotation = Quaternion.Euler(transform.rotation.x, transform.rotation.y, zRotation);
	}
}
