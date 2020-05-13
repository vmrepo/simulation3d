using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class connectionfinger1 : MonoBehaviour
{
    public const int CylinderFullHeight = 2;//it is cylinder, remember for cylinder, local y (height) is half of real
    public GameObject pivotObject = null;
    public CommonJoint joint = new CommonJoint();
    public DriveJoint drive = new DriveJoint();

    public void Init(finger1 device)
    {
        GetComponent<Rigidbody>().mass = device.config.SectionMass / 2;
        GetComponent<Rigidbody>().useGravity = device.config.UseGravity;

        transform.localScale = new Vector3(device.config.SectionThick, device.config.SectionWidth / CylinderFullHeight, device.config.SectionThick);
        transform.position = pivotObject.transform.rotation * (Vector3.down * device.config.ConnectorHeight / 2) + pivotObject.transform.position;
        transform.rotation = pivotObject.transform.rotation * Quaternion.AngleAxis(90, Vector3.right);

        joint.Config(pivotObject, gameObject, device.config.Kinematic, JointPhysics.Hinge);

        drive.KinematicAngularVelocity = device.config.SectionKinematicAngularVelocity;
        drive.Proportional = device.config.SectionACSProportional;
        drive.Integral = device.config.SectionACSIntegral;
        drive.Differential = device.config.SectionACSDifferential;
        drive.Attach(joint);
        drive.AngleRange.SetLimits(device.config.SectionAngle0, device.config.SectionAngle1);
        drive.AngleRange.SetTarget(device.config.SectionAngle0);
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
