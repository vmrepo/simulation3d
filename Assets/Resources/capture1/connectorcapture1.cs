using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class connectorcapture1 : MonoBehaviour
{
    public DriveJoint drive = new DriveJoint();

    public int CylinderFullHeight = 2;//it is cylinder, remember for cylinder, local y (height) is half of real
    public GameObject holder = null;
    public Vector3 anchorposition = Vector3.zero;
    public Quaternion anchorrotation = Quaternion.identity;
    public float diameter = 0.1f;
    public float width = 0.005f;
    public float angle0 = 0.0f;
    public float angle1 = 360.0f;

    public void Init()
    {
        //соединяем, размещаем
        {
            Joint joint = holder.GetComponent<Joint>();
            joint.connectedBody = GetComponent<Rigidbody>();
            transform.localScale = new Vector3(diameter, width, diameter);
            transform.position = holder.transform.rotation * anchorposition + holder.transform.position;
            transform.rotation = holder.transform.rotation * anchorrotation;
            //ось и якорь
            joint.axis = Vector3.down;
            joint.anchor = new Vector3(0.0f, 0.0f, 0.0f);
        }

        {
            //следующее звено
            Joint joint = GetComponent<Joint>();
            GameObject next = joint.connectedBody.gameObject;
            armhingecapture1 nextbehavior = joint.connectedBody.GetComponent<armhingecapture1>();

            //размещаем следующее звено
            next.transform.localScale = new Vector3(nextbehavior.diameter, nextbehavior.width, nextbehavior.diameter);
            next.transform.position = transform.rotation * (Vector3.down * (width * CylinderFullHeight / 2 + nextbehavior.width * nextbehavior.CylinderFullHeight / 2)) + transform.position;
            next.transform.rotation = transform.rotation;

            //ось и якорь шарнира
            joint.axis = Vector3.down;
            joint.anchor = new Vector3(0.0f, -0.5f * CylinderFullHeight, 0.0f);

            //настраиваем привод шарнира
            drive.AttachGameObject(gameObject);
            drive.AngleRange.SetLimits(angle0, angle1);
            drive.AngleRange.SetTarget(angle0);

            //инициализируем следующие звенья
            nextbehavior.Init();
        }
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
}
