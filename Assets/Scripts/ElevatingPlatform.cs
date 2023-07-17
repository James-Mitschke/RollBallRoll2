using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatingPlatform : MonoBehaviour
{
    public bool isAscending;
    public float maxDistance = 1f;
    public float movementSpeed = 1f;
    public int stopTimeInSeconds = 1;
    private bool currentlyAscending = true;
    private int timer = 0;
    private Vector3 startPos;

    void Start()
    {
        startPos = this.transform.position;
        currentlyAscending = isAscending;
    }

    void FixedUpdate()
    {
        // TODO: Move this out into its own method, there's a lot of duplicate code here so would be good to abstract out into something that is re-usable.
        if (isAscending)
        {
            if (currentlyAscending && this.transform.position.y < startPos.y + maxDistance)
            {
                this.transform.Translate(0, 1 * movementSpeed * Time.deltaTime, 0);
            }
            else if (!currentlyAscending && this.transform.position.y > startPos.y)
            {
                this.transform.Translate(0, -1 * movementSpeed * Time.deltaTime, 0);
            }
            else
            {
                if (timer < stopTimeInSeconds * 1000 * Time.deltaTime)
                {
                    timer += 1;
                }
                else
                {
                    currentlyAscending = !currentlyAscending;
                    timer = 0;
                }
            }
        }
        else
        {
            if (currentlyAscending && this.transform.position.y < startPos.y)
            {
                this.transform.Translate(0, 1 * movementSpeed * Time.deltaTime, 0);
            }
            else if (!currentlyAscending && this.transform.position.y > startPos.y - maxDistance)
            {
                this.transform.Translate(0, -1 * movementSpeed * Time.deltaTime, 0);
            }
            else
            {
                if (timer < stopTimeInSeconds * 1000 * Time.deltaTime)
                {
                    timer += 1;
                }
                else
                {
                    currentlyAscending = !currentlyAscending;
                    timer = 0;
                }
            }
        }
        
    }
}
