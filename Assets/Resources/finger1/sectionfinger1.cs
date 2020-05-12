using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sectionfinger1 : MonoBehaviour
{
    public GameObject pivotObject = null;
    public CommonJoint joint = new CommonJoint();
    public DriveJoint drive = new DriveJoint();

    public void Init(finger1 device)
    {
        GetComponent<Rigidbody>().mass = device.config.SectionMass;
        GetComponent<Rigidbody>().useGravity = device.config.UseGravity;

        transform.localScale = new Vector3(device.config.SectionThick, device.config.SectionHeight, device.config.SectionWidth);
        transform.position = pivotObject.transform.rotation * Quaternion.AngleAxis(90, Vector3.left) * (Vector3.down * device.config.SectionHeight / 2) + pivotObject.transform.position;
        transform.rotation = pivotObject.transform.rotation * Quaternion.AngleAxis(90, Vector3.left);

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
