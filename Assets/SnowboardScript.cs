using UnityEngine;
using System.Collections;

public class SnowboardScript : MonoBehaviour {

    public float turnSpeed;
    public float forwardSpeed;
    public float tiltDegree;
    public float tiltSpeed;
    public float jumpStrength;
    public float initialJump;
    public Vector3 forcePosition;
    public float turnAngle;
    bool drop = false;
    float jumpTimer = 0f;
    public float upTime = 0f;
    public float dropStrength;
    public bool isGrounded = false;
    public Transform pointer;

    Vector3 rightTurnRotation = new Vector3(0, 90, -30);
    Vector3 leftTurnRotation = new Vector3(0, -90, 30);
	// Update is called once per frame
	void Update () {

        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        float yValue = -9.8f;
        turnAngle = horizontal * turnSpeed * Time.deltaTime;
        Vector3 currentRotation = transform.rotation.eulerAngles;
        currentRotation.y += turnAngle;
        //currentRotation.y = Mathf.Clamp(currentRotation.y, -90, 90);
        transform.rotation = Quaternion.Euler(currentRotation);

        Vector3 turnMovement = transform.right * turnSpeed * horizontal;
        Vector3 snowboardRotation = transform.localRotation.eulerAngles;

        snowboardRotation.z = horizontal * -tiltDegree;

        Vector3 targetRotation = new Vector3(10, 0, 0);

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            jumpTimer = upTime - 0.1f;
            yValue += initialJump;
        }
        if(jumpTimer > 0 && !drop)
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
            yValue += dropStrength;
        }

        //Debug.Log(Time.deltaTime);
        Quaternion newRotation = Quaternion.Euler(snowboardRotation);
        transform.rotation = Quaternion.Lerp(transform.localRotation, newRotation, tiltSpeed);

        Vector3 newVelocity = transform.forward * forwardSpeed * 100 * Time.deltaTime;
        newVelocity.y = yValue;
        Debug.Log(newVelocity);
        GetComponent<Rigidbody>().velocity = newVelocity;

    }

    void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.tag == "Ground")
        {
            isGrounded = true;
            jumpTimer = 0;
            drop = false;
        }
    }

    void OnCollisionExit(Collision collision)
    {
        if(collision.collider.tag == "Ground")
        {
            isGrounded = false;
        }
    }
}
