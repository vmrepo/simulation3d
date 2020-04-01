using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class manipulator2
{
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

    public void Create()
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
