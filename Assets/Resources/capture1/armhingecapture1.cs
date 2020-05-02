using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class armhingecapture1 : MonoBehaviour
{
    public KinematicJoint kinematic = new KinematicJoint();

    public int CylinderFullHeight = 2;//it is cylinder, remember for cylinder, local y (height) is half of real
    public float diameter = 0.1f;
    public float width = 0.005f;

    public void Init()
    {
        //следующее звено
        Joint joint = GetComponent<Joint>();
        GameObject next = joint.connectedBody.gameObject;
        armcapture1 nextbehavior = joint.connectedBody.GetComponent<armcapture1>();

        //размещаем следующее звено
        next.transform.localScale = new Vector3(nextbehavior.diameter, nextbehavior.length, nextbehavior.diameter);
        next.transform.position = transform.rotation * (Vector3.down * (width * CylinderFullHeight / 2 + nextbehavior.length * nextbehavior.CylinderFullHeight / 2)) + transform.position;
        next.transform.rotation = transform.rotation;

        //ось и якорь шарнира, требуются для инициализации, но значение базе разницы т.к. FixedJoint
        joint.axis = Vector3.down;
        joint.anchor = new Vector3(0.0f, 0.0f, 0.0f);

        //кинематическая связь
        kinematic.AttachGameObject(gameObject);
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

    }

    public void KinematicUpdate()
    {
        kinematic.Update();
    }
}
