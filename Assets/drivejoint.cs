using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DriveJoint
{
    public AngleRange AngleRange = new AngleRange();
    public float KinematicAngularVelocity = 100.0f;
    public float Proportional = 1.5f;
    public float Integral = 0.0f;
    public float Differential = 1.1f;

    private KinematicJoint kinematicJoint = null;
    private GameObject gameObject = null;
    private HingeJoint hingeJoint = null;
    private Quaternion rotationInit = Quaternion.identity;

    private float deltaSAngle = 0.0f;

    public GameObject GetGameObject()
    {
        return gameObject;
    }

    public void AttachGameObject(GameObject obj)
    {
        kinematicJoint = null;
        gameObject = obj;
        hingeJoint = gameObject.GetComponent<HingeJoint>();
        rotationInit = Quaternion.Inverse(gameObject.transform.rotation) * hingeJoint.connectedBody.transform.rotation;
    }

    public void AttachKinematic(KinematicJoint kinematic)
    {
        kinematicJoint = kinematic;
        gameObject = kinematic.GetGameObject();
        hingeJoint = gameObject.GetComponent<HingeJoint>();
        rotationInit = Quaternion.Inverse(gameObject.transform.rotation) * hingeJoint.connectedBody.transform.rotation;
    }

    public void Update()
    {
        if (gameObject == null)
            return;

        if (hingeJoint.connectedBody.isKinematic)
        {
            kinematicJoint.Rotate(Quaternion.AngleAxis(AngleRange.GetTarget(), Vector3.up));
            return;
        }

        Quaternion localRotation = Quaternion.Inverse(gameObject.transform.rotation) * hingeJoint.connectedBody.transform.rotation;
        Quaternion rotation = Quaternion.Inverse(rotationInit) * localRotation;

        float angle = Vector3.Dot(rotation.eulerAngles, Vector3.up);//Vector3.up - cylinder axis

        float deltaAngle = AngleRange.GetTarget() - angle;

        if (deltaAngle < -180)
            deltaAngle = 360 + deltaAngle;
        else if (deltaAngle > 180)
            deltaAngle = -360 + deltaAngle;

        if (Mathf.Sign(deltaAngle) > 0)
            deltaSAngle += deltaAngle;
        else
            deltaSAngle -= deltaAngle;

        float deltaVelocity = -hingeJoint.velocity;

        if (Mathf.Sign(deltaVelocity) == Mathf.Sign(deltaAngle))
            deltaVelocity = -Mathf.Abs(deltaVelocity);
        else
            deltaVelocity = Mathf.Abs(deltaVelocity);

        float kP = Proportional;
        float kI = Integral;
        float kD = Differential;

        JointMotor motor = hingeJoint.motor;
        motor.targetVelocity = -Mathf.Sign(deltaAngle) * 1000000000;
        motor.force = kP * Mathf.Abs(deltaAngle) + kI * deltaSAngle + kD * deltaVelocity;
        motor.freeSpin = true;
        hingeJoint.motor = motor;
        hingeJoint.useMotor = true;
    }
}
