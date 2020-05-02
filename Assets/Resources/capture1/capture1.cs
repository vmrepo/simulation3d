using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class anchor
{
    public GameObject gameobject = null;//объект прикрепления
    public Vector3 position = Vector3.zero;//позиция точки прикрепления в локальных координатах объекта прикрепления    
    public Quaternion rotation = Quaternion.identity;//направление прикрепления в локальных координатах объекта прикрепления
}

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
    public float ClampAngle0 = 240.0f;
    public float ClampAngle1 = 120.0f;
    public float ClampDiameter = 0.06f;
    public float ClampWidth = 0.028f;
}

public class capture1 : device
{
    public anchor anchor = new anchor();
    public configcapture1 config = new configcapture1();

    private bool iscreated = false;
    private GameObject connector = null;
    private GameObject armhinge = null;
    private GameObject arm = null;
    private GameObject clamphinge = null;
    private GameObject clamp = null;

    public override void Place()
    {
        if (!iscreated)
        {
            //создание из префабов и связи объектов
            connector = GameObject.Instantiate(Resources.Load("capture1/connector", typeof(GameObject)) as GameObject);
            armhinge = GameObject.Instantiate(Resources.Load("capture1/armhinge", typeof(GameObject)) as GameObject);
            arm = GameObject.Instantiate(Resources.Load("capture1/arm", typeof(GameObject)) as GameObject);
            clamphinge = GameObject.Instantiate(Resources.Load("capture1/clamphinge", typeof(GameObject)) as GameObject);
            clamp = GameObject.Instantiate(Resources.Load("capture1/clamp", typeof(GameObject)) as GameObject);

            connector.GetComponent<Joint>().connectedBody = armhinge.GetComponent<Rigidbody>();
            armhinge.GetComponent<Joint>().connectedBody = arm.GetComponent<Rigidbody>();
            arm.GetComponent<Joint>().connectedBody = clamphinge.GetComponent<Rigidbody>();
            clamphinge.GetComponent<Joint>().connectedBody = clamp.GetComponent<Rigidbody>();

            iscreated = true;
        }

        connector.GetComponent<connectorcapture1>().Init(this);
        armhinge.GetComponent<armhingecapture1>().Init(this);
        arm.GetComponent<armcapture1>().Init(this);
        clamphinge.GetComponent<clamphingecapture1>().Init(this);
        clamp.GetComponent<clampcapture1>().Init(this);
    }

    public override void Remove()
    {
        MonoBehaviour.Destroy(connector);
        MonoBehaviour.Destroy(armhinge);
        MonoBehaviour.Destroy(arm);
        MonoBehaviour.Destroy(clamphinge);
        MonoBehaviour.Destroy(clamp);

        iscreated = false;
        connector = null;
        armhinge = null;
        arm = null;
        clamphinge = null;
        clamp = null;
    }

    public override void KinematicUpdate()
    {
        connector.GetComponent<connectorcapture1>().KinematicUpdate();
        armhinge.GetComponent<armhingecapture1>().KinematicUpdate();
        arm.GetComponent<armcapture1>().KinematicUpdate();
        clamphinge.GetComponent<clamphingecapture1>().KinematicUpdate();
        clamp.GetComponent<clampcapture1>().KinematicUpdate();
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
