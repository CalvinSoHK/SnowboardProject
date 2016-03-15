using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class EndScreen : MonoBehaviour {

    public Text finishText;

	// Use this for initialization
	void Start () {
        string printText = "You reached the finish line!";
        printText += "\n\n You hit " + PlayerPrefs.GetInt("Kids") + " kids on the way down!";
        printText += "\n\n You bounced off " + PlayerPrefs.GetInt("Rocks") + " rocks!";
        printText += "\n\n You gained " + PlayerPrefs.GetFloat("score") + " points!";
        finishText.text = printText;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
