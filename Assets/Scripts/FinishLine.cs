using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class FinishLine : MonoBehaviour {

    Text infoText;
    float score;

	// Use this for initialization
	void Start () {
        infoText = GameObject.Find("Canvas").transform.GetChild(1).GetComponent<Text>();
    }
	
	// Update is called once per frame
	void Update () {

    }
    
    void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Player")
        {
            infoText.text = "You reached the End!";
            score = int.Parse(GameObject.Find("Canvas").transform.GetChild(0).GetComponent<Text>().text);
            PlayerPrefs.SetFloat("score", score);
            SceneManager.LoadScene("Finish");
        }
    }
}
           
