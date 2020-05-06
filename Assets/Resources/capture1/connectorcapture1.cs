using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class connectorcapture1 : MonoBehaviour
{
    public int CylinderFullHeight = 2;//it is cylinder, remember for cylinder, local y (height) is half of real
    public KinematicJoint kinematicAnchor = new KinematicJoint();
    public KinematicJoint kinematic = new KinematicJoint();
    public DriveJoint drive = new DriveJoint();

    public void Init(capture1 device)
    {
        GetComponent<Rigidbody>().mass = device.config.ConnectorMass / 2;
        GetComponent<Rigidbody>().useGravity = device.config.UseGravity;
        GetComponent<Rigidbody>().isKinematic = device.config.Kinematic;

        drive.KinematicAngularVelocity = device.config.ArmKinematicAngularVelocity;
        drive.Proportional = device.config.ArmACSProportional;
        drive.Integral = device.config.ArmACSIntegral;
        drive.Differential = device.config.ArmACSDifferential;

        {
            //соединяем, размещаем
            Joint joint = device.anchor.fixedjoint;
            joint.connectedBody = GetComponent<Rigidbody>();
            transform.localScale = new Vector3(device.config.ConnectorDiameter, device.config.ConnectorWidth / 2 / CylinderFullHeight, device.config.ConnectorDiameter);
            transform.position = device.anchor.gameobject.transform.rotation * (device.anchor.position + Vector3.down * device.config.ConnectorWidth / 4) + device.anchor.gameobject.transform.position;
            transform.rotation = device.anchor.gameobject.transform.rotation * device.anchor.rotation;
            //ось и якорь
            joint.axis = Vector3.up;
            joint.anchor = new Vector3(0.0f, 0.0f, 0.0f);
        }

        {
            //следующее звено
            Joint joint = GetComponent<Joint>();
            GameObject next = joint.connectedBody.gameObject;
            armhingecapture1 nextbehavior = joint.connectedBody.GetComponent<armhingecapture1>();

            //размещаем следующее звено
            next.transform.localScale = new Vector3(device.config.ConnectorDiameter, device.config.ConnectorWidth / 2 / nextbehavior.CylinderFullHeight, device.config.ConnectorDiameter);
            next.transform.position = transform.rotation * (Vector3.down * device.config.ConnectorWidth / 2) + transform.position;
            next.transform.rotation = transform.rotation;

            //ось и якорь шарнира
            joint.axis = Vector3.up;
            joint.anchor = new Vector3(0.0f, -0.5f * CylinderFullHeight, 0.0f);
        }

        //кинематическая связь
        kinematicAnchor.AttachGameObject(device.anchor.gameobject, gameObject);
        kinematic.AttachGameObject(gameObject);

        //привод шарнира
        drive.AttachKinematic(kinematic);
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
        kinematicAnchor.Update();
        kinematic.Update();
    }
}
