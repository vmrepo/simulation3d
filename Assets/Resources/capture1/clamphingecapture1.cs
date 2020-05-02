using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class clamphingecapture1 : MonoBehaviour
{
    public int CylinderFullHeight = 2;//it is cylinder, remember for cylinder, local y (height) is half of real
    public float diameter = 0.1f;
    public float width = 0.015f;

    public void Init()
    {
        //следующее звено
        Joint joint = GetComponent<Joint>();
        GameObject next = joint.connectedBody.gameObject;
        clampcapture1 nextbehavior = joint.connectedBody.GetComponent<clampcapture1>();

        //размещаем следующее звено
        next.transform.localScale = new Vector3(nextbehavior.diameter, nextbehavior.width, nextbehavior.diameter);
        next.transform.position = Quaternion.AngleAxis(90, Vector3.back) * transform.rotation * (Vector3.down * (diameter / 2 + nextbehavior.width * nextbehavior.CylinderFullHeight / 2)) + transform.position;
        next.transform.rotation = Quaternion.AngleAxis(90, Vector3.back) * transform.rotation;

        //ось и якорь шарнира, требуются для инициализации, но значение базе разницы т.к. FixedJoint
        joint.axis = Vector3.down;
        joint.anchor = new Vector3(0.0f, 0.0f, 0.0f);

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
}
