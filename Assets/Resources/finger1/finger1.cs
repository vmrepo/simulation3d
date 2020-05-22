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
    public float RibHeight = 0.01f;
    public float SectionMass = 0.01f;
    public float SectionHeight = 0.03f;
    public float SectionWidth = 0.015f;
    public float SectionThick = 0.01f;
    public float SectionKinematicAngularVelocity = 100.0f;
    public float SectionACSProportional = 1.5f;
    public float SectionACSIntegral = 0.0f;
    public float SectionACSDifferential = 1.1f;
    public float SectionAngle0 = 340.0f;
    public float SectionAngle1 = 10.0f;
    public int SectionCount = 5;
}

public class finger1section
{
    public GameObject connection = null;
    public GameObject section = null;

    static public finger1section Create(GameObject pivotObject, bool isFirst)
    {
        finger1section section = new finger1section();

        section.connection = GameObject.Instantiate(Resources.Load("finger1/connection", typeof(GameObject)) as GameObject);
        section.section = GameObject.Instantiate(Resources.Load("finger1/section", typeof(GameObject)) as GameObject);

        section.connection.GetComponent<connectionfinger1>().pivotObject = pivotObject;
        section.connection.GetComponent<connectionfinger1>().isFirst = isFirst;
        section.section.GetComponent<sectionfinger1>().pivotObject = section.connection;

        return section;
    }

    public void Remove()
    {
        MonoBehaviour.Destroy(connection);
        MonoBehaviour.Destroy(section);

        connection = null;
        section = null;
    }

    public void Init(finger1 device)
    {
        connection.GetComponent<connectionfinger1>().Init(device);
        section.GetComponent<sectionfinger1>().Init(device);
    }

    public void KinematicUpdate()
    {
        connection.GetComponent<connectionfinger1>().KinematicUpdate();
        section.GetComponent<sectionfinger1>().KinematicUpdate();
    }

    public void SetPos(float angle0)
    {
        connection.GetComponent<connectionfinger1>().drive.AngleRange.SetTarget(angle0);
    }

    public float GetPos0()
    {
        return connection.GetComponent<connectionfinger1>().drive.AngleRange.GetTarget();
    }
}

public class finger1 : device
{
    public pivot pivot = new pivot();
    public configfinger1 config = new configfinger1();

    private bool iscreated = false;
    private GameObject connector = null;

    private List<finger1section> sections = new List<finger1section>();

    public override void Place()
    {
        if (!iscreated)
        {
            //создание из префабов и связи объектов
            connector = GameObject.Instantiate(Resources.Load("finger1/connector", typeof(GameObject)) as GameObject);

            GameObject pivotObject = connector;
            for (int i = 0; i < config.SectionCount; i++)
            {
                sections.Add(finger1section.Create(pivotObject, i == 0));
                pivotObject = sections[i].section;
            }

            iscreated = true;
        }

        connector.GetComponent<connectorfinger1>().Init(this);

        for (int i = 0; i < config.SectionCount; i++)
            sections[i].Init(this);
    }

    public override void Remove()
    {
        MonoBehaviour.Destroy(connector);

        for (int i = 0; i < config.SectionCount; i++)
            sections[i].Remove();

        iscreated = false;
        connector = null;

        sections.Clear();
    }

    public override void KinematicUpdate()
    {
        connector.GetComponent<connectorfinger1>().KinematicUpdate();

        for (int i = 0; i < config.SectionCount; i++)
            sections[i].KinematicUpdate();
    }

    public void SetPos(float angle0)
    {
        for (int i = 0; i < config.SectionCount; i++)
            sections[i].SetPos(angle0);
    }

    public float GetPos0()
    {
        return sections[0].GetPos0();
    }
}
