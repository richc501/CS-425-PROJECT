using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]

public class motor : MonoBehaviour {
    private CharacterController controller;
    public Camera camera;
    public float turnSpeed;
    public float mouseSense;
    float cameraRotX;
    private Vector3 moveDirection;
    private Vector3 lastDirection;
    public float BaseSpeed = 4.0f;
    public float JumpSpeed = 8.0f;
    public float Gravity = 20.0f;

    public float RunSpeedIncrease = 10.0f;

    public float RampUpTime = 0.75f;
    private bool moveKeyDown = false;
    private float moveDownTime = 0f;
    private float friction = 15.0f;

    private MotorStates motorState = MotorStates.Default;

    private bool canWallRun = true;

    private float wallRunMaxTime = 15f;
    private float wallRunTime = 0.0f;
    private RaycastHit wallHit;

    float climbTime = 0.0f;
    bool canClimb = true;
    bool canGrabLedge = true;

    bool rightWallRun = true;
    bool leftWallRun = true;
    // Use this for initialization
    void Start () {
        camera = Camera.main;
        controller = GetComponent<CharacterController>();
        turnSpeed = 90f;
        mouseSense = 2.5f;
        cameraRotX = 0f;
    }
	
	// Update is called once per frame
	void Update () {

        switch(motorState)
        {
            case (MotorStates.Climbing):
                UpdateWallClimb();
                break;
            case (MotorStates.Jumping):
                UpdateJump();
                break;
            case (MotorStates.Ledgegrabbing):
                UpdateLedgeGrab();
                break;
            case (MotorStates.MusclingUp):
                MuscleUp();
                break;
            case (MotorStates.Wallrunning):
                UpdateWallRun();
                break;


            default:
                UpdateDefault();
                break;
        }



        controller.Move(moveDirection * Time.deltaTime);
        lastDirection = moveDirection;
    }

    void UpdateJump()
    {
        StandardCameraUpdate();

        wallHit = DoWallRunCheck();
        if(wallHit.collider != null)
        {
            motorState = MotorStates.Wallrunning;
            return;
        }

        RaycastHit hit = DoWallClimbCheck(new Ray(transform.position,
                                          transform.TransformDirection(Vector3.forward).normalized * 0.1f));
        if (hit.collider != null)
        {
            motorState = MotorStates.Climbing;
            return;
        }

        if (Input.GetButton("Jump"))
        {
            LedgeGrab();
        }

        moveDirection.y -= Gravity * Time.deltaTime;

        if(controller.isGrounded)
        {
            motorState = MotorStates.Default;
        }
    }

