using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBehaviour : MonoBehaviour
{
    private Vector3 offset;
    private GameObject player;
    private Vector3 velocity;

    // Start is called before the first frame update
    void Start()
    {
        if (!player)
        {
            player = GameObject.FindGameObjectWithTag("Player");
        }

        velocity = Vector3.zero;
        offset = transform.position - player.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position + offset); 

        if (distanceToPlayer > 1)
        {
            transform.position = Vector3.SmoothDamp(transform.position, player.transform.position + offset, ref velocity, 0.4f);
        }
    }
}
