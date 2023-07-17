using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public Vector3 endPos;
    public float movementSpeed = 1f;
    public int stopTimeInSeconds = 1;
    private Vector3 startPos;
    private bool movingToEndPos = true;
    private int waitTimer = 0;

    // Start is called before the first frame update
    void Start()
    {
        startPos = this.transform.position;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (movingToEndPos)
        {
            MovePlatform(endPos);
        }
        else
        {
            MovePlatform(startPos);
        }
    }

    void MovePlatform(Vector3 position)
    {
        var distanceToPos = Vector3.Distance(position, this.transform.position);

        if (distanceToPos > 0.5f)
        {
            var translation = GetTranslateAmount(this.transform.position, position);
            this.transform.Translate(translation);
        }
        else
        {
            if (waitTimer < stopTimeInSeconds * 1000 * Time.deltaTime)
            {
                waitTimer += 1;
            }
            else
            {
                movingToEndPos = !movingToEndPos;
                waitTimer = 0;
            }
        }
    }

    Vector3 GetTranslateAmount(Vector3 currentPosition, Vector3 goalPosition)
    {
        var movementAmountPerAxis = 1f;
        var totalX = 0f;
        var totalY = 0f;
        var totalZ = 0f;

        var xAmount = goalPosition.x - currentPosition.x;
        var yAmount = goalPosition.y - currentPosition.y;
        var zAmount = goalPosition.z - currentPosition.z;

        var xAbsAmount = Mathf.Abs(xAmount);
        var yAbsAmount = Mathf.Abs(yAmount);
        var zAbsAmount = Mathf.Abs(zAmount);

        if (xAbsAmount > 0 && yAbsAmount > 0 && zAbsAmount > 0)
        {
            movementAmountPerAxis = 0.33f;
        }
        else if (xAbsAmount > 0 && yAbsAmount > 0 || xAbsAmount > 0 && zAbsAmount > 0 || yAbsAmount > 0 && zAbsAmount > 0)
        {
            movementAmountPerAxis = 0.66f;
        }

        if (xAbsAmount > 0)
        {
            totalX = xAmount / xAbsAmount;
        }
        if (yAbsAmount > 0)
        {
            totalY = yAmount / yAbsAmount;
        }
        if (zAbsAmount > 0)
        {
            totalZ = zAmount / zAbsAmount;
        }

        return new Vector3(totalX * movementAmountPerAxis * movementSpeed * Time.deltaTime,
                totalY * movementAmountPerAxis * movementSpeed * Time.deltaTime,
                totalZ * movementAmountPerAxis * movementSpeed * Time.deltaTime);
    }
}
