using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatingPlatform : MonoBehaviour
{
    public bool rotateClockwise;
    public float rotationSpeed = 1f;
    private int direction = 1;

    // Start is called before the first frame update
    void Start()
    {
        direction = rotateClockwise ? 1 : -1;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        this.transform.Rotate(new Vector3(0, direction * rotationSpeed * Time.deltaTime, 0));
    }
}
