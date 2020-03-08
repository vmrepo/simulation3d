using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DriveJoint
{
    private bool isInit = false;
    private Quaternion rotationInit = Quaternion.identity;
    private float deltaSAngle = 0.0f;

    public void Update(GameObject gameObject, float targetangle)
    {
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

        float deltaAngle = targetangle - angle;

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

        float kP = 1.5f;
        float kI = 0.0f;
        float kD = 1.1f;

        JointMotor motor = hinge.motor;
        motor.targetVelocity = -Mathf.Sign(deltaAngle) * 1000;
        motor.force = kP * Mathf.Abs(deltaAngle) + kI * deltaSAngle + kD * deltaVelocity;
        motor.freeSpin = false;
        hinge.motor = motor;
        hinge.useMotor = true;

        MonoBehaviour.print(rotation.eulerAngles + "; " + hinge.axis + "; " + angle);
    }
}
