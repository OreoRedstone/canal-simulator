using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControl : MonoBehaviour
{
    public float boatAcceleration;
    public float boatMaxSpeed;
    public float turboModifier;
    public bool turbo;

    private float throttle;
    private float rudderRotation;
    private GameObject motor;
    private Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        motor = GameObject.Find("Motor");
        rb = transform.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        Gamepad gamepad = Gamepad.current;
        Keyboard keyboard = Keyboard.current;

        if(keyboard != null && gamepad != null)
        {
            throttle = Mathf.Clamp(gamepad.rightTrigger.EvaluateMagnitude() - gamepad.leftTrigger.EvaluateMagnitude() + Convert.ToInt32(keyboard.wKey.isPressed) - Convert.ToInt32(keyboard.sKey.isPressed), -1, 1);
            rudderRotation = Mathf.Clamp(gamepad.leftStick.x.ReadValue() + Convert.ToInt32(keyboard.dKey.isPressed) - Convert.ToInt32(keyboard.aKey.isPressed), -1, 1);
        }
        else if(gamepad != null)
        {
            throttle = gamepad.rightTrigger.EvaluateMagnitude() - gamepad.leftTrigger.EvaluateMagnitude();
            rudderRotation = gamepad.leftStick.x.ReadValue();
        }
        else if(keyboard != null)
        {
            throttle = Convert.ToInt32(keyboard.wKey.isPressed) - Convert.ToInt32(keyboard.sKey.isPressed);
            rudderRotation = Convert.ToInt32(keyboard.dKey.isPressed) - Convert.ToInt32(keyboard.aKey.isPressed);
        }

        motor.transform.localEulerAngles = new Vector3(0, rudderRotation * 30, 0);
    }

    private void FixedUpdate()
    {
        if(turbo)
        {
            rb.AddForceAtPosition(motor.transform.forward * throttle * turboModifier * boatAcceleration, motor.transform.position);
        }
        else
        {
            rb.AddForceAtPosition(motor.transform.forward * throttle * boatAcceleration, motor.transform.position);
        }
    }
}
