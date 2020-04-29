using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class configcapture1
{
    public bool Kinematic = true;
    public float KinematicAngularVelocity = 100.0f;
    public bool UseGravity = false;
    public float ConnectorMass = 0.2f;
    public float ConnectorDiameter = 0.1f;
    public float ConnectorWidth = 0.02f;
    public float ArmMass = 0.2f;
    public float ArmACSProportional = 0.3f;
    public float ArmACSIntegral = 0.0f;
    public float ArmACSDifferential = 0.2f;
    public float ArmAngle0 = 0.0f;
    public float ArmAngle1 = 360.0f;
    public float ArmDiameter = 0.03f;
    public float ArmLength = 0.4f;
    public float ClamphingeMass = 0.2f;
    public float ClamphingeDiameter = 0.1f;
    public float ClamphingeWidth = 0.03f;
    public float ClampMass = 0.1f;
    public float ClampACSProportional = 0.3f;
    public float ClampACSIntegral = 0.0f;
    public float ClampACSDifferential = 0.2f;
    public float ClampAngle0 = 60.0f;
    public float ClampAngle1 = 300.0f;
    public float ClampDiameter = 0.06f;
    public float ClampWidth = 0.028f;
}

public class capture1 : device
{
    public configcapture1 config = new configcapture1();

    public GameObject holder = null;
    public Vector3 anchorposition = Vector3.zero;
    public Quaternion anchorrotation = Quaternion.identity;

    private bool isinited = false;
    private GameObject connector = null;
    private GameObject armhinge = null;
    private GameObject arm = null;
    private GameObject clamphinge = null;
    private GameObject clamp = null;

    public override void Place()
    {
        if (!isinited)
        {
            //создание из префабов и связи объектов
            connector = GameObject.Instantiate(Resources.Load("capture1/connector", typeof(GameObject)) as GameObject);
            armhinge = GameObject.Instantiate(Resources.Load("capture1/armhinge", typeof(GameObject)) as GameObject);
            arm = GameObject.Instantiate(Resources.Load("capture1/arm", typeof(GameObject)) as GameObject);
            clamphinge = GameObject.Instantiate(Resources.Load("capture1/clamphinge", typeof(GameObject)) as GameObject);
            clamp = GameObject.Instantiate(Resources.Load("capture1/clamp", typeof(GameObject)) as GameObject);

            connector.GetComponent<HingeJoint>().connectedBody = armhinge.GetComponent<Rigidbody>();
            armhinge.GetComponent<FixedJoint>().connectedBody = arm.GetComponent<Rigidbody>();
            arm.GetComponent<HingeJoint>().connectedBody = clamphinge.GetComponent<Rigidbody>();
            clamphinge.GetComponent<FixedJoint>().connectedBody = clamp.GetComponent<Rigidbody>();

            isinited = true;
        }

        {
            connector.GetComponent<Rigidbody>().mass = config.ConnectorMass / 2;
            var b = connector.GetComponent<connectorcapture1>();
            b.drive.KinematicAngularVelocity = config.KinematicAngularVelocity;
            b.drive.Proportional = config.ArmACSProportional;
            b.drive.Integral = config.ArmACSIntegral;
            b.drive.Differential = config.ArmACSDifferential;
            b.holder = holder;
            b.anchorposition = anchorposition;
            b.anchorrotation = anchorrotation;
            b.diameter = config.ConnectorDiameter;
            b.width = config.ConnectorWidth / 2 / b.CylinderFullHeight;
            b.angle0 = config.ArmAngle0;
            b.angle1 = config.ArmAngle1;
        }

        {
            armhinge.GetComponent<Rigidbody>().mass = config.ConnectorMass / 2;
            var b = armhinge.GetComponent<armhingecapture1>();
            b.diameter = config.ConnectorDiameter;
            b.width = config.ConnectorWidth / 2 / b.CylinderFullHeight;
        }

        {
            arm.GetComponent<Rigidbody>().mass = config.ArmMass;
            var b = arm.GetComponent<armcapture1>();
            b.drive.KinematicAngularVelocity = config.KinematicAngularVelocity;
            b.drive.Proportional = config.ClampACSProportional;
            b.drive.Integral = config.ClampACSIntegral;
            b.drive.Differential = config.ClampACSDifferential;
            b.diameter = config.ArmDiameter;
            b.length = config.ArmLength / b.CylinderFullHeight;
            b.angle0 = config.ClampAngle0;
            b.angle1 = config.ClampAngle1;
        }

        {
            clamphinge.GetComponent<Rigidbody>().mass = config.ClamphingeMass;
            var b = clamphinge.GetComponent<clamphingecapture1>();
            b.diameter = config.ClamphingeDiameter;
            b.width = config.ClamphingeWidth / b.CylinderFullHeight;
        }

        {
            clamp.GetComponent<Rigidbody>().mass = config.ClampMass;
            var b = clamp.GetComponent<clampcapture1>();
            b.diameter = config.ClampDiameter;
            b.width = config.ClampWidth / b.CylinderFullHeight;
        }

        connector.GetComponent<Rigidbody>().useGravity = config.UseGravity;
        armhinge.GetComponent<Rigidbody>().useGravity = config.UseGravity;
        arm.GetComponent<Rigidbody>().useGravity = config.UseGravity;
        clamphinge.GetComponent<Rigidbody>().useGravity = config.UseGravity;
        clamp.GetComponent<Rigidbody>().useGravity = config.UseGravity;

        connector.GetComponent<Rigidbody>().isKinematic = config.Kinematic;
        armhinge.GetComponent<Rigidbody>().isKinematic = config.Kinematic;
        arm.GetComponent<Rigidbody>().isKinematic = config.Kinematic;
        clamphinge.GetComponent<Rigidbody>().isKinematic = config.Kinematic;
        clamp.GetComponent<Rigidbody>().isKinematic = config.Kinematic;

        connector.GetComponent<connectorcapture1>().Init();
    }

    public override void Remove()
    {
        MonoBehaviour.Destroy(connector);
        MonoBehaviour.Destroy(armhinge);
        MonoBehaviour.Destroy(arm);
        MonoBehaviour.Destroy(clamphinge);
        MonoBehaviour.Destroy(clamp);

        isinited = false;
        connector = null;
        armhinge = null;
        arm = null;
        clamphinge = null;
        clamp = null;
    }

    public void SetPos(float angle0, float angle1)
    {
        connector.GetComponent<connectorcapture1>().drive.AngleRange.SetTarget(angle0);
        arm.GetComponent<armcapture1>().drive.AngleRange.SetTarget(angle1);
    }

    public float GetPos0()
    {
        return connector.GetComponent<connectorcapture1>().drive.AngleRange.GetTarget();
    }

    public float GetPos1()
    {
        return arm.GetComponent<armcapture1>().drive.AngleRange.GetTarget();
    }
}
