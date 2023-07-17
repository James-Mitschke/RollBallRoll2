using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatingPlatform : MonoBehaviour
{
    public bool rotateClockwise;
    public float rotationSpeed = 1f;
    private int direction = 1;

    void Start()
    {
        direction = rotateClockwise ? 1 : -1;
    }

    void FixedUpdate()
    {
        RotatePlatform();
    }

    /// <summary>
    /// Rotates the platform based on the direction and speed assigned in the editor.
    /// </summary>
    private void RotatePlatform()
    {
        this.transform.Rotate(new Vector3(0, direction * rotationSpeed * Time.deltaTime, 0));
    }
}
