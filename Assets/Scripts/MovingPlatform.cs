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

    void Start()
    {
        startPos = this.transform.position;
    }

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

    /// <summary>
    /// Moves the platform to a given position at a speed specified in the editor and then makes the platform delay before moving again.
    /// </summary>
    /// <param name="position">A <see cref="Vector3"/> position in world to move to, specified in the editor.</param>
    private void MovePlatform(Vector3 position)
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

    /// <summary>
    /// Basically lerp but with functionality to adjust movement amount based on which axis need movement.
    /// </summary>
    /// <param name="currentPosition">The position of this gameobject currently.</param>
    /// <param name="goalPosition">The position we want the gameobject to move to.</param>
    /// <returns>A <see cref="Vector3"/> that contains the distance to travel along each axis.</returns>
    private Vector3 GetTranslateAmount(Vector3 currentPosition, Vector3 goalPosition)
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
