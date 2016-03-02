using UnityEngine;
using System.Collections;

public class SnowboardScript : MonoBehaviour {

    public float turnSpeed;
    public float defaultForwardSpeed;
    public float tiltDegree;
    public float tiltSpeed;
    public float jumpStrength;
    public float initialJump;
    public float turnAngle;
    public float maxSpeed;
    public float slopeAngle;
    bool drop = false;
    float jumpTimer = 0f;
    public float upTime = 0f;
    float airTimer = 0;
    public float dropStrength;
    public bool isGrounded = false;
    public float acceleration;
    public float vertical;

    Vector3 rightTurnRotation = new Vector3(0, 90, -30);
    Vector3 leftTurnRotation = new Vector3(0, -90, 30);
	// Update is called once per frame
	void Update () {

        float horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");
        float yValue = -9.8f;
        turnAngle = horizontal * turnSpeed * Time.deltaTime;
        Vector3 currentRotation = transform.rotation.eulerAngles;
        currentRotation.y += turnAngle;
        //currentRotation.y = Mathf.Clamp(currentRotation.y, -90, 90);
        transform.rotation = Quaternion.Euler(currentRotation);

        Vector3 turnMovement = transform.right * turnSpeed * horizontal;
        Vector3 snowboardRotation = transform.localRotation.eulerAngles;

        snowboardRotation.z = horizontal * -tiltDegree;
        snowboardRotation.x = slopeAngle;

        Vector3 targetRotation = new Vector3(10, 0, 0);

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            jumpTimer = upTime - 0.1f;
            yValue += initialJump;
            drop = false;
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
        if(drop)
        {
            airTimer += Time.deltaTime;
            yValue += dropStrength + airTimer * -9.8f; 
        }

        float additionalSpeed = Mathf.Lerp(0, maxSpeed, acceleration);


        //Debug.Log(Time.deltaTime);
        Quaternion newRotation = Quaternion.Euler(snowboardRotation);
        transform.rotation = Quaternion.Lerp(transform.localRotation, newRotation, tiltSpeed);

        Vector3 newVelocity = transform.forward * (defaultForwardSpeed + additionalSpeed * vertical) * 100 * Time.deltaTime;
        newVelocity.y = yValue;
        Debug.Log(newVelocity);
        GetComponent<Rigidbody>().velocity = Vector3.Lerp(GetComponent<Rigidbody>().velocity, newVelocity, 0.1f);

    }

    void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.tag == "Ground")
        {
            isGrounded = true;
            slopeAngle = collision.collider.GetComponent<SlopeInfo>().slopeDegree;
            jumpTimer = 0;
            drop = false;
            airTimer = 0;
        }
        if(collision.collider.tag == "Wall" )
        {
            isGrounded = true;
            drop = true;
        }
        if(collision.collider.tag == "Rock")
        {
            isGrounded = true;
            jumpTimer = 0;
            drop = true;
            airTimer = 0;
        }
    }

    void OnCollisionExit(Collision collision)
    {
        if(collision.collider.tag == "Ground")
        {
            isGrounded = false;
            jumpTimer += 0.00000001f;
        }
    }
}
