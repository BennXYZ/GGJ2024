using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : LevelObject
{
    public float cameraMovementSpeed;
    public float movementSpeed;
    public float acceleration;
    public float jumpForce;
    public float maxLowerCameraAngle;
    public float maxHigherCameraAngle;
    public Rigidbody rigidbody;
    public Transform cameraHolder;

    Vector2 currentNormalizedSpeed;

    private void Awake()
    {
        if(!rigidbody)
            rigidbody = GetComponent<Rigidbody>();
        cameraHolder = transform.Find("CameraHolder");
    }

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        //Debugging for me
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Jump"))
            rigidbody.AddForce(Vector3.up * 100 * jumpForce);

        if(acceleration != 0)
        {
            if (currentNormalizedSpeed.x > Input.GetAxis("Horizontal"))
                currentNormalizedSpeed.x = Mathf.Max(currentNormalizedSpeed.x - acceleration * Time.deltaTime, Input.GetAxis("Horizontal"));
            else if (currentNormalizedSpeed.x < Input.GetAxis("Horizontal"))
                currentNormalizedSpeed.x = Mathf.Max(currentNormalizedSpeed.x + acceleration * Time.deltaTime, Input.GetAxis("Horizontal"));


            if (currentNormalizedSpeed.y > Input.GetAxis("Vertical"))
                currentNormalizedSpeed.y = Mathf.Max(currentNormalizedSpeed.y - acceleration * Time.deltaTime, Input.GetAxis("Vertical"));
            else if (currentNormalizedSpeed.y < Input.GetAxis("Vertical"))
                currentNormalizedSpeed.y = Mathf.Max(currentNormalizedSpeed.y + acceleration * Time.deltaTime, Input.GetAxis("Vertical"));
        }

        rigidbody.transform.Rotate(Vector3.up, Input.GetAxis("Mouse X") * cameraMovementSpeed);
        float setoffEulerAngle = cameraHolder.transform.localEulerAngles.x;
        if (setoffEulerAngle > 180)
            setoffEulerAngle -= 360;
        float targetRotation = setoffEulerAngle - Input.GetAxis("Mouse Y") * cameraMovementSpeed;
        targetRotation = Mathf.Clamp(targetRotation, maxLowerCameraAngle, maxHigherCameraAngle);
        cameraHolder.transform.Rotate(targetRotation - setoffEulerAngle, 0,0);
    }

    private void FixedUpdate()
    {
        rigidbody.velocity = Vector3.up * rigidbody.velocity.y + rigidbody.transform.forward * currentNormalizedSpeed.y * movementSpeed + rigidbody.transform.right * currentNormalizedSpeed.x * movementSpeed;
    }
}
