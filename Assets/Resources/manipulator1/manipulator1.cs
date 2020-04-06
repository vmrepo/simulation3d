using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class configmanipulator1
{
    public float x = 0.0f;
    public float y = 0.0f;
    public float z = 0.0f;
    public float angle = 0.0f;
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
}

public class manipulator1 : device
{
    public configmanipulator1 config = null;

    private bool isinited = false;
    private GameObject chassis = null;
    private GameObject rotatingplatform = null;
    private GameObject holder = null;
    private GameObject leverhinge = null;
    private GameObject lever = null;
    private GameObject armhinge = null;
    private GameObject arm = null;

    public override void Place()
    {
        if (config == null)
        {
            config = new configmanipulator1();
        }

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
            b.x = config.x;
            b.y = config.y;
            b.z = config.z;
            b.angle = config.angle;
            b.height = config.ChassisHeight;
            b.width = config.ChassisWidth;
            b.angle0 = config.RotatingplatformAngle0;
            b.angle1 = config.RotatingplatformAngle1;
        }

        {
            var b = rotatingplatform.GetComponent<rotatingplatform>();
            b.diameter = config.RotatingplatformDiameter;
            b.width = config.RotatingplatformWidth / 2;/*div 2 for cylinder*/
        }

        {
            var b = holder.GetComponent<holder>();
            b.width = config.HolderWidth;
            b.length = config.HolderLength;
            b.angle0 = config.LeverAngle0;
            b.angle1 = config.LeverAngle1;
        }

        {
            var b = leverhinge.GetComponent<leverhinge>();
            b.diameter = config.LeverhingeDiameter;
            b.width = config.LeverhingeWidth / 2;/*div 2 for cylinder*/
        }

        {
            var b = lever.GetComponent<lever>();
            b.length = config.LeverLength;
            b.width = config.LeverWidth;
            b.angle0 = config.ArmAngle0;
            b.angle1 = config.ArmAngle1;
        }

        {
            var b = armhinge.GetComponent<armhinge>();
            b.diameter = config.ArmhingeDiameter;
            b.width = config.ArmhingeWidth / 2;/*div 2 for cylinder*/
        }

        {
            var b = arm.GetComponent<arm>();
            b.width = config.ArmWidth;
            b.length = config.ArmLength;
        }

        chassis.GetComponent<chassis>().Init();
    }

    public override void Remove()
    {
        MonoBehaviour.Destroy(chassis);
        MonoBehaviour.Destroy(rotatingplatform);
        MonoBehaviour.Destroy(holder);
        MonoBehaviour.Destroy(leverhinge);
        MonoBehaviour.Destroy(lever);
        MonoBehaviour.Destroy(armhinge);
        MonoBehaviour.Destroy(arm);

        isinited = false;
        chassis = null;
        rotatingplatform = null;
        holder = null;
        leverhinge = null;
        lever = null;
        armhinge = null;
        arm = null;
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
