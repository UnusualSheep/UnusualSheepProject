using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class AimStateManager : MonoBehaviour
{
    public Cinemachine.AxisState xAxis, yAxis;
    [SerializeField] Transform cameraFollowPosition;
    [SerializeField] MovementStateMachine movementStateMachine;
    // Start is called before the first frame update
    void Start()
    {
        movementStateMachine = GetComponent<MovementStateMachine>();
    }

    // Update is called once per frame
    void Update()
    {
        if (movementStateMachine.canMove)
        {
            xAxis.Update(Time.deltaTime);
            yAxis.Update(Time.deltaTime);
        }
    }

    private void LateUpdate()
    {
        cameraFollowPosition.localEulerAngles = new Vector3(yAxis.Value, cameraFollowPosition.localEulerAngles.y, cameraFollowPosition.localEulerAngles.z);
        transform.eulerAngles = new Vector3(transform.eulerAngles.x, xAxis.Value, transform.eulerAngles.z);
    }
}
