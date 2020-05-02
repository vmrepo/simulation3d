using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class armhingecapture1 : MonoBehaviour
{
    public int CylinderFullHeight = 2;//it is cylinder, remember for cylinder, local y (height) is half of real
    public KinematicJoint kinematic = new KinematicJoint();

    public void Init(capture1 device)
    {
        GetComponent<Rigidbody>().mass = device.config.ConnectorMass / 2;
        GetComponent<Rigidbody>().useGravity = device.config.UseGravity;
        GetComponent<Rigidbody>().isKinematic = device.config.Kinematic;

        //следующее звено
        Joint joint = GetComponent<Joint>();
        GameObject next = joint.connectedBody.gameObject;
        armcapture1 nextbehavior = joint.connectedBody.GetComponent<armcapture1>();

        //размещаем следующее звено
        next.transform.localScale = new Vector3(device.config.ArmDiameter, device.config.ArmLength / nextbehavior.CylinderFullHeight, device.config.ArmDiameter);
        next.transform.position = transform.rotation * (Vector3.down * (device.config.ConnectorWidth / 4 + device.config.ArmLength / 2)) + transform.position;
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
