using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class connectorcapture1 : MonoBehaviour
{
    public DriveJoint drive = new DriveJoint();

    public float diameter = 0.1f;
    public float width = 0.005f;
    //remember for cylinder, width (y - scale) is half of real
    public float angle0 = 0.0f;
    public float angle1 = 360.0f;

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
