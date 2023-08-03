using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Floater : MonoBehaviour
{
    public Rigidbody bodyRigidbody;
    public float depthBeforeSubmerged = 1f;
    public float displacementAmount = 3f;

    public int floaterCount = 1;

    public float waterLevel;

    public float waterDrag = 0.99f;
    public float waterAngularDrag = 0.5f;

    private void FixedUpdate()
    {
        bodyRigidbody.AddForceAtPosition(Physics.gravity / floaterCount, transform.position, ForceMode.Acceleration);
        //float waveHeight = WaveManager.instance.GetWaveHeight(transform.position.x, transform.position.z);
        if (transform.position.y < waterLevel)
        {
            float displacementMultiplier = Mathf.Clamp01((waterLevel - transform.position.y) / depthBeforeSubmerged) * displacementAmount;
            bodyRigidbody.AddForceAtPosition(new Vector3(0f, Mathf.Abs(Physics.gravity.y) * displacementMultiplier, 0f), transform.position, ForceMode.Acceleration);
            bodyRigidbody.AddForce(displacementMultiplier * -bodyRigidbody.velocity * waterDrag * Time.fixedDeltaTime, ForceMode.VelocityChange);
            bodyRigidbody.AddTorque(displacementMultiplier * -bodyRigidbody.angularVelocity * waterAngularDrag * Time.fixedDeltaTime, ForceMode.VelocityChange);
        }
    }

    public void UpdateValues(float newWaterLevel, int newFloaterCount, float newWaterDrag, float newWaterAngularDrag)
    {
        waterLevel = newWaterLevel;
        floaterCount = newFloaterCount;
        waterDrag = newWaterDrag;
        waterAngularDrag = newWaterAngularDrag;
    }
}
