using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DriveJoint
{
    public float Proportional = 1.5f;
    public float Integral = 0.0f;
    public float Differential = 1.1f;

    private GameObject gameObject = null;
    private bool isInit = false;
    private Quaternion rotationInit = Quaternion.identity;
    private float deltaSAngle = 0.0f;
    private float targetangle = 0;
    private float downlimit = 0;
    private float uplimit = 360;

    public void AttachGameObject(GameObject obj)
    {
        gameObject = obj;
    }

    private float checkrange(float angle)
    {
        //приводим к даиапазону [0, 360)

        angle = angle % 360;
        if (angle < 0)
            angle += 360;

        return angle;
    }

    private void checklimits()
    {
        //контролируем ограничение для targetangle

        if (downlimit < uplimit)
        {
            if (targetangle < downlimit || uplimit <= targetangle)
            {
                if (DistanceAngle(targetangle, downlimit) < DistanceAngle(targetangle, uplimit))
                    targetangle = downlimit < 360 ? downlimit : 0;
                else
                    targetangle = uplimit < 360 ? uplimit : 0;
            }
        }
        else
        {
            if (targetangle < downlimit && uplimit <= targetangle)
            {
                if (DistanceAngle(targetangle, downlimit) < DistanceAngle(targetangle, uplimit))
                    targetangle = downlimit < 360 ? downlimit : 0;
                else
                    targetangle = uplimit < 360 ? uplimit : 0;
            }
        }
    }

    public bool SetAngleLimits(float angle1, float angle2)
    {
        downlimit = checkrange(angle1);
        uplimit = checkrange(angle2);

        if (uplimit == 0)
            uplimit = 360;

        checklimits();

        return true;
    }

    public float GetAngleDownLimit()
    {
        return downlimit;
    }

    public float GetAngleUpLimit()
    {
        return uplimit;
    }

    public float DistanceAngle(float angle1, float angle2)
    {
        angle1 = checkrange(angle1);
        angle2 = checkrange(angle2);

        float distance = Mathf.Abs(angle2 - angle1);

        if (distance > 180)
            distance = 360 - distance;

        return distance;
    }

    public void SetTargetAngle(float angle)
    {
        targetangle = checkrange(angle);
        checklimits();
    }

    public float GetTargetAngle()
    {
        return targetangle;
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

        float kP = Proportional;
        float kI = Integral;
        float kD = Differential;

        JointMotor motor = hinge.motor;
        motor.targetVelocity = -Mathf.Sign(deltaAngle) * 1000;
        motor.force = kP * Mathf.Abs(deltaAngle) + kI * deltaSAngle + kD * deltaVelocity;
        motor.freeSpin = false;
        hinge.motor = motor;
        hinge.useMotor = true;
    }
}
