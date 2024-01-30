using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Boat : MonoBehaviour
{
    public float hpModifier;
    public float boatMaxSpeed;
    public float turboModifier;
    public bool turbo;
    public float fuelUsageModifier = 1f;

    public float rpm;
    private RectTransform tachoIndicator;

    public float fuelLevel;
    public float maxFuelLevel;
    private RectTransform fuelIndicator;
    public float fuelUsageRate = 0.22f;

    private float throttle;
    private float rudderRotation;
    private GameObject motor;
    private Rigidbody rb;
    private GameObject bow;

    public float waterLevel;
    public float waterDrag;
    public float waterAngularDrag;

    // Start is called before the first frame update
    void Start()
    {
        bow = GameObject.Find("Bow");
        motor = GameObject.Find("Motor");
        rb = transform.GetComponent<Rigidbody>();
        tachoIndicator = GameObject.Find("TachoIndicatorParent").GetComponent<RectTransform>();
        if (fuelUsageModifier != 0f)
        {
            fuelIndicator = GameObject.Find("FuelIndicatorParent").GetComponent<RectTransform>();
        }

        Floater[] floaters = GetComponentsInChildren<Floater>();
        foreach (Floater floater in floaters)
        {
            floater.UpdateValues(waterLevel, floaters.Length, waterDrag, waterAngularDrag);
        }
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

        motor.transform.localEulerAngles = new Vector3(0, rudderRotation * 50, 0);

        //(Mathf.Cos(Vector3.Angle(motor.transform.forward, rb.velocity)) * Mathf.Sqrt(Mathf.Pow(rb.velocity.x, 2) + Mathf.Pow(rb.velocity.z, 2)))

        rpm += Time.deltaTime * Mathf.Abs(throttle) * 3000000 * hpModifier * Mathf.Sqrt(2000-rpm);
        if(rpm > 800)
        {
            rpm -= Time.deltaTime * 200;
        }
        else
        {
            rpm = 800 + UnityEngine.Random.Range(-10, 10);
        }
        if(rpm > 2000)
        {
            rpm = 2000 + UnityEngine.Random.Range(-50, 50);
        }

        fuelLevel -= Time.deltaTime * Mathf.Pow(rpm, 3) * fuelUsageRate * 0.00000000001f * fuelUsageModifier;
        if(fuelLevel > maxFuelLevel)
        {
            fuelLevel = maxFuelLevel;
        }
        if(fuelLevel < 0)
        {
            fuelLevel = 0;
        }

        if(fuelUsageModifier != 0f)
        {
            fuelIndicator.localEulerAngles = new Vector3(0, 0, (fuelLevel / maxFuelLevel) * -180);
        }
        tachoIndicator.localEulerAngles = new Vector3(0, 0, (rpm/2000) * -180);
    }

    private void FixedUpdate()
    {
        if (rb.velocity.magnitude >= boatMaxSpeed * (turbo ? turboModifier : 1)) return;
        if (throttle == 0) return;

        rb.AddForceAtPosition(hpModifier * Mathf.Pow(rpm, 3) * motor.transform.forward, motor.transform.position * throttle / Math.Abs(throttle) * (turbo ? turboModifier : 1));
        rb.AddForceAtPosition(0.5f * hpModifier * Mathf.Pow(rpm, 3) * -motor.transform.forward, bow.transform.position * throttle / Math.Abs(throttle) * (turbo ? turboModifier : 1));
    }
}
