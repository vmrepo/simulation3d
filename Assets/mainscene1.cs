using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mainscene1 : MonoBehaviour
{
    private GameObject chassis = null;
    private GameObject rotatingplatform = null;
    private GameObject holder = null;
    private GameObject leverhinge = null;
    private GameObject lever = null;
    private GameObject armhinge = null;
    private GameObject arm = null;

    // Start is called before the first frame update
    void Start()
    {
        chassis = Instantiate(Resources.Load("manipulator1/chassis", typeof(GameObject)) as GameObject);
        rotatingplatform = Instantiate(Resources.Load("manipulator1/rotatingplatform", typeof(GameObject)) as GameObject);
        holder = Instantiate(Resources.Load("manipulator1/holder", typeof(GameObject)) as GameObject);
        leverhinge = Instantiate(Resources.Load("manipulator1/leverhinge", typeof(GameObject)) as GameObject);
        lever = Instantiate(Resources.Load("manipulator1/lever", typeof(GameObject)) as GameObject);
        armhinge = Instantiate(Resources.Load("manipulator1/armhinge", typeof(GameObject)) as GameObject);
        arm = Instantiate(Resources.Load("manipulator1/arm", typeof(GameObject)) as GameObject);

        chassis.GetComponent<HingeJoint>().connectedBody = rotatingplatform.GetComponent<Rigidbody>();
        rotatingplatform.GetComponent<FixedJoint>().connectedBody = holder.GetComponent<Rigidbody>();
        holder.GetComponent<HingeJoint>().connectedBody = leverhinge.GetComponent<Rigidbody>();
        leverhinge.GetComponent<FixedJoint>().connectedBody = lever.GetComponent<Rigidbody>();
        lever.GetComponent<HingeJoint>().connectedBody = armhinge.GetComponent<Rigidbody>();
        armhinge.GetComponent<FixedJoint>().connectedBody = arm.GetComponent<Rigidbody>();

        chassis.GetComponent<chassis>().Init();

        GameObject.Find("Main Camera").GetComponent<camera>().target = chassis.transform;
    }

    // Update is called once per frame
    void Update()
    {
        float delta = 30.0f;

        if (Input.GetKeyDown(KeyCode.Q))
        {
            DriveJoint drive = chassis.GetComponent<chassis>().drive;
            float a = drive.GetTargetAngle();
            drive.SetTargetAngle(a - delta);
        }

        if (Input.GetKeyDown(KeyCode.W))
        {
            DriveJoint drive = chassis.GetComponent<chassis>().drive;
            float a = drive.GetTargetAngle();
            drive.SetTargetAngle(a + delta);
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            DriveJoint drive = holder.GetComponent<holder>().drive;
            float a = drive.GetTargetAngle();
            drive.SetTargetAngle(a - delta);
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            DriveJoint drive = holder.GetComponent<holder>().drive;
            float a = drive.GetTargetAngle();
            drive.SetTargetAngle(a + delta);
        }

        if (Input.GetKeyDown(KeyCode.Z))
        {
            DriveJoint drive = lever.GetComponent<lever>().drive;
            float a = drive.GetTargetAngle();
            drive.SetTargetAngle(a - delta);
        }

        if (Input.GetKeyDown(KeyCode.X))
        {
            DriveJoint drive = lever.GetComponent<lever>().drive;
            float a = drive.GetTargetAngle();
            drive.SetTargetAngle(a + delta);
        }
    }
}
