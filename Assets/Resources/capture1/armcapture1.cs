using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class armcapture1 : MonoBehaviour
{
    public const int CylinderFullHeight = 2;//it is cylinder, remember for cylinder, local y (height) is half of real
    public GameObject pivotObject = null;
    public CommonJoint joint = new CommonJoint();

    public void Init(capture1 device)
    {
        GetComponent<Rigidbody>().mass = device.config.ArmMass;
        GetComponent<Rigidbody>().useGravity = device.config.UseGravity;

        transform.localScale = new Vector3(device.config.ArmDiameter, device.config.ArmLength / CylinderFullHeight, device.config.ArmDiameter);
        transform.position = pivotObject.transform.rotation * (Vector3.down * (device.config.ConnectorWidth / 4 + device.config.ArmLength / 2)) + pivotObject.transform.position;
        transform.rotation = pivotObject.transform.rotation;

        joint.Config(pivotObject, gameObject, device.config.ArmKinematic, JointPhysics.Fixed, Vector3.up, Vector3.zero);
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
