using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class armcapture1 : MonoBehaviour
{
    public DriveJoint drive = new DriveJoint();

    public float diameter = 0.03f;
    public float length = 0.2f;
    //remember for cylinder, length (y - scale) is half of real
    public float angle0 = 30.0f;
    public float angle1 = 330.0f;

    public void Init()
    {
        //...
    }

    public void Kinematic()
    {
        //...
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
        if (GetComponent<Rigidbody>().isKinematic)
        {
            //...
        }
        else
        {
            drive.Update();
        }
    }
}
