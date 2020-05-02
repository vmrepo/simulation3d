using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class clamphingecapture1 : MonoBehaviour
{
    public int CylinderFullHeight = 2;//it is cylinder, remember for cylinder, local y (height) is half of real
    public KinematicJoint kinematic = new KinematicJoint();

    public void Init(capture1 device)
    {
        GetComponent<Rigidbody>().mass = device.config.ClamphingeMass;
        GetComponent<Rigidbody>().useGravity = device.config.UseGravity;
        GetComponent<Rigidbody>().isKinematic = device.config.Kinematic;

        //следующее звено
        Joint joint = GetComponent<Joint>();
        GameObject next = joint.connectedBody.gameObject;
        clampcapture1 nextbehavior = joint.connectedBody.GetComponent<clampcapture1>();

        //размещаем следующее звено
        next.transform.localScale = new Vector3(device.config.ClampDiameter, device.config.ClampWidth / nextbehavior.CylinderFullHeight, device.config.ClampDiameter);
        next.transform.position = Quaternion.AngleAxis(90, Vector3.back) * transform.rotation * (Vector3.down * (device.config.ClamphingeDiameter / 2 + device.config.ClampWidth / 2)) + transform.position;
        next.transform.rotation = Quaternion.AngleAxis(90, Vector3.back) * transform.rotation;

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
