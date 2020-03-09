using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DriveJoint
{
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

    public bool SetAngleLimits(float angle1, float angle2)
    {
        //приводим к даиапазону [0, 360)
        angle1 = angle1 % 360;
        if (angle1 < 0)
            angle1 += 360;

        //приводим к даиапазону [0, 360)
        angle2 = angle2 % 360;
        if (angle2 < 0)
            angle2 += 360;

        if (angle2 == 0)
            angle2 = 360;

        if (angle1 >= angle2)
            return false;

        downlimit = angle1;
        uplimit = angle2;

        //контролируем ограничение
        if (targetangle < downlimit || uplimit <= targetangle)
        {
            if (DistanceAngle(targetangle, downlimit) < DistanceAngle(targetangle, uplimit))
                targetangle = downlimit;
            else
                targetangle = uplimit < 360 ? uplimit : 0;
        }

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
        //приводим к даиапзону [0, 360)
        angle1 = angle1 % 360;
        if (angle1 < 0)
            angle1 += 360;

        //приводим к даиапзону [0, 360)
        angle2 = angle2 % 360;
        if (angle2 < 0)
            angle2 += 360;

        float distance = Mathf.Abs(angle2 - angle1);

        if (distance > 180)
            distance = 360 - distance;

        return distance;
    }

    public void SetTargetAngle(float angle)
    {
        //приводим к даиапзону [0, 360)
        angle = angle % 360;
        if (angle < 0)
            angle += 360;

        targetangle = angle;

        //контролируем ограничение
        if (targetangle < downlimit || uplimit <= targetangle)
        {
            if (DistanceAngle(targetangle, downlimit) < DistanceAngle(targetangle, uplimit))
                targetangle = downlimit;
            else
                targetangle = uplimit < 360 ? uplimit : 0;
        }
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

        float kP = 1.5f;
        float kI = 0.0f;
        float kD = 1.1f;

        JointMotor motor = hinge.motor;
        motor.targetVelocity = -Mathf.Sign(deltaAngle) * 1000;
        motor.force = kP * Mathf.Abs(deltaAngle) + kI * deltaSAngle + kD * deltaVelocity;
        motor.freeSpin = false;
        hinge.motor = motor;
        hinge.useMotor = true;
    }
}
