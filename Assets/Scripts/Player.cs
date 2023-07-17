using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Not sure why these are constants, maybe I was following a guide? Even though I'm happy with the values it would be good to have these be editable in the editor.
    const float movementSpeed = 250f;
    const float jumpSpeed = 10000f;
    private Rigidbody playerRB;
    private bool isGrounded = true;
    private int floorLayer;

    void Start()
    {
        playerRB = gameObject.GetComponent<Rigidbody>();
        floorLayer = LayerMask.NameToLayer("Floor");
    }

    void FixedUpdate()
    {
        // Why FixedUpdate for this? Because Time.deltaTime gives consistent and smoove movement in FixedUpdate.
        MovePlayer();
    }

    /// <summary>
    /// A method to handle the movement of the player.
    /// This will be reworked at a later point to use player assigned keys instead of WASD Space.
    /// TODO: Make movement direction be relative to camera direction.
    /// TODO: Rework to use player assignable keys instead of hardcoded WASD Space.
    /// </summary>
    private void MovePlayer()
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

        // If the player isn't intentionally moving then slow down their movement. This won't actually stop the player which isn't ideal.
        // TODO: Improve and make this stop the player when velocity gets below a small arbitrary boundary.
        if (!Input.anyKey)
        {
            playerRB.AddForce(-(playerRB.velocity.x / 4), 0, -(playerRB.velocity.z / 4));
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        // If the player is touching an object on the floor layer, enable jumping
        if (!isGrounded && collision.gameObject.layer == floorLayer)
        {
            isGrounded = true;
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        // Placeholder for stay attached to platform functionality
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
        // If the player isn't touching an object on the floor layer, disable jumping
        if (isGrounded && collision.gameObject.layer == floorLayer)
        {
            isGrounded = false;
        }
    }
}