    void UpdateWallRun()
    {
        if (!controller.isGrounded && canWallRun && wallRunTime < wallRunMaxTime)
        {
            // Wall run stuff...
            wallHit = DoWallRunCheck();
            if (wallHit.collider == null){
             StopWallRun();
             return;
            }

            motorState = MotorStates.Wallrunning;

            //if(rightWallRun)
            //{
            //    camera.transform.Rotate(Vector3.forward, 10.0f * Time.deltaTime);
            //}
            //else if(leftWallRun)
            //{
            //    camera.transform.Rotate(Vector3.forward, -10.0f * Time.deltaTime);
            //}


            float previousJumpHeight = moveDirection.y;

            Vector3 crossProduct = Vector3.Cross(Vector3.up, wallHit.normal);

            Quaternion lookDirection = Quaternion.LookRotation(crossProduct);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookDirection, 3.5f * Time.deltaTime);

            moveDirection = crossProduct;
            moveDirection.Normalize();
            moveDirection *= BaseSpeed + (RunSpeedIncrease * (moveDownTime / RampUpTime));

            if (wallRunTime == 0.0f)
            {
                moveDirection.y = JumpSpeed / 4;
            }
            else
            {
                moveDirection.y = previousJumpHeight;
                moveDirection.y -= (Gravity / 4) * Time.deltaTime;
            }

            if(Input.GetButton("Jump"))
            {
                motorState = MotorStates.Jumping;
                moveDirection.y = JumpSpeed;
            }

            wallRunTime += Time.deltaTime;

            if (wallRunTime > wallRunMaxTime)
            {
                canWallRun = false;
            }
        }
        else
        {
            StopWallRun();
        }
    }

    void StopWallRun()
    {
        if (motorState == MotorStates.Wallrunning)
            canWallRun = false;

        wallRunTime = 0.0f;
        motorState = MotorStates.Default;
    }

    void UpdateWallClimb()
    {
        if (!moveKeyDown)
        {
            climbTime = 0.0f;
            if (motorState == MotorStates.Climbing)
                canClimb = false;
            motorState = MotorStates.Default;
            return;
        }

        Ray forwardRay = new Ray(transform.position, transform.TransformDirection(Vector3.forward).normalized);
        forwardRay.direction *= 0.1f;

        RaycastHit hit = DoWallClimbCheck(forwardRay);
        if (canClimb && hit.collider != null &&
            climbTime < 0.5f && Vector3.Angle(forwardRay.direction, hit.normal) > 165)
        {

            climbTime += Time.deltaTime;

            // Look up. Disabled for now.
            Quaternion lookDirection = Quaternion.LookRotation(hit.normal * -1);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookDirection, 3.5f * Time.deltaTime);
            //camera.transform.Rotate(-85f * (climbTime / 0.5f), 0f, 0f); //            ^ Magic number for tweaking look time

            // Move up.
            moveDirection += transform.TransformDirection(Vector3.up);
            moveDirection.Normalize();
            moveDirection *= BaseSpeed;

            motorState = MotorStates.Climbing;

            if (Input.GetButton("Jump"))
            {
                LedgeGrab();
            }

        }
        else
        {
            if (motorState == MotorStates.Climbing)
                canClimb = false;
            climbTime = 0f;
            motorState = MotorStates.Default;
        }
    }

    void LedgeGrab()
    {
        if (canGrabLedge &&
            (motorState == MotorStates.Jumping || motorState == MotorStates.Climbing) &&
            Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward).normalized, 1f))
        {
            motorState = MotorStates.Ledgegrabbing;
        }
    }

    void UpdateLedgeGrab()
    {
        // Need to make a non-standard update to limit how people can look around while hanging.
        StandardCameraUpdate();

        if (moveDirection.y != 0)
        {
            moveDirection.y -= friction * Time.deltaTime;
            moveDirection.y = Mathf.Clamp(moveDirection.y, 0, 100);
        }

        if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
        {
            canGrabLedge = false;
            motorState = MotorStates.Default;
            climbTime = 0f;
        }

        if (Input.GetButton("Jump"))
        {
            // Muscle up
            motorState = MotorStates.MusclingUp;
            climbTime = 0f;
        }
    }

    void MuscleUp()
    {

        Ray ray = new Ray(transform.position, transform.TransformDirection(Vector3.forward));
        ray.direction.Normalize();
        ray.origin = ray.origin - new Vector3(0f, 1f, 0f);

        if (Physics.Raycast(ray.origin, ray.direction, 1f))
        {
            moveDirection = transform.TransformDirection(Vector3.up + Vector3.forward);
            moveDirection.Normalize();
            moveDirection *= BaseSpeed;
        }
        else
        {
            motorState = MotorStates.Default;
        }
    }

    RaycastHit DoWallClimbCheck(Ray forwardRay)
    {
        RaycastHit hit;

        Physics.Raycast(forwardRay.origin, forwardRay.direction, out hit, 1f);

        return hit;
    }

    RaycastHit DoWallRunCheck()
    {
        Ray rayRight = new Ray(transform.position, transform.TransformDirection(Vector3.right));
        Ray rayLeft = new Ray(transform.position, transform.TransformDirection(Vector3.left));

        RaycastHit wallImpactRight;
        RaycastHit wallImpactLeft;

        bool rightImpact = Physics.Raycast(rayRight.origin, rayRight.direction, out wallImpactRight, 1f);
        bool leftImpact = Physics.Raycast(rayLeft.origin, rayLeft.direction, out wallImpactLeft, 1f);

        bool rightWallRun = rightImpact;
        bool leftWallRun = leftImpact;

        if (rightImpact && Vector3.Angle(transform.TransformDirection(Vector3.forward), wallImpactRight.normal) > 90)
        {
            return wallImpactRight;
        }
        else if (leftImpact && Vector3.Angle(transform.TransformDirection(Vector3.forward), wallImpactLeft.normal) > 90)
        {
            wallImpactLeft.normal *= -1;
            return wallImpactLeft;
        }
        else
        {
            return new RaycastHit();
        }
    }

    void StandardCameraUpdate()
    {
        transform.Rotate(0f, (Input.GetAxis("Mouse X") * mouseSense) * turnSpeed * Time.deltaTime, 0f);
        camera.transform.forward = transform.forward;

        cameraRotX -= Input.GetAxis("Mouse Y") * mouseSense;
        camera.transform.Rotate(cameraRotX, 0f, 0f);
    }

    void UpdateDefault()
    {
        StandardCameraUpdate();

        moveKeyDown = Input.GetKey(KeyCode.W);
        if (moveKeyDown && moveDownTime < RampUpTime)
        {
            moveDownTime += Time.deltaTime;
            if (moveDownTime > RampUpTime)
            {
                moveDownTime = RampUpTime;
            }
        }

        if (controller.isGrounded)
        {
            canClimb = true;
            canWallRun = true;
            canGrabLedge = true;

            // Stop  momentum only if grounded. Can't slow down while airborne.
            if (!moveKeyDown)
            {
                moveDownTime = 0f;
            }

            moveDirection = new Vector3(Input.GetAxisRaw("Horizontal"), 0f, Input.GetAxisRaw("Vertical"));
            moveDirection = transform.TransformDirection(moveDirection);
            moveDirection.Normalize();

            moveDirection *= BaseSpeed + (RunSpeedIncrease * (moveDownTime / RampUpTime));

            // slow to a stop if no input
            if ((moveDirection == Vector3.zero && lastDirection != Vector3.zero))
            {
                if (lastDirection.x != 0)
                {
                    moveDirection.x = DoSlowDown(lastDirection.x);
                }

                if (lastDirection.z != 0)
                {
                    moveDirection.z = DoSlowDown(lastDirection.z);
                }
            }

            if (Input.GetButton("Jump"))
            {
                motorState = MotorStates.Jumping;
                moveDirection.y = JumpSpeed;
            }
        }
        moveDirection.y -= Gravity * Time.deltaTime;
    }

    float DoSlowDown(float lastVelocity)
    {
        if (lastVelocity > 0)
        {
            lastVelocity -= friction * Time.deltaTime;
            if (lastVelocity < 0)
                lastVelocity = 0;
        }
        else
        {
            lastVelocity += friction * Time.deltaTime;
            if (lastVelocity > 0)
                lastVelocity = 0;
        }
        return lastVelocity;
    }
}

public enum MotorStates
{
    Climbing,
    Default,
    Falling,
    Jumping,
    Ledgegrabbing,
    MusclingUp,
    Wallrunning
}
