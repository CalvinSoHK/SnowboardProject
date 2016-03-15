using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class TitleScreen : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	    if(Input.GetKeyDown(KeyCode.Space))
        {
            PlayerPrefs.SetInt("Kids", 0);
            PlayerPrefs.SetInt("Rocks", 0);
            SceneManager.LoadScene("SnowboardPrototype");
        }
	}
}
