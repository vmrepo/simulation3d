using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class clamphingecapture1 : MonoBehaviour
{
    public const int CylinderFullHeight = 2;//it is cylinder, remember for cylinder, local y (height) is half of real
    public GameObject pivotObject = null;
    public CommonJoint joint = new CommonJoint();
    public DriveJoint drive = new DriveJoint();

    public void Init(capture1 device)
    {
        GetComponent<Rigidbody>().mass = device.config.ClamphingeMass;
        GetComponent<Rigidbody>().useGravity = device.config.UseGravity;

        transform.localScale = new Vector3(device.config.ClamphingeDiameter, device.config.ClamphingeWidth / CylinderFullHeight, device.config.ClamphingeDiameter);
        transform.position = pivotObject.transform.rotation * (Vector3.down * (device.config.ArmLength / 2 + device.config.ClamphingeDiameter / 2)) + pivotObject.transform.position;
        transform.rotation = pivotObject.transform.rotation * Quaternion.AngleAxis(90, Vector3.forward);

        joint.Config(pivotObject, gameObject, device.config.ClampKinematic, JointPhysics.Hinge, Quaternion.AngleAxis(90, Vector3.forward) * Vector3.up, new Vector3(0.0f, -(0.5f + device.config.ClamphingeDiameter / 2 / device.config.ArmLength) * armcapture1.CylinderFullHeight, 0.0f));

        drive.KinematicAngularVelocity = device.config.ClampKinematicAngularVelocity;
        drive.Proportional = device.config.ClampACSProportional;
        drive.Integral = device.config.ClampACSIntegral;
        drive.Differential = device.config.ClampACSDifferential;
        drive.Attach(joint);
        drive.AngleRange.SetLimits(device.config.ClampAngle0, device.config.ClampAngle1);
        drive.AngleRange.SetTarget(device.config.ClampAngle0);
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
