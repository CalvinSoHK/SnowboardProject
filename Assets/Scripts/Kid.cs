using UnityEngine;
using UnityEngine.UI; 
using System.Collections;

public class Kid : MonoBehaviour {

    public float speed;
    public float pointValue = 100f;
    Text infoText;
    Text scoreText;
    bool flying = false;
    public string[] message;
    AudioSource audio;

	// Update is called once per frame
    void Start()
    {
        infoText = GameObject.Find("Canvas").transform.GetChild(1).GetComponent<Text>();
        scoreText = GameObject.Find("Canvas").transform.GetChild(0).GetComponent<Text>();
        audio = GetComponent<AudioSource>();
    }

	void Update () {
        GetComponent<Rigidbody>().velocity = new Vector3(0, -9.8f, speed);

        if (flying)
        {
            GetComponent<Rigidbody>().AddForce(0, 1500, 0);
        }
	}
   
    void OnCollisionEnter(Collision collision)
    {
        //Debug.Log(collision.collider.tag);
        if (collision.collider.tag == "Player")
        {
            int randomNumber = (int)(Random.value * message.Length);
            //Debug.Log(message[randomNumber]);
            infoText.text = message[randomNumber];
            GetComponentInChildren<Collider>().isTrigger = true;
            GetComponent<Rigidbody>().AddForce(0, 1000, 0);
            flying = true;
            int x = int.Parse(scoreText.text);
            scoreText.text = (x + pointValue).ToString();
            audio.Play();
            int temp = PlayerPrefs.GetInt("Kids");
            PlayerPrefs.SetInt("Kids", temp + 1);
            pointValue = 0;
        }
    }
}
