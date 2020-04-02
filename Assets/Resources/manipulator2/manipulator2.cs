﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class manipulator2
{
    public float ChassisHeight = 1.0f;
    public float ChassisWidth = 0.5f;
    public float RotatingplatformAngle0 = 0.0f;
    public float RotatingplatformAngle1 = 360.0f;
    public float RotatingplatformDiameter = 0.5f;
    public float RotatingplatformWidth = 0.01f * 2;/*mul 2 for cylinder*/
    public float LeverhingeDiameter = 0.08f;
    public float LeverWidth = 0.03f;
    public float LeverLength = 0.6f;
    public float ArmAngle0 = 60.0f;
    public float ArmAngle1 = 120.0f;
    public float ArmhingeDiameter = 0.08f;
    public float ArmhingeWidth = 0.0115f * 2;/*mul 2 for cylinder*/
    public float ArmWidth = 0.03f;
    public float ArmLength = 0.9f;
    public float HolderWidth = 0.03f;
    public float HolderLength = 0.14f;
    public float HoldersDistance = 2 * 0.0895f;
    public float WheelhingeWidth = 0.023f * 2;/*mul 2 for cylinder*/
    public float WheelDiameter = 0.32f;
    public float WheelWidth = 0.0115f * 2;/*mul 2 for cylinder*/
    public float WheelLever = 0.12f;
    public float LeverAngle0 = 10.0f;
    public float LeverAngle1 = 60.0f;

    private bool isinited = false;
    private GameObject chassis = null;
    private GameObject rotatingplatform = null;
    private GameObject leverhinge = null;
    private GameObject lever = null;
    private GameObject armhinge = null;
    private GameObject arm = null;
    private GameObject holder1 = null;
    private GameObject wheelhinge1 = null;
    private GameObject wheel1 = null;
    private GameObject leverhinge1 = null;
    private GameObject lever1 = null;
    private GameObject armhinge1 = null;
    private GameObject holder2 = null;
    private GameObject wheelhinge2 = null;
    private GameObject wheel2 = null;

    public void Place(float x, float y, float z, float a)
    {
        if (!isinited)
        {
            chassis = GameObject.Instantiate(Resources.Load("manipulator2/chassis", typeof(GameObject)) as GameObject);
            rotatingplatform = GameObject.Instantiate(Resources.Load("manipulator2/rotatingplatform", typeof(GameObject)) as GameObject);
            leverhinge = GameObject.Instantiate(Resources.Load("manipulator2/leverhinge", typeof(GameObject)) as GameObject);
            lever = GameObject.Instantiate(Resources.Load("manipulator2/lever", typeof(GameObject)) as GameObject);
            armhinge = GameObject.Instantiate(Resources.Load("manipulator2/armhinge", typeof(GameObject)) as GameObject);
            arm = GameObject.Instantiate(Resources.Load("manipulator2/arm", typeof(GameObject)) as GameObject);
            holder1 = GameObject.Instantiate(Resources.Load("manipulator2/holder1", typeof(GameObject)) as GameObject);
            wheelhinge1 = GameObject.Instantiate(Resources.Load("manipulator2/wheelhinge1", typeof(GameObject)) as GameObject);
            wheel1 = GameObject.Instantiate(Resources.Load("manipulator2/wheel1", typeof(GameObject)) as GameObject);
            leverhinge1 = GameObject.Instantiate(Resources.Load("manipulator2/leverhinge1", typeof(GameObject)) as GameObject);
            lever1 = GameObject.Instantiate(Resources.Load("manipulator2/lever1", typeof(GameObject)) as GameObject);
            armhinge1 = GameObject.Instantiate(Resources.Load("manipulator2/armhinge1", typeof(GameObject)) as GameObject);
            holder2 = GameObject.Instantiate(Resources.Load("manipulator2/holder2", typeof(GameObject)) as GameObject);
            wheelhinge2 = GameObject.Instantiate(Resources.Load("manipulator2/wheelhinge2", typeof(GameObject)) as GameObject);
            wheel2 = GameObject.Instantiate(Resources.Load("manipulator2/wheel2", typeof(GameObject)) as GameObject);

            chassis.GetComponent<HingeJoint>().connectedBody = rotatingplatform.GetComponent<Rigidbody>();
            rotatingplatform.GetComponents<FixedJoint>()[0].connectedBody = holder1.GetComponent<Rigidbody>();
            rotatingplatform.GetComponents<FixedJoint>()[1].connectedBody = holder2.GetComponent<Rigidbody>();
            leverhinge.GetComponent<FixedJoint>().connectedBody = lever.GetComponent<Rigidbody>();
            lever.GetComponent<HingeJoint>().connectedBody = armhinge.GetComponent<Rigidbody>();
            armhinge.GetComponent<FixedJoint>().connectedBody = arm.GetComponent<Rigidbody>();
            arm.GetComponent<HingeJoint>().connectedBody = armhinge1.GetComponent<Rigidbody>();
            holder1.GetComponent<HingeJoint>().connectedBody = wheelhinge1.GetComponent<Rigidbody>();
            wheelhinge1.GetComponent<FixedJoint>().connectedBody = wheel1.GetComponent<Rigidbody>();
            wheel1.GetComponent<FixedJoint>().connectedBody = leverhinge1.GetComponent<Rigidbody>();
            lever1.GetComponents<HingeJoint>()[0].connectedBody = leverhinge1.GetComponent<Rigidbody>();
            lever1.GetComponents<HingeJoint>()[1].connectedBody = armhinge1.GetComponent<Rigidbody>();
            holder2.GetComponent<HingeJoint>().connectedBody = wheelhinge2.GetComponent<Rigidbody>();
            wheelhinge2.GetComponent<FixedJoint>().connectedBody = wheel2.GetComponent<Rigidbody>();
            wheel2.GetComponent<FixedJoint>().connectedBody = leverhinge.GetComponent<Rigidbody>();

            armhinge.GetComponent<armhingemanipulator2>().wheel1object = wheel1;
            armhinge.GetComponent<armhingemanipulator2>().armhinge1object = armhinge1;
            leverhinge1.GetComponent<leverhinge1manipulator2>().lever1object = lever1;
            wheel1.GetComponent<wheel1manipulator2>().lever1object = lever1;
            wheel1.GetComponent<wheel1manipulator2>().rotatingplatformobject = rotatingplatform;
            wheel2.GetComponent<wheel2manipulator2>().leverobject = lever;
            wheel2.GetComponent<wheel2manipulator2>().rotatingplatformobject = rotatingplatform;

            isinited = true;
        }

        {
            var b = chassis.GetComponent<chassismanipulator2>();
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
            var b = rotatingplatform.GetComponent<rotatingplatformmanipulator2>();
            b.diameter = RotatingplatformDiameter;
            b.width = RotatingplatformWidth / 2;/*div 2 for cylinder*/
        }

        {
            var b = leverhinge.GetComponent<leverhingemanipulator2>();
            b.diameter = LeverhingeDiameter;
        }

        {
            var b = lever.GetComponent<levermanipulator2>();
            b.width = LeverWidth;
            b.length = LeverLength;
            b.angle0 = ArmAngle0;
            b.angle1 = ArmAngle1;
        }

        {
            var b = armhinge.GetComponent<armhingemanipulator2>();
            b.diameter = ArmhingeDiameter;
            b.width = ArmhingeWidth / 2;/*div 2 for cylinder*/
        }

        {
            var b = arm.GetComponent<armmanipulator2>();
            b.width = ArmWidth;
            b.length = ArmLength;
        }

        {
            var b = holder1.GetComponent<holder1manipulator2>();
            b.width = HolderWidth;
            b.length = HolderLength;
            b.offset = HoldersDistance / 2;
            b.angle0 = ArmAngle0 + LeverAngle0;
            b.angle1 = ArmAngle1 + LeverAngle1;
        }

        {
            var b = wheelhinge1.GetComponent<wheelhinge1manipulator2>();
            b.diameter = LeverhingeDiameter;
            b.width = WheelhingeWidth / 2;/*div 2 for cylinder*/
        }

        {
            var b = wheel1.GetComponent<wheel1manipulator2>();
            b.diameter = WheelDiameter;
            b.width = WheelWidth / 2;/*div 2 for cylinder*/
            b.lever = WheelLever;
        }

        {
            var b = leverhinge1.GetComponent<leverhinge1manipulator2>();
            b.diameter = LeverhingeDiameter;
        }

        {
            var b = lever1.GetComponent<lever1manipulator2>();
            b.width = LeverWidth;
            b.length = LeverLength;
        }

        {
            var b = armhinge1.GetComponent<armhinge1manipulator2>();
            b.diameter = ArmhingeDiameter;
            b.width = ArmhingeWidth / 2;/*div 2 for cylinder*/
        }

        {
            var b = holder2.GetComponent<holder2manipulator2>();
            b.width = HolderWidth;
            b.length = HolderLength;
            b.offset = HoldersDistance / 2;
            b.angle0 = LeverAngle0;
            b.angle1 = LeverAngle1;
        }

        {
            var b = wheelhinge2.GetComponent<wheelhinge2manipulator2>();
            b.diameter = LeverhingeDiameter;
            b.width = WheelhingeWidth / 2;/*div 2 for cylinder*/
        }

        {
            var b = wheel2.GetComponent<wheel2manipulator2>();
            b.diameter = WheelDiameter;
            b.width = WheelWidth / 2;/*div 2 for cylinder*/
        }

        chassis.GetComponent<chassismanipulator2>().Init();
    }

    public void SetPos0(float angle)
    {
        DriveJoint drive = chassis.GetComponent<chassismanipulator2>().drive;
        drive.SetTargetAngle(angle);
    }

    public void SetPos1(float angle)
    {
        DriveJoint drive = lever.GetComponent<levermanipulator2>().drive;
        DriveJoint drive1 = holder1.GetComponent<holder1manipulator2>().drive;
        DriveJoint drive2 = holder2.GetComponent<holder2manipulator2>().drive;

        drive2.SetTargetAngle(angle);
        drive1.SetTargetAngle(drive2.GetTargetAngle() + drive.GetTargetAngle());
    }

    public void SetPos2(float angle)
    {
        DriveJoint drive = lever.GetComponent<levermanipulator2>().drive;
        DriveJoint drive1 = holder1.GetComponent<holder1manipulator2>().drive;
        DriveJoint drive2 = holder2.GetComponent<holder2manipulator2>().drive;

        drive.SetTargetAngle(angle);
        drive1.SetTargetAngle(drive2.GetTargetAngle() + drive.GetTargetAngle());
    }

    public float GetPos0()
    {
        return chassis.GetComponent<chassismanipulator2>().drive.GetTargetAngle();
    }

    public float GetPos1()
    {
        return holder2.GetComponent<holder2manipulator2>().drive.GetTargetAngle();
    }

    public float GetPos2()
    {
        return lever.GetComponent<levermanipulator2>().drive.GetTargetAngle();
    }
}