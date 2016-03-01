using UnityEngine;
using System.Collections;

public class CameraScript : MonoBehaviour {

    public GameObject board;

	// Update is called once per frame
	void Update () {
        transform.position = board.transform.position - board.transform.forward * 6 + new Vector3(0, 3f, 0);
        transform.LookAt(board.transform);
	}
}
