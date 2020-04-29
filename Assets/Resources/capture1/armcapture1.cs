using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class armcapture1 : MonoBehaviour
{
    public DriveJoint drive = new DriveJoint();

    public int CylinderFullHeight = 2;//it is cylinder, remember for cylinder, local y (height) is half of real
    public float diameter = 0.03f;
    public float length = 0.2f;
    public float angle0 = 30.0f;
    public float angle1 = 330.0f;

    public void Init()
    {
        //следующее звено
        HingeJoint hinge = GetComponent<HingeJoint>();
        GameObject next = hinge.connectedBody.gameObject;
        clamphingecapture1 nextbehavior = hinge.connectedBody.GetComponent<clamphingecapture1>();

        //размещаем следующее звено
        next.transform.localScale = new Vector3(nextbehavior.diameter, nextbehavior.width, nextbehavior.diameter);
        next.transform.position = transform.rotation * (Vector3.down * (length * CylinderFullHeight / 2 + nextbehavior.diameter / 2)) + transform.position;
        next.transform.rotation = transform.rotation * Quaternion.AngleAxis(90, Vector3.forward);

        //ось и якорь шарнира
        hinge.axis = Quaternion.AngleAxis(90, Vector3.forward) * Vector3.down;
        hinge.anchor = new Vector3(0.0f, -(0.5f * CylinderFullHeight + nextbehavior.diameter / length / 2), 0.0f);

        //настраиваем привод шарнира
        drive.AttachGameObject(gameObject);
        drive.AngleRange.SetLimits(angle0, angle1);
        drive.AngleRange.SetTarget(angle0);

        //инициализируем следующие звенья
        nextbehavior.Init();
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
        //drive.Update();
    }
}
