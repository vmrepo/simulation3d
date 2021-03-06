﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class clampconnectorcapture1 : MonoBehaviour
{
    public const int CylinderFullHeight = 2;//it is cylinder, remember for cylinder, local y (height) is half of real
    public GameObject pivotObject = null;
    public CommonJoint joint = new CommonJoint();

    public void Init(capture1 device)
    {
        GetComponent<Rigidbody>().mass = device.config.ClampMass / 2;
        GetComponent<Rigidbody>().useGravity = device.config.UseGravity;

        transform.localScale = new Vector3(device.config.ClampDiameter, device.config.ClampWidth / 2 / CylinderFullHeight, device.config.ClampDiameter);
        transform.position = pivotObject.transform.rotation * Quaternion.AngleAxis(90, Vector3.back) * (Vector3.down * (device.config.ClamphingeDiameter / 2 + device.config.ClampWidth / 2 / 2)) + pivotObject.transform.position;
        transform.rotation = pivotObject.transform.rotation * Quaternion.AngleAxis(90, Vector3.back);

        joint.Config(pivotObject, gameObject, device.config.ClampConnectorKinematic, JointPhysics.Fixed);
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
