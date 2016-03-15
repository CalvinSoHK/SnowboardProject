using UnityEngine;
using System.Collections;

public class CameraScript : MonoBehaviour {

    public GameObject board;
    bool isGrounded;
    bool behindPlayer = true;
    float airTimer;
    Vector3 forward;
    float vertical;

	// Update is called once per frame
	void Update () {
        vertical = board.GetComponent<SnowboardScript>().vertical;

        isGrounded = board.GetComponent<SnowboardScript>().isGrounded;
        if(isGrounded && !behindPlayer)
        {
            transform.position = Vector3.Lerp(transform.position, board.transform.position - board.transform.forward * (8 + vertical * 2) + new Vector3(0, 3f, 0), 0.2f);
            transform.LookAt(board.transform);
            forward = board.transform.forward;
            airTimer = 0;
            float dist = Vector3.Distance(transform.position, board.transform.position - board.transform.forward * (8 + vertical * 2) + new Vector3(0, 3f, 0));
            Debug.Log(dist);
            if (dist < 0.75f)
            {
                behindPlayer = true;
            }
        }
        if (isGrounded )//&& behindPlayer)
        {
            transform.position = board.transform.position - board.transform.forward * (8 + vertical * 2) + new Vector3(0, 3f, 0);
            transform.LookAt(board.transform);
            forward = board.transform.forward;
        }
        else if (airTimer < 0.1f)
        {
            transform.position = board.transform.position - board.transform.forward * (8 + vertical * 2) + new Vector3(0, 3f, 0);
            transform.LookAt(board.transform);
            forward = board.transform.forward;
            airTimer += Time.deltaTime;
        }
        else if (airTimer > 0.1f) 
        {
            behindPlayer = false;
            transform.position = board.transform.position - forward * (8 + vertical * 2) + new Vector3(0, 3f, 0);
            transform.LookAt(board.transform);
            airTimer += Time.deltaTime;
        }
	}
}
