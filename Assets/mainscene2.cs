using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mainscene2 : MonoBehaviour
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

    // Start is called before the first frame update
    void Start()
    {
        chassis = Instantiate(Resources.Load("manipulator2/chassis", typeof(GameObject)) as GameObject);
        rotatingplatform = Instantiate(Resources.Load("manipulator2/rotatingplatform", typeof(GameObject)) as GameObject);
        leverhinge = Instantiate(Resources.Load("manipulator2/leverhinge", typeof(GameObject)) as GameObject);
        lever = Instantiate(Resources.Load("manipulator2/lever", typeof(GameObject)) as GameObject);
        armhinge = Instantiate(Resources.Load("manipulator2/armhinge", typeof(GameObject)) as GameObject);
        arm = Instantiate(Resources.Load("manipulator2/arm", typeof(GameObject)) as GameObject);
        holder1 = Instantiate(Resources.Load("manipulator2/holder1", typeof(GameObject)) as GameObject);
        wheelhinge1 = Instantiate(Resources.Load("manipulator2/wheelhinge1", typeof(GameObject)) as GameObject);
        wheel1 = Instantiate(Resources.Load("manipulator2/wheel1", typeof(GameObject)) as GameObject);
        leverhinge1 = Instantiate(Resources.Load("manipulator2/leverhinge1", typeof(GameObject)) as GameObject);
        lever1 = Instantiate(Resources.Load("manipulator2/lever1", typeof(GameObject)) as GameObject);
        armhinge1 = Instantiate(Resources.Load("manipulator2/armhinge1", typeof(GameObject)) as GameObject);
        holder2 = Instantiate(Resources.Load("manipulator2/holder2", typeof(GameObject)) as GameObject);
        wheelhinge2 = Instantiate(Resources.Load("manipulator2/wheelhinge2", typeof(GameObject)) as GameObject);
        wheel2 = Instantiate(Resources.Load("manipulator2/wheel2", typeof(GameObject)) as GameObject);

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

        GameObject.Find("Main Camera").GetComponent<camera>().target = chassis.transform;
    }

    // Update is called once per frame
    void Update()
    {
        float delta = 10.0f;

        if (Input.GetKeyDown(KeyCode.Q))
        {
            DriveJoint drive = chassis.GetComponent<chassismanipulator2>().drive;
            drive.SetTargetAngle(drive.GetTargetAngle() - delta);
        }

        if (Input.GetKeyDown(KeyCode.W))
        {
            DriveJoint drive = chassis.GetComponent<chassismanipulator2>().drive;
            drive.SetTargetAngle(drive.GetTargetAngle() + delta);
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            DriveJoint drive = lever.GetComponent<levermanipulator2>().drive;
            DriveJoint drive1 = holder1.GetComponent<holder1manipulator2>().drive;
            DriveJoint drive2 = holder2.GetComponent<holder2manipulator2>().drive;

            drive2.SetTargetAngle(drive2.GetTargetAngle() - delta);
            drive1.SetTargetAngle(drive2.GetTargetAngle() + drive.GetTargetAngle());
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            DriveJoint drive = lever.GetComponent<levermanipulator2>().drive;
            DriveJoint drive1 = holder1.GetComponent<holder1manipulator2>().drive;
            DriveJoint drive2 = holder2.GetComponent<holder2manipulator2>().drive;

            drive2.SetTargetAngle(drive2.GetTargetAngle() + delta);
            drive1.SetTargetAngle(drive2.GetTargetAngle() + drive.GetTargetAngle());
        }

        if (Input.GetKeyDown(KeyCode.Z))
        {
            DriveJoint drive = lever.GetComponent<levermanipulator2>().drive;
            DriveJoint drive1 = holder1.GetComponent<holder1manipulator2>().drive;
            DriveJoint drive2 = holder2.GetComponent<holder2manipulator2>().drive;

            drive.SetTargetAngle(drive.GetTargetAngle() - delta);
            drive1.SetTargetAngle(drive2.GetTargetAngle() + drive.GetTargetAngle());
        }

        if (Input.GetKeyDown(KeyCode.X))
        {
            DriveJoint drive = lever.GetComponent<levermanipulator2>().drive;
            DriveJoint drive1 = holder1.GetComponent<holder1manipulator2>().drive;
            DriveJoint drive2 = holder2.GetComponent<holder2manipulator2>().drive;

            drive.SetTargetAngle(drive.GetTargetAngle() + delta);
            drive1.SetTargetAngle(drive2.GetTargetAngle() + drive.GetTargetAngle());
        }
    }
}
