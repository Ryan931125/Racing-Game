using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody _rb;
    private WirelessMotionController _motionController;

    // Shake detection
    // public float shakeThreshold = 500f;
    // public float shakeCooldown = 0.1f;
    // private float lastShakeTime = 0f;
    // private Vector3 lastEulerAngles = Vector3.zero;

    // Drifting
    public float driftThreshold;
    public Transform rearPoint;
    public float driftForce = 10f;
    [SerializeField]
    private bool isDrifting;
    // Boost
    public float boostSpeed;
    public float boostAcceleration;
    public float startBoostTime;
    public float boostDuration;
    private bool flag;

    public float turnThreshold;
    public float moveThreshold;
    [SerializeField]
    private float accelerationMultiplier;
    [SerializeField]
    private int moveDirection;

    private float _currentSpeed = 0f;
    public float maxSpeed;
    public float minSpeed = -5;
    [SerializeField]
    private float acceleration;

    private Vector3 _turnAngle;
    [SerializeField]
    private float maxRotationAngle;
    [SerializeField]
    private float rotationSpeed;
    [SerializeField]
    private Transform hands;
    [SerializeField]
    private Transform frontLeftWheel;
    [SerializeField]
    private Transform frontRightWheel;

    // Start is called before the first frame update
    void Awake(){
        _rb = GetComponent<Rigidbody>();
        _motionController = GetComponent<WirelessMotionController>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // DetectShake();
        UpdateGear();
        Steer();
        Drift();
        Move();
    }

    // private void DetectShake(){
    //     // Debug.Log($"{_motionController.pitch} {_motionController.yaw} {_motionController.roll}");
    //     // Vector3 currentEulerAngles = new Vector3(_motionController.pitch, _motionController.yaw, _motionController.roll);
    //     Vector3 currentEulerAngles = new Vector3(0, 0, _motionController.roll);
    //     Vector3 deltaAngles = currentEulerAngles - lastEulerAngles;

    //     // Wrap angle differences to [-180, 180]
    //     deltaAngles = new Vector3(
    //         Mathf.DeltaAngle(lastEulerAngles.x, currentEulerAngles.x),
    //         Mathf.DeltaAngle(lastEulerAngles.y, currentEulerAngles.y),
    //         Mathf.DeltaAngle(lastEulerAngles.z, currentEulerAngles.z)
    //     );

    //     float angularSpeed = deltaAngles.magnitude / Time.deltaTime;

    //     // Debug.Log($"Current angle: {currentEulerAngles}, Last angle: {lastEulerAngles}, Angular speed: {angularSpeed}");

    //     if (angularSpeed > shakeThreshold && Time.time - lastShakeTime > shakeCooldown)
    //     {
    //         lastShakeTime = Time.time;
    //         Debug.Log("Shake detected!");
    //         // Trigger your shake response here
    //     }

    //     lastEulerAngles = currentEulerAngles;
    // }

    private void UpdateGear(){
        if(_motionController.potentiometer < 0.2f){
            accelerationMultiplier = -(_motionController.potentiometer - 0.2f) * 2f;
            moveDirection = -1;
        }
        else{
            accelerationMultiplier = Mathf.Round((_motionController.potentiometer - 0.2f) * 2f + 0.5f);
            moveDirection = 1;
        }
    }

    private void Steer(){
        // if(Input.GetKey(KeyCode.A)){
        //     RotateVisual(maxRotationAngle, rotationSpeed * 1f);
        // }
        // else if(Input.GetKey(KeyCode.D)){
        //     RotateVisual(-maxRotationAngle, rotationSpeed * 1f);
        // }
        // else{
        //     RotateVisual(0, rotationSpeed * 1f);
        // }
        if(_motionController.yaw < -turnThreshold || _motionController.yaw > turnThreshold){
            RotateVisual(maxRotationAngle * Mathf.Sign(_motionController.yaw), rotationSpeed * 1f * Mathf.Sign(_motionController.yaw) * _motionController.yaw / 180f);
        }
        else{
            RotateVisual(0, rotationSpeed * 1f);
        }
    }

    private void RotateRigidBody(){
        _turnAngle = frontLeftWheel.eulerAngles - transform.eulerAngles;
        _turnAngle.y = RegularizeAngle(_turnAngle.y);
        Quaternion deltaRotation = Quaternion.Euler(Mathf.Sign(_currentSpeed) * Time.fixedDeltaTime * _turnAngle);
        _rb.MoveRotation(_rb.rotation * deltaRotation);
    }

    private float RegularizeAngle(float angle){
        angle = (angle > 180) ? angle - 360 : angle;
        angle = (angle < -180) ? angle + 360 : angle;
        return angle;
    }

    private void RotateVisual(float targetAngle, float rotationSpeed){
        float handAngle = RegularizeAngle(hands.localRotation.eulerAngles.z);
        float wheelAngle = RegularizeAngle(frontLeftWheel.localRotation.eulerAngles.y);
        hands.Rotate(0, 0, (targetAngle - handAngle) * Time.fixedDeltaTime * rotationSpeed, Space.Self);
        frontLeftWheel.Rotate(0, (-targetAngle - wheelAngle) * Time.fixedDeltaTime * rotationSpeed, 0, Space.Self);
        frontRightWheel.Rotate(0, (-targetAngle - wheelAngle) * Time.fixedDeltaTime * rotationSpeed, 0, Space.Self);
    }

    void Move(){
        // _currentSpeed = Mathf.Lerp(_currentSpeed, maxSpeed, Time.deltaTime * acceleration);
        // if(Input.GetKey(KeyCode.W)){
        //     _currentSpeed = Mathf.Lerp(_currentSpeed, maxSpeed, Time.deltaTime * acceleration * 1f);
        //     Debug.Log("Move Forward");
        // }
        // else if(Input.GetKey(KeyCode.S)){
        //     _currentSpeed = Mathf.Lerp(_currentSpeed, minSpeed, Time.deltaTime * acceleration * 2f);
        //     Debug.Log("Move Backward");
        // }
        // else{
        //     _currentSpeed = Mathf.Lerp(_currentSpeed, 0, Time.deltaTime * acceleration * 0.5f);
        // }

        // if(_motionController.isTriggered){
        //     float targetSpeed = moveDirection == 1 ? maxSpeed : minSpeed;
        //     _currentSpeed = Mathf.Lerp(_currentSpeed, targetSpeed, Time.deltaTime * acceleration * accelerationMultiplier);
        //     Debug.Log($"Move targetSpeed: {targetSpeed}, accelerationMulti: {accelerationMultiplier}");
        // }
        // else{
        //     _currentSpeed = Mathf.Lerp(_currentSpeed, 0, Time.deltaTime * acceleration * 0.5f);
        // }

        
        
        // Bonus 1
        if(_motionController.pitch > moveThreshold){
            if(Time.time - startBoostTime < boostDuration){
                _currentSpeed = Mathf.Lerp(_currentSpeed, boostSpeed, Time.deltaTime * boostAcceleration * 1f * Mathf.Abs(_motionController.pitch / 180f));
            }
            else{
                float tempMulti = _currentSpeed > maxSpeed ? 10 : 1; 
                _currentSpeed = Mathf.Lerp(_currentSpeed, maxSpeed, Time.deltaTime * acceleration * tempMulti * Mathf.Abs(_motionController.pitch / 180f));
            }
            // Debug.Log("Move Forward");
        }
        else if(_motionController.pitch < -moveThreshold){
            _currentSpeed = Mathf.Lerp(_currentSpeed, minSpeed, Time.deltaTime * acceleration * 2f * Mathf.Abs(_motionController.pitch / 180f));
            // Debug.Log("Move Backward");
        }
        else{
            _currentSpeed = Mathf.Lerp(_currentSpeed, 0, Time.deltaTime * acceleration * 0.5f);
        }

        Debug.Log($"{_currentSpeed}");

        RotateRigidBody();

        Vector3 velocity = _currentSpeed * transform.forward;
        velocity.y = _rb.velocity.y;
        _rb.velocity = velocity;
    }

    private void Drift(){
        if(_motionController.yaw < -driftThreshold || _motionController.yaw > driftThreshold){
            isDrifting = true;
            _rb.AddForceAtPosition(rearPoint.forward * driftForce * Mathf.Sign(_motionController.yaw), rearPoint.position, ForceMode.Force);
            flag = false;
        }
        else{
            isDrifting = false;
            if(flag == false){
                startBoostTime = Time.time;  
                flag = true;   
            }
        }
    }
    
}
