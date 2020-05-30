using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class clampcapture1 : MonoBehaviour
{
    public const int CylinderFullHeight = 2;//it is cylinder, remember for cylinder, local y (height) is half of real
    public GameObject pivotObject = null;
    public CommonJoint joint = new CommonJoint();
    public DriveJoint drive = new DriveJoint();

    public void Init(capture1 device)
    {
        GetComponent<Rigidbody>().mass = device.config.ClampMass / 2;
        GetComponent<Rigidbody>().useGravity = device.config.UseGravity;

        transform.localScale = new Vector3(device.config.ClampDiameter, device.config.ClampWidth / 2 / CylinderFullHeight, device.config.ClampDiameter);
        transform.position = pivotObject.transform.rotation * (Vector3.down * device.config.ClampWidth / 2) + pivotObject.transform.position;
        transform.rotation = pivotObject.transform.rotation;

        joint.Config(pivotObject, gameObject, device.config.ClampConnectorKinematic, JointPhysics.Hinge);

        drive.KinematicAngularVelocity = device.config.ClampConnectorKinematicAngularVelocity;
        drive.Proportional = device.config.ClampConnectorACSProportional;
        drive.Integral = device.config.ClampConnectorACSIntegral;
        drive.Differential = device.config.ClampConnectorACSDifferential;
        drive.Attach(joint);
        drive.AngleRange.SetLimits(device.config.ClampConnectorAngle0, device.config.ClampConnectorAngle1);
        drive.AngleRange.SetTarget(device.config.ClampConnectorAngle0);
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
