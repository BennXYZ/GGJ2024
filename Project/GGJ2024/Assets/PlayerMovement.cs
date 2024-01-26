using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float cameraMovementSpeed;
    public float playerMovementSpeed;
    public float playerSpeedUpSpeed;
    public float maxLowerCameraAngle;
    public float maxHigherCameraAngle;
    public Rigidbody rigidbody;
    public Transform cameraHolder;

    private void Awake()
    {
        if(!rigidbody)
            rigidbody = GetComponent<Rigidbody>();
        cameraHolder = transform.Find("CameraHolder");
    }

    // Start is called before the first frame update
    void Start()
    {
        //Debugging for me
    }

    // Update is called once per frame
    void Update()
    {
        rigidbody.transform.Rotate(Vector3.up, Input.GetAxis("Mouse X") * cameraMovementSpeed);
        float setoffEulerAngle = cameraHolder.transform.localEulerAngles.x;
        if (setoffEulerAngle > 180)
            setoffEulerAngle -= 360;
        float targetRotation = setoffEulerAngle - Input.GetAxis("Mouse Y") * cameraMovementSpeed;
        targetRotation = Mathf.Clamp(targetRotation, maxLowerCameraAngle, maxHigherCameraAngle);
        cameraHolder.transform.Rotate(targetRotation - setoffEulerAngle, 0,0);
    }
}
