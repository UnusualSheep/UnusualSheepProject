using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleRotationScript : MonoBehaviour
{
    [SerializeField] Vector3 rotationVector;
    [SerializeField] float rotationSpeed;

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(rotationVector * rotationSpeed * Time.deltaTime);
    }
}
