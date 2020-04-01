using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class manipulator1
{
    private GameObject chassis = null;
    private GameObject rotatingplatform = null;
    private GameObject holder = null;
    private GameObject leverhinge = null;
    private GameObject lever = null;
    private GameObject armhinge = null;
    private GameObject arm = null;

    public void Create()
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
