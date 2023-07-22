using Assets.Scripts.Platforms;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatingPlatform : MonoBehaviour
{
    public bool rotateClockwise;
    public float rotationSpeed = 1f;
    private GameObject transformTracker;
    public PlatformModel platformModel;
    private int direction = 1;

    void Start()
    {
        platformModel = new PlatformModel(this.transform.position);
        direction = rotateClockwise ? 1 : -1;

        if (transformTracker == null)
        {
            transformTracker = this.GetComponentInChildren<Transform>().gameObject;
        }
    }

    void FixedUpdate()
    {
        platformModel.CurrentPosition = transformTracker.transform.position;

        RotatePlatform();

        platformModel.OldPosition = transformTracker.transform.position;
    }

    /// <summary>
    /// Rotates the platform based on the direction and speed assigned in the editor.
    /// </summary>
    private void RotatePlatform()
    {
        this.transform.Rotate(new Vector3(0, direction * rotationSpeed * Time.deltaTime, 0));
    }

    public PlatformModel GetPlatformPositions()
    {
        return this.platformModel;
    }
}
