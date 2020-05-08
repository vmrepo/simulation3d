using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class connectorcapture1 : MonoBehaviour
{
    public const int CylinderFullHeight = 2;//it is cylinder, remember for cylinder, local y (height) is half of real
    public CommonJoint joint = new CommonJoint();

    public void Init(capture1 device)
    {
        GetComponent<Rigidbody>().mass = device.config.ConnectorMass / 2;
        GetComponent<Rigidbody>().useGravity = device.config.UseGravity;

        transform.localScale = new Vector3(device.config.ConnectorDiameter, device.config.ConnectorWidth / 2 / CylinderFullHeight, device.config.ConnectorDiameter);
        transform.position = device.pivot.Object.transform.rotation * (device.pivot.position + Vector3.down * device.config.ConnectorWidth / 4) + device.pivot.Object.transform.position;
        transform.rotation = device.pivot.Object.transform.rotation * device.pivot.rotation;

        joint.Config(device.pivot.Object, gameObject, device.config.ArmKinematic, JointPhysics.Fixed, Vector3.up, Vector3.zero);
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
        joint.KinematicUpdate();
    }
}
