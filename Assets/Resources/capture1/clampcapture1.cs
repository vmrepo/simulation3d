using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class clampcapture1 : MonoBehaviour
{
    public int CylinderFullHeight = 2;//it is cylinder, remember for cylinder, local y (height) is half of real

    public void Init(capture1 device)
    {
        GetComponent<Rigidbody>().mass = device.config.ClampMass;
        GetComponent<Rigidbody>().useGravity = device.config.UseGravity;
        GetComponent<Rigidbody>().isKinematic = device.config.Kinematic;
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

    }
}
