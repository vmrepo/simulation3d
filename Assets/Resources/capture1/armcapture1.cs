using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class armcapture1 : MonoBehaviour
{
    public int CylinderFullHeight = 2;//it is cylinder, remember for cylinder, local y (height) is half of real
    public KinematicJoint kinematic = new KinematicJoint();
    public DriveJoint drive = new DriveJoint();

    public void Init(capture1 device)
    {
        GetComponent<Rigidbody>().mass = device.config.ArmMass;
        GetComponent<Rigidbody>().useGravity = device.config.UseGravity;
        GetComponent<Rigidbody>().isKinematic = device.config.Kinematic;

        drive.KinematicAngularVelocity = device.config.ClampKinematicAngularVelocity;
        drive.Proportional = device.config.ClampACSProportional;
        drive.Integral = device.config.ClampACSIntegral;
        drive.Differential = device.config.ClampACSDifferential;

        //следующее звено
        Joint joint = GetComponent<Joint>();
        GameObject next = joint.connectedBody.gameObject;
        clamphingecapture1 nextbehavior = joint.connectedBody.GetComponent<clamphingecapture1>();

        //размещаем следующее звено
        next.transform.localScale = new Vector3(device.config.ClamphingeDiameter, device.config.ClamphingeWidth / nextbehavior.CylinderFullHeight, device.config.ClamphingeDiameter);
        next.transform.position = transform.rotation * (Vector3.down * (device.config.ArmLength / 2 + device.config.ClamphingeDiameter / 2)) + transform.position;
        next.transform.rotation = transform.rotation * Quaternion.AngleAxis(90, Vector3.forward);

        //ось и якорь шарнира
        joint.axis = Quaternion.AngleAxis(90, Vector3.forward) * Vector3.up;
        joint.anchor = new Vector3(0.0f, -(0.5f + device.config.ClamphingeDiameter / 2 / device.config.ArmLength) * CylinderFullHeight, 0.0f);

        //кинематическая связь
        kinematic.AttachGameObject(gameObject);

        //привод шарнира
        drive.AttachKinematic(kinematic);
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
        kinematic.Update();
    }
}
