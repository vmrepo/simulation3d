using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class connectorfinger1 : MonoBehaviour
{
    public CommonJoint joint = new CommonJoint();

    public void Init(finger1 device)
    {
        GetComponent<Rigidbody>().mass = device.config.ConnectorMass;
        GetComponent<Rigidbody>().useGravity = device.config.UseGravity;

        transform.localScale = new Vector3(device.config.SectionThick, device.config.ConnectorHeight, device.config.SectionWidth);
        Vector3 ofs = device.pivot.rotation * Vector3.down * device.config.ConnectorHeight / 2;
        transform.position = device.pivot.Object.transform.rotation * (device.pivot.position + ofs) + device.pivot.Object.transform.position;
        transform.rotation = device.pivot.Object.transform.rotation * device.pivot.rotation;
        transform.rotation = device.pivot.Object.transform.rotation * device.pivot.rotation;

        joint.Config(device.pivot.Object, gameObject, device.config.Kinematic, JointPhysics.Fixed);
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
