using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class manipulator1
{
    public float ChassisHeight = 1.0f;
    public float ChassisWidth = 0.5f;
    public float RotatingplatformAngle0 = 0.0f;
    public float RotatingplatformAngle1 = 360.0f;
    public float RotatingplatformDiameter = 0.5f;
    public float RotatingplatformWidth = 0.01f * 2;/*mul 2 for cylinder*/
    public float HolderWidth = 0.03f;
    public float HolderLength = 0.14f;
    public float LeverAngle0 = 10.0f;
    public float LeverAngle1 = 60.0f;
    public float LeverhingeDiameter = 0.08f;
    public float LeverhingeWidth = 0.0115f * 2;/*mul 2 for cylinder*/
    public float LeverWidth = 0.03f;
    public float LeverLength = 0.6f;
    public float ArmAngle0 = 60.0f;
    public float ArmAngle1 = 120.0f;
    public float ArmhingeDiameter = 0.08f;
    public float ArmhingeWidth = 0.0115f * 2;/*mul 2 for cylinder*/
    public float ArmWidth = 0.03f;
    public float ArmLength = 0.6f;

    private bool isinited = false;
    private GameObject chassis = null;
    private GameObject rotatingplatform = null;
    private GameObject holder = null;
    private GameObject leverhinge = null;
    private GameObject lever = null;
    private GameObject armhinge = null;
    private GameObject arm = null;

    public void Place(float x, float y, float z, float a)
    {
        if (!isinited)
        {
            chassis = GameObject.Instantiate(Resources.Load("manipulator1/chassis", typeof(GameObject)) as GameObject);
            rotatingplatform = GameObject.Instantiate(Resources.Load("manipulator1/rotatingplatform", typeof(GameObject)) as GameObject);
            holder = GameObject.Instantiate(Resources.Load("manipulator1/holder", typeof(GameObject)) as GameObject);
            leverhinge = GameObject.Instantiate(Resources.Load("manipulator1/leverhinge", typeof(GameObject)) as GameObject);
            lever = GameObject.Instantiate(Resources.Load("manipulator1/lever", typeof(GameObject)) as GameObject);
            armhinge = GameObject.Instantiate(Resources.Load("manipulator1/armhinge", typeof(GameObject)) as GameObject);
            arm = GameObject.Instantiate(Resources.Load("manipulator1/arm", typeof(GameObject)) as GameObject);

            chassis.GetComponent<HingeJoint>().connectedBody = rotatingplatform.GetComponent<Rigidbody>();
            rotatingplatform.GetComponent<FixedJoint>().connectedBody = holder.GetComponent<Rigidbody>();
            holder.GetComponent<HingeJoint>().connectedBody = leverhinge.GetComponent<Rigidbody>();
            leverhinge.GetComponent<FixedJoint>().connectedBody = lever.GetComponent<Rigidbody>();
            lever.GetComponent<HingeJoint>().connectedBody = armhinge.GetComponent<Rigidbody>();
            armhinge.GetComponent<FixedJoint>().connectedBody = arm.GetComponent<Rigidbody>();

            isinited = true;
        }

        {
            var b = chassis.GetComponent<chassis>();
            b.x = x;
            b.y = y;
            b.z = z;
            b.angle = a;
            b.height = ChassisHeight;
            b.width = ChassisWidth;
            b.angle0 = RotatingplatformAngle0;
            b.angle1 = RotatingplatformAngle1;
        }

        {
            var b = rotatingplatform.GetComponent<rotatingplatform>();
            b.diameter = RotatingplatformDiameter;
            b.width = RotatingplatformWidth / 2;/*div 2 for cylinder*/
        }

        {
            var b = holder.GetComponent<holder>();
            b.width = HolderWidth;
            b.length = HolderLength;
            b.angle0 = LeverAngle0;
            b.angle1 = LeverAngle1;
        }

        {
            var b = leverhinge.GetComponent<leverhinge>();
            b.diameter = LeverhingeDiameter;
            b.width = LeverhingeWidth / 2;/*div 2 for cylinder*/
        }

        {
            var b = lever.GetComponent<lever>();
            b.length = LeverLength;
            b.width = LeverWidth;
            b.angle0 = ArmAngle0;
            b.angle1 = ArmAngle1;
        }

        {
            var b = armhinge.GetComponent<armhinge>();
            b.diameter = ArmhingeDiameter;
            b.width = ArmhingeWidth / 2;/*div 2 for cylinder*/
        }

        {
            var b = arm.GetComponent<arm>();
            b.width = ArmWidth;
            b.length = ArmLength;
        }

        chassis.GetComponent<chassis>().Init();
    }

    public void SetPos0(float angle)
    {
        chassis.GetComponent<chassis>().drive.SetTargetAngle(angle);
    }

    public void SetPos1(float angle)
    {
        holder.GetComponent<holder>().drive.SetTargetAngle(angle);
    }

    public void SetPos2(float angle)
    {
        lever.GetComponent<lever>().drive.SetTargetAngle(angle);
    }

    public float GetPos0()
    {
        return chassis.GetComponent<chassis>().drive.GetTargetAngle();
    }

    public float GetPos1()
    {
        return holder.GetComponent<holder>().drive.GetTargetAngle();
    }

    public float GetPos2()
    {
        return lever.GetComponent<lever>().drive.GetTargetAngle();
    }
}
