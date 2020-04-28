using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class connectorcapture1 : MonoBehaviour
{
    public DriveJoint drive = new DriveJoint();

    public FixedJoint fixedjoint = null;
    public Vector3 initpoint = Vector3.zero;
    public Quaternion initrotation = Quaternion.identity;
    public float diameter = 0.1f;
    public float width = 0.005f;
    //remember for cylinder, width (y - scale) is half of real
    public float angle0 = 0.0f;
    public float angle1 = 360.0f;

    public void Init()
    {
        //размещаем, устанавливаем размеры и соединяем звено
        transform.position = initrotation * (Vector3.down * width / 2 * 2/*mul 2 for cylinder*/) + initpoint;
        transform.rotation = initrotation;
        transform.localScale = new Vector3(diameter, width, diameter);
        fixedjoint.connectedBody = GetComponent<Rigidbody>();

        //следующее звено
        HingeJoint hinge = GetComponent<HingeJoint>();
        GameObject next = hinge.connectedBody.gameObject;
        armhingecapture1 nextbehavior = hinge.connectedBody.GetComponent<armhingecapture1>();

        //размещаем следующее звено
        next.transform.localScale = new Vector3(nextbehavior.diameter, nextbehavior.width, nextbehavior.diameter);
        next.transform.position = transform.rotation * (Vector3.down * (width / 2 * 2/*mul 2 for cylinder*/ + nextbehavior.width / 2 * 2/*mul 2 for cylinder*/)) + transform.position;
        next.transform.rotation = transform.rotation;

        //ось и якорь шарнира
        hinge.axis = Vector3.down;
        hinge.anchor = new Vector3(0.0f, 0.5f, 0.0f);

        //настраиваем привод шарнира
        drive.AttachGameObject(gameObject);
        drive.AngleRange.SetLimits(angle0, angle1);
        drive.AngleRange.SetTarget(angle0);

        //инициализируем следующие звенья
        nextbehavior.Init();
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
            //drive.Update();
        }
    }
}
