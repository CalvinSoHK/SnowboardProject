using UnityEngine;
using System.Collections;

public class CameraScriptv2 : MonoBehaviour {

    public GameObject board;
    bool isGrounded;
    float airTimer;
    Vector3 forward;
    float vertical;
    float shakeTimer = 0;
	
	// Update is called once per frame
	void Update () {
        vertical = board.GetComponent<SnowboardScript>().vertical;
        isGrounded = board.GetComponent<SnowboardScript>().isGrounded;

        if (isGrounded)
        {
            forward = board.transform.forward;
            transform.localPosition = Vector3.Lerp(transform.localPosition, new Vector3 (0, 0.5f, -1.5f - vertical * 0.5f) , 0.05f);
        }
        else
        {
            transform.position = Vector3.Lerp(transform.position, board.transform.position - forward * (8), 0.2f);
        }

        transform.LookAt(board.transform);
    }

}
