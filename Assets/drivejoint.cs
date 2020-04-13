using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DriveJoint
{
    public AngleRange AngleRange = new AngleRange();
    public float Proportional = 1.5f;
    public float Integral = 0.0f;
    public float Differential = 1.1f;

    private GameObject gameObject = null;
    private bool isInit = false;
    private Quaternion rotationInit = Quaternion.identity;
    private float deltaSAngle = 0.0f;

    public void AttachGameObject(GameObject obj)
    {
        gameObject = obj;
    }

    public void Update()
    {
        if (gameObject == null)
            return;

        HingeJoint hinge = gameObject.GetComponent<HingeJoint>();

        Quaternion localRotation = Quaternion.Inverse(gameObject.transform.rotation) * hinge.connectedBody.transform.rotation;

        if (!isInit)
        {
            rotationInit = localRotation;
            isInit = true;
        }

        Quaternion rotation = Quaternion.Inverse(rotationInit) * localRotation;

        Vector3 axis = new Vector3(0, 1, 0);//cylinder axis
        float angle = Vector3.Dot(rotation.eulerAngles, axis);

        float deltaAngle = AngleRange.GetTarget() - angle;

        if (deltaAngle < -180)
            deltaAngle = 360 + deltaAngle;
        else if (deltaAngle > 180)
            deltaAngle = -360 + deltaAngle;

        if (Mathf.Sign(deltaAngle) > 0)
            deltaSAngle += deltaAngle;
        else
            deltaSAngle -= deltaAngle;

        float deltaVelocity = -hinge.velocity;

        if (Mathf.Sign(deltaVelocity) == Mathf.Sign(deltaAngle))
            deltaVelocity = -Mathf.Abs(deltaVelocity);
        else
            deltaVelocity = Mathf.Abs(deltaVelocity);

        float kP = Proportional;
        float kI = Integral;
        float kD = Differential;

        JointMotor motor = hinge.motor;
        motor.targetVelocity = -Mathf.Sign(deltaAngle) * 1000000000;
        motor.force = kP * Mathf.Abs(deltaAngle) + kI * deltaSAngle + kD * deltaVelocity;
        motor.freeSpin = true;
        hinge.motor = motor;
        hinge.useMotor = true;
    }
}
