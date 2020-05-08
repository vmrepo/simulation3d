using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class armhingecapture1 : MonoBehaviour
{
    public const int CylinderFullHeight = 2;//it is cylinder, remember for cylinder, local y (height) is half of real
    public GameObject pivotObject = null;
    public CommonJoint joint = new CommonJoint();
    public DriveJoint drive = new DriveJoint();

    public void Init(capture1 device)
    {
        GetComponent<Rigidbody>().mass = device.config.ConnectorMass / 2;
        GetComponent<Rigidbody>().useGravity = device.config.UseGravity;

        transform.localScale = new Vector3(device.config.ConnectorDiameter, device.config.ConnectorWidth / 2 / CylinderFullHeight, device.config.ConnectorDiameter);
        transform.position = pivotObject.transform.rotation * (Vector3.down * device.config.ConnectorWidth / 2) + pivotObject.transform.position;
        transform.rotation = pivotObject.transform.rotation;

        joint.Config(pivotObject, gameObject, device.config.ArmKinematic, JointPhysics.Hinge, Vector3.up, new Vector3(0.0f, -0.5f * CylinderFullHeight, 0.0f));

        drive.KinematicAngularVelocity = device.config.ArmKinematicAngularVelocity;
        drive.Proportional = device.config.ArmACSProportional;
        drive.Integral = device.config.ArmACSIntegral;
        drive.Differential = device.config.ArmACSDifferential;
        drive.Attach(joint);
        drive.AngleRange.SetLimits(device.config.ArmAngle0, device.config.ArmAngle1);
        drive.AngleRange.SetTarget(device.config.ArmAngle0);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        drive.Update();
    }

    public void KinematicUpdate()
    {
        joint.KinematicUpdate();
    }
}
