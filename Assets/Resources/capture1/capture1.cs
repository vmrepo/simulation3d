using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class configcapture1
{
    public bool UseGravity = false;
    public float ConnectorMass = 0.2f;
    public float ConnectorDiameter = 0.1f;
    public float ConnectorWidth = 0.02f;
    public bool ArmKinematic = true;
    public float ArmMass = 0.2f;
    public float ArmKinematicAngularVelocity = 100.0f;
    public float ArmACSProportional = 1.5f;
    public float ArmACSIntegral = 0.0f;
    public float ArmACSDifferential = 1.1f;
    public float ArmAngle0 = 0.0f;
    public float ArmAngle1 = 360.0f;
    public float ArmDiameter = 0.03f;
    public float ArmLength = 0.4f;
    public float ClamphingeMass = 0.2f;
    public float ClamphingeDiameter = 0.1f;
    public float ClamphingeWidth = 0.03f;
    public bool ClampKinematic = true;
    public float ClampMass = 0.1f;
    public float ClampKinematicAngularVelocity = 100.0f;
    public float ClampACSProportional = 1.5f;
    public float ClampACSIntegral = 0.0f;
    public float ClampACSDifferential = 1.1f;
    public float ClampAngle0 = 240.0f;
    public float ClampAngle1 = 120.0f;
    public float ClampDiameter = 0.06f;
    public float ClampWidth = 0.028f;
    public bool ClampConnectorKinematic = true;
    public float ClampConnectorKinematicAngularVelocity = 100.0f;
    public float ClampConnectorACSProportional = 1.5f;
    public float ClampConnectorACSIntegral = 0.0f;
    public float ClampConnectorACSDifferential = 1.1f;
    public float ClampConnectorAngle0 = 0.0f;
    public float ClampConnectorAngle1 = 360.0f;

}

public class capture1 : device
{
    public pivot pivot = new pivot();
    public configcapture1 config = new configcapture1();

    private bool iscreated = false;
    private GameObject connector = null;
    private GameObject armhinge = null;
    private GameObject arm = null;
    private GameObject clamphinge = null;
    private GameObject clampconnector = null;
    private GameObject clamp = null;

    public GameObject GetClamp()
    {
        return clamp;
    }

    public override void Place()
    {
        if (!iscreated)
        {
            //создание из префабов и связи объектов
            connector = GameObject.Instantiate(Resources.Load("capture1/connector", typeof(GameObject)) as GameObject);
            armhinge = GameObject.Instantiate(Resources.Load("capture1/armhinge", typeof(GameObject)) as GameObject);
            arm = GameObject.Instantiate(Resources.Load("capture1/arm", typeof(GameObject)) as GameObject);
            clamphinge = GameObject.Instantiate(Resources.Load("capture1/clamphinge", typeof(GameObject)) as GameObject);
            clampconnector = GameObject.Instantiate(Resources.Load("capture1/clampconnector", typeof(GameObject)) as GameObject);
            clamp = GameObject.Instantiate(Resources.Load("capture1/clamp", typeof(GameObject)) as GameObject);

            armhinge.GetComponent<armhingecapture1>().pivotObject = connector;
            arm.GetComponent<armcapture1>().pivotObject = armhinge;
            clamphinge.GetComponent<clamphingecapture1>().pivotObject = arm;
            clampconnector.GetComponent<clampconnectorcapture1>().pivotObject = clamphinge;
            clamp.GetComponent<clampcapture1>().pivotObject = clampconnector;

            iscreated = true;
        }

        connector.GetComponent<connectorcapture1>().Init(this);
        armhinge.GetComponent<armhingecapture1>().Init(this);
        arm.GetComponent<armcapture1>().Init(this);
        clamphinge.GetComponent<clamphingecapture1>().Init(this);
        clampconnector.GetComponent<clampconnectorcapture1>().Init(this);
        clamp.GetComponent<clampcapture1>().Init(this);
    }

    public override void Remove()
    {
        MonoBehaviour.Destroy(connector);
        MonoBehaviour.Destroy(armhinge);
        MonoBehaviour.Destroy(arm);
        MonoBehaviour.Destroy(clamphinge);
        MonoBehaviour.Destroy(clampconnector);
        MonoBehaviour.Destroy(clamp);

        iscreated = false;
        connector = null;
        armhinge = null;
        arm = null;
        clamphinge = null;
        clampconnector = null;
        clamp = null;
    }

    public override void KinematicUpdate()
    {
        connector.GetComponent<connectorcapture1>().KinematicUpdate();
        armhinge.GetComponent<armhingecapture1>().KinematicUpdate();
        arm.GetComponent<armcapture1>().KinematicUpdate();
        clamphinge.GetComponent<clamphingecapture1>().KinematicUpdate();
        clampconnector.GetComponent<clampconnectorcapture1>().KinematicUpdate();
        clamp.GetComponent<clampcapture1>().KinematicUpdate();
    }

    public void SetPos(float angle0, float angle1, float angle2)
    {
        armhinge.GetComponent<armhingecapture1>().drive.AngleRange.SetTarget(angle0);
        clamphinge.GetComponent<clamphingecapture1>().drive.AngleRange.SetTarget(angle1);
        clamp.GetComponent<clampcapture1>().drive.AngleRange.SetTarget(angle2);
    }

    public float GetPos0()
    {
        return armhinge.GetComponent<armhingecapture1>().drive.AngleRange.GetTarget();
    }

    public float GetPos1()
    {
        return clamphinge.GetComponent<clamphingecapture1>().drive.AngleRange.GetTarget();
    }

    public float GetPos2()
    {
        return clamp.GetComponent<clampcapture1>().drive.AngleRange.GetTarget();
    }
}
