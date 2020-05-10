using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class configfinger1
{
    public bool UseGravity = false;
    public bool Kinematic = true;
    public float ConnectorMass = 0.01f;
    public float ConnectorHeight = 0.01f;
    public float SectionMass = 0.01f;
    public float SectionHeight = 0.03f;
    public float SectionWidth = 0.015f;
    public float SectionThick = 0.01f;
    public float SectionKinematicAngularVelocity = 100.0f;
    public float SectionACSProportional = 1.5f;
    public float SectionACSIntegral = 0.0f;
    public float SectionACSDifferential = 1.1f;
    public float SectionAngle0 = 315.0f;
    public float SectionAngle1 = 45.0f;
    public int SectionCount = 5;
}

public class finger1 : device
{
    public pivot pivot = new pivot();
    public configfinger1 config = new configfinger1();

    private bool iscreated = false;
    private GameObject connector = null;
    private GameObject connection = null;
    private GameObject section = null;

    public override void Place()
    {
        if (!iscreated)
        {
            //создание из префабов и связи объектов
            connector = GameObject.Instantiate(Resources.Load("finger1/connector", typeof(GameObject)) as GameObject);
            connection = GameObject.Instantiate(Resources.Load("finger1/connection", typeof(GameObject)) as GameObject);
            section = GameObject.Instantiate(Resources.Load("finger1/section", typeof(GameObject)) as GameObject);

            connection.GetComponent<connectionfinger1>().pivotObject = connector;
            section.GetComponent<sectionfinger1>().pivotObject = connection;

            iscreated = true;
        }

        connector.GetComponent<connectorfinger1>().Init(this);
        connection.GetComponent<connectionfinger1>().Init(this);
        section.GetComponent<sectionfinger1>().Init(this);
    }

    public override void Remove()
    {
        MonoBehaviour.Destroy(connector);
        MonoBehaviour.Destroy(connection);
        MonoBehaviour.Destroy(section);

        iscreated = false;
        connector = null;
        connection = null;
        section = null;
    }

    public override void KinematicUpdate()
    {
        connector.GetComponent<connectorfinger1>().KinematicUpdate();
        connection.GetComponent<connectionfinger1>().KinematicUpdate();
        section.GetComponent<sectionfinger1>().KinematicUpdate();
    }

    public void SetPos(float angle0)
    {
        section.GetComponent<sectionfinger1>().drive.AngleRange.SetTarget(angle0);
    }

    public float GetPos0()
    {
        return section.GetComponent<sectionfinger1>().drive.AngleRange.GetTarget();
    }
}
