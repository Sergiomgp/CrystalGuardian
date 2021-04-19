using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectRotation : MonoBehaviour
{
    public float xAngle, yAngle, zAngle;
    public float rotationSpeed;
    // Update is called once per frame
    void Update()
    {
        transform.Rotate(xAngle, yAngle * rotationSpeed * Time.deltaTime, zAngle,  Space.Self);
    }
}
