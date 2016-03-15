using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SnowboardScript : MonoBehaviour {

    public float groundTurnSpeed, airTurnspeed, upTime = 0f, dropStrength, floatStrength, acceleration, tiltDegree, tiltSpeed, jumpStrength, initialJump,
        maxSpeed, slopeAngle, vertical, bounceStrength;
    public Vector3 slopeForward;
    public bool isGrounded = false;
    Vector3 forward, bounceVector;
    Text infoText;
    Text scoreText;
    Text directionText;
    float turnAngle, groundTimer, slopeMaxSpeed, accel, currentSpeed, jumpTimer = 0f, airTimer = 0, multiplier = 0;
    bool drop = false, bounce = false;
    AudioSource audio;

    void Start()
    {
        infoText = GameObject.Find("Canvas").transform.GetChild(1).GetComponent<Text>();
        scoreText = GameObject.Find("Canvas").transform.GetChild(0).GetComponent<Text>();
        directionText = GameObject.Find("Canvas").transform.GetChild(2).GetComponent<Text>();
        accel = acceleration;
        audio = GetComponent<AudioSource>();
    }

	// Update is called once per frame
	void Update () {
        float turnSpeed;

        if((transform.rotation.eulerAngles.y < 90 && transform.rotation.eulerAngles.y > 0) || 
                (transform.rotation.eulerAngles.y < 360 && transform.rotation.eulerAngles.y > 270))
        {
            directionText.text = "Downhill";
        }
        else
        {
            directionText.text = "WARNING: UPHILL!";
        }


        if (groundTimer > 0)
        {
            groundTimer -= Time.deltaTime;
            if (groundTimer < 0)
            {
                isGrounded = false;
            }
        }

        if (isGrounded)
        {
            turnSpeed = groundTurnSpeed;
        }
        else
        {
            turnSpeed = airTurnspeed;
        }
        float horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");
        float yValue = -9.8f;
        turnAngle = horizontal * turnSpeed * Time.deltaTime;
        Vector3 currentRotation = transform.rotation.eulerAngles;
        currentRotation.y += turnAngle;
        //currentRotation.y = Mathf.Clamp(currentRotation.y, -90, 90);
        transform.rotation = Quaternion.Euler(currentRotation);
           
        Vector3 snowboardRotation = transform.localRotation.eulerAngles;

        snowboardRotation.z = horizontal * -tiltDegree;
        snowboardRotation.x = slopeAngle;

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            jumpTimer = upTime - 0.1f;
            yValue += initialJump;
            drop = false;
            isGrounded = false;
            forward = transform.forward;
            audio.Play();
        }


        if (jumpTimer > 0 && !drop)
        {
            yValue += jumpStrength;
            jumpTimer -= Time.deltaTime;
            if (jumpTimer < 0)
            {
                drop = true;           
            }
        }
        if (bounce)
        {
            airTimer += Time.deltaTime;
            bounceVector = transform.up * bounceStrength;
            //yValue += floatStrength + airTimer;
            if(airTimer >= 0.2f)
            {
                bounce = false;
                airTimer = 0;
            }

        }
        else if (drop)
        {
            airTimer += Time.deltaTime;
            yValue += dropStrength + airTimer * -9.8f; 
        }

   

        //Debug.Log(Time.deltaTime);
        Quaternion newRotation = Quaternion.Euler(snowboardRotation);
        transform.rotation = Quaternion.Lerp(transform.localRotation, newRotation, tiltSpeed);
        if (isGrounded)
        {
            if (vertical > 0 && currentSpeed < slopeMaxSpeed)
            {
                currentSpeed += accel;
            }
            else if (vertical == 0)
            {
                if (currentSpeed > slopeMaxSpeed - 2)
                {
                    currentSpeed -= (0.5f * accel);
                }
                else if(currentSpeed < slopeMaxSpeed - 2)
                {
                    currentSpeed += accel * 0.5f;
                }
            }
            else if (currentSpeed > 0.1f || currentSpeed > slopeMaxSpeed)
            {
                currentSpeed -= accel;
            }
            //Debug.Log(currentSpeed);
        }
        Vector3 newVelocity = forward * (currentSpeed) * 100 * Time.deltaTime;
        newVelocity.y = yValue;
        newVelocity += bounceVector;
        bounceVector = new Vector3(0, 0, 0);
        GetComponent<Rigidbody>().velocity = Vector3.Lerp(GetComponent<Rigidbody>().velocity, newVelocity, 0.1f);
        //Debug.Log(currentSpeed);
    }

    void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.tag == "Ground")
        {
            groundTimer = -0.1f ;
            isGrounded = true;
            slopeAngle = collision.collider.GetComponent<SlopeInfo>().slopeDegree;
            //Debug.Log(slopeAngle);
            if(slopeAngle < 180 && slopeAngle != 0) { slopeMaxSpeed = maxSpeed + slopeAngle * 0.3f; accel = acceleration + slopeAngle * 0.1f; }
            if(slopeAngle == 0) { slopeMaxSpeed = maxSpeed; accel = acceleration; }
           
            //Debug.Log(slopeMaxSpeed);
            slopeForward = collision.collider.GetComponent<SlopeInfo>().forwardRotation;
            jumpTimer = 0;
            drop = false;
            airTimer = 0;
            multiplier = 1;
            
            
        }
        if(collision.collider.tag == "Wall" )
        {
            isGrounded = true;
            drop = true;
        }
        if(collision.collider.tag == "Rock")
        {
            isGrounded = false;
            jumpTimer = 0;
            drop = true;
            airTimer = 0;
            bounce = true;
            int x = int.Parse(scoreText.text);
            scoreText.text = (x + 50 * multiplier).ToString();
            multiplier++;
            printRockBounce();
            int temp = PlayerPrefs.GetInt("Rocks");
            PlayerPrefs.SetInt("Rocks", temp + 1);
        }
    }

    void OnCollisionExit(Collision collision)
    {
        if(collision.collider.tag == "Ground")
        {
            //isGrounded = false;
            forward = transform.forward;
            groundTimer = 2f;
            jumpTimer += 0.00000001f;
        }
    }

    void printRockBounce()
    {
        string[] message = { "Bounce!", "Up we go!", "I'm FREE!", "This is SO REAL." };
        int randomNumber = (int)(Random.value * message.Length - 0.1);
        infoText.text = message[randomNumber];
    }
}
