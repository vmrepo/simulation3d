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
    public delegate void Ontriggerenter(Collider other, int index);
    public GameObject connection = null;
    public GameObject section = null;
    public int index = -1;
    public Ontriggerenter ontriggerenter = null;

    static public finger1section Create(GameObject pivotObject, int index)
    {
        finger1section section = new finger1section();

        section.connection = GameObject.Instantiate(Resources.Load("finger1/connection", typeof(GameObject)) as GameObject);
        section.section = GameObject.Instantiate(Resources.Load("finger1/section", typeof(GameObject)) as GameObject);

        section.connection.GetComponent<connectionfinger1>().pivotObject = pivotObject;
        section.connection.GetComponent<connectionfinger1>().isFirst = (index == 0);
        section.section.GetComponent<sectionfinger1>().pivotObject = section.connection;
        section.section.GetComponent<sectionfinger1>().ontriggerenter = section.OnTriggerEnter;
        section.index = index;

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

    public void SetLimits(float angle0, float angle1)
    {
        connection.GetComponent<connectionfinger1>().drive.AngleRange.SetLimits(angle0, angle1);
    }

    public void SetPos(float angle0)
    {
        connection.GetComponent<connectionfinger1>().drive.AngleRange.SetTarget(angle0);
    }

    public float GetPos0()
    {
        return connection.GetComponent<connectionfinger1>().drive.AngleRange.GetTarget();
    }

    public float GetCurrent0()
    {
        return connection.GetComponent<connectionfinger1>().drive.GetAngle();
    }

    public void OnTriggerEnter(Collider other)
    {
        if (ontriggerenter != null)
        {
            ontriggerenter(other, index);
        }
    }
}

public class finger1 : device
{
    public delegate void Oncaught(GameObject gameObject);
    public Oncaught oncaught = null;

    public pivot pivot = new pivot();
    public configfinger1 config = new configfinger1();

    private bool isclenched = false;//палец сжимается или сжат
    private int caught = -1;//индекс зацепившейся секции пальца

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
                sections.Add(finger1section.Create(pivotObject, i));
                pivotObject = sections[i].section;
            }

            iscreated = true;
        }

        connector.GetComponent<connectorfinger1>().Init(this);

        for (int i = 0; i < config.SectionCount; i++)
        {
            sections[i].Init(this);
            sections[i].ontriggerenter = OnTriggerEnter;
        }
    }

    public override void Remove()
    {
        MonoBehaviour.Destroy(connector);

        for (int i = 0; i < config.SectionCount; i++)
        {
            sections[i].Remove();
        }

        iscreated = false;
        connector = null;

        sections.Clear();
    }

    public override void KinematicUpdate()
    {
        connector.GetComponent<connectorfinger1>().KinematicUpdate();

        for (int i = 0; i < config.SectionCount; i++)
        {
            sections[i].KinematicUpdate();
        }
    }

    public void Clench(bool isclenched_)
    {
        if(!isclenched && isclenched_)
        {
            for (int i = 0; i < config.SectionCount; i++)
            {
                sections[i].SetPos(config.SectionAngle1);
            }
        }

        if (isclenched && !isclenched_)
        {
            for (int i = 0; i < config.SectionCount; i++)
            {
                // на штатные лимиты
                sections[i].SetLimits(config.SectionAngle0, config.SectionAngle1);
                sections[i].SetPos(config.SectionAngle0);
            }

            caught = -1;
        }

        isclenched = isclenched_;
    }

    private void OnTriggerEnter(Collider other, int index)
    {
        if (other.gameObject.GetComponent<thing>() == null)
            return;

        if (!isclenched)
            return;

        if (index <= caught)
            return;

        caught = index;

        // переустановить лимиты, задать целевые углы
        for (int i = 0; i < config.SectionCount; i++)
        {
            if (i <= index)
            {
                sections[i].SetPos(sections[i].GetCurrent0());
            }
            else
            {
                float angle1 = 30;//угол доводки секции, который лучше выглядит
                sections[i].SetLimits(config.SectionAngle0, angle1);
                sections[i].SetPos(angle1);
            }
        }

        if (oncaught != null)
        {
            oncaught(other.gameObject);
        }
    }

    public bool IsCaught()
    {
        return caught != -1;
    }
}
