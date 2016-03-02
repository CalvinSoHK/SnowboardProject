using UnityEngine;
using System.Collections;

public class CameraScript : MonoBehaviour {

    public GameObject board;
    float vertical;

	// Update is called once per frame
	void Update () {
        vertical = board.GetComponent<SnowboardScript>().vertical;
        transform.position = board.transform.position - board.transform.forward * (8 + vertical * 2) + new Vector3(0, 3f, 0);
        transform.LookAt(board.transform);
	}
}
