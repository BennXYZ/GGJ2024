using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : PlayerComponent
{
    public float cameraMovementSpeed;
    public float movementSpeed;
    public float acceleration;
    public float jumpForce;
    public float fallForce;
    public float maxLowerCameraAngle;
    public float maxHigherCameraAngle;
    public new Rigidbody rigidbody;
    public Transform cameraHolder;
    [SerializeField]
    GameObject jumpAudioPrefab;

    public Transform rotateable;

    public Animator animator;

    public float jumpDetectionRadius;
    public float jumpDetectionRange;

    [Range(0, 10)]
    public int groundedCheckCount;

    Vector2 currentNormalizedSpeed;
    bool isGrounded;

    private void Awake()
    {
        if(!rigidbody)
            rigidbody = GetComponent<Rigidbody>();
        if(!cameraHolder)
            cameraHolder = transform.Find("CameraHolder");
    }

    // Start is called before the first frame update
    protected void Start()
    {
        //Debugging for me
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = false;
        isGrounded |= Physics.Raycast(transform.position, Vector3.down, jumpDetectionRange);
        if(!isGrounded)
        {
            for (int i = 0; i < groundedCheckCount; i++)
            {
                isGrounded |= Physics.Raycast(transform.position + Vector3.right * Mathf.Sin(2 * Mathf.PI * (i / 8f)) * jumpDetectionRadius + Vector3.forward * Mathf.Cos(2 * Mathf.PI * (i / 8f)) * jumpDetectionRadius, Vector3.down, jumpDetectionRange);
                if (isGrounded)
                    break;
            }
        }
        if (isGrounded && Input.GetButtonDown("Jump"))
        {
            Instantiate(jumpAudioPrefab, transform.position, transform.rotation);
            rigidbody.AddForce(Vector3.up * 100 * jumpForce);
        }
        rigidbody.AddForce(Vector3.down * fallForce);
        animator.SetBool("IsGrounded", isGrounded);

        if (acceleration != 0)
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

        rotateable.Rotate(0, Input.GetAxis("Mouse X") * cameraMovementSpeed * Time.deltaTime, 0, Space.World);
        float setoffEulerAngle = cameraHolder.transform.localEulerAngles.x;
        if (setoffEulerAngle > 180)
            setoffEulerAngle -= 360;
        float targetRotation = setoffEulerAngle - Input.GetAxis("Mouse Y") * cameraMovementSpeed * Time.deltaTime;
        targetRotation = Mathf.Clamp(targetRotation, maxLowerCameraAngle, maxHigherCameraAngle);
        cameraHolder.transform.Rotate(targetRotation - setoffEulerAngle, 0,0);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawLine(transform.position, transform.position + Vector3.down * jumpDetectionRange);
        for (int i = 0; i < groundedCheckCount; i++)
        {
            Gizmos.DrawLine(transform.position + Vector3.right * Mathf.Sin(2 * Mathf.PI * (i / (float)groundedCheckCount)) * jumpDetectionRadius + Vector3.forward * Mathf.Cos(2 * Mathf.PI * (i / (float)groundedCheckCount)) * jumpDetectionRadius,
                                transform.position + Vector3.right * Mathf.Sin(2 * Mathf.PI * (i / (float)groundedCheckCount)) * jumpDetectionRadius + Vector3.forward * Mathf.Cos(2 * Mathf.PI * (i / (float)groundedCheckCount)) * jumpDetectionRadius + Vector3.down * jumpDetectionRange);
        }
    }

    private void FixedUpdate()
    {
        rigidbody.velocity = Vector3.up * rigidbody.velocity.y + rotateable.forward * currentNormalizedSpeed.y * movementSpeed + rotateable.right * currentNormalizedSpeed.x * movementSpeed;
        animator.SetFloat("WalkSpeed", currentNormalizedSpeed.magnitude);
        animator.SetFloat("JumpSpeed", isGrounded ? 0 : rigidbody.velocity.y);
    }
}
