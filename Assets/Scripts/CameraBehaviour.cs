using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBehaviour : MonoBehaviour
{
    private Vector3 offset;
    private GameObject player;
    private Vector3 velocity;

    void Start()
    {
        if (!player)
        {
            player = GameObject.FindGameObjectWithTag("Player");
        }

        velocity = Vector3.zero;
        offset = transform.position - player.transform.position;
    }

    void Update()
    {
        // Handled in Update as no physics to break.
        MoveCamera();
    }

    /// <summary>
    /// A method to move the camera to follow the player.
    /// TODO: Add camera rotation on the y axis to allow the player to look around.
    /// </summary>
    private void MoveCamera()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position + offset);

        if (distanceToPlayer > 1)
        {
            transform.position = Vector3.SmoothDamp(transform.position, player.transform.position + offset, ref velocity, 0.4f);
        }
    }
}
