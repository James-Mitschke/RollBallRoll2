using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    const float movementSpeed = 250f;
    const float jumpSpeed = 10000f;
    private Rigidbody playerRB;
    private bool isGrounded = true;
    private int floorLayer;

    // Start is called before the first frame update
    void Start()
    {
        playerRB = gameObject.GetComponent<Rigidbody>();
        floorLayer = LayerMask.NameToLayer("Floor");
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.W) && playerRB.velocity.z < movementSpeed)
        {
            playerRB.AddForce(0, 0, 1 * movementSpeed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.A) && playerRB.velocity.x > -movementSpeed)
        {
            playerRB.AddForce(-1 * movementSpeed * Time.deltaTime, 0, 0);
        }
        if (Input.GetKey(KeyCode.D) && playerRB.velocity.x < movementSpeed)
        {
            playerRB.AddForce(1 * movementSpeed * Time.deltaTime, 0, 0);
        }
        if (Input.GetKey(KeyCode.S) && playerRB.velocity.x > -movementSpeed)
        {
            playerRB.AddForce(0, 0, -1 * movementSpeed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.Space) && isGrounded)
        {
            playerRB.AddForce(0, 1 * jumpSpeed * Time.deltaTime, 0);
        }
        if (!Input.anyKey)
        {
            playerRB.AddForce(-(playerRB.velocity.x / 4), 0, -(playerRB.velocity.z / 4));
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!isGrounded && collision.gameObject.layer == floorLayer)
        {
            isGrounded = true;
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Elevator":
                break;

            case "Mover":
                break;

            case "Rotator":
                break;

            default:
                break;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (isGrounded && collision.gameObject.layer == floorLayer)
        {
            isGrounded = false;
        }
    }
}
