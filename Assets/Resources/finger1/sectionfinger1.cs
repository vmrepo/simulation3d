using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sectionfinger1 : MonoBehaviour
{
    public GameObject pivotObject = null;
    public CommonJoint joint = new CommonJoint();

    public void Init(finger1 device)
    {
        GetComponent<Rigidbody>().mass = device.config.SectionMass / 2;
        GetComponent<Rigidbody>().useGravity = device.config.UseGravity;

        transform.localScale = new Vector3(device.config.SectionThick, device.config.SectionHeight, device.config.SectionWidth);
        transform.position = pivotObject.transform.rotation * Quaternion.AngleAxis(90, Vector3.left) * (Vector3.down * device.config.SectionHeight / 2) + pivotObject.transform.position;
        transform.rotation = pivotObject.transform.rotation * Quaternion.AngleAxis(90, Vector3.left);

        joint.Config(pivotObject, gameObject, device.config.Kinematic, JointPhysics.Fixed);
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
