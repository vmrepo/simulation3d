using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class holder2manipulator2 : MonoBehaviour
{
    public DriveJoint drive = new DriveJoint();

    public float width = 0.03f;
    public float length = 0.14f;
    public float offset = 0.0895f;
    public float angle0 = 10.0f;
    public float angle1 = 60.0f;

    public void Init(Vector3 position, float angle)
    {
        //следующее звено
        HingeJoint hinge = GetComponent<HingeJoint>();
        GameObject next = hinge.connectedBody.gameObject;
        wheelhinge2manipulator2 nextbehavior = hinge.connectedBody.GetComponent<wheelhinge2manipulator2>();

        //размещаем следующее звено
        next.transform.localScale = new Vector3(nextbehavior.diameter, nextbehavior.width, nextbehavior.diameter);
        next.transform.position = new Vector3(transform.position.x, transform.position.y + (length + nextbehavior.diameter) / 2, transform.position.z);

        //якорь шарнира
        hinge.anchor = new Vector3(0.0f, 0.5f + nextbehavior.diameter / length / 2, 0.0f);

        //настраиваем привод шарнира
        drive.AttachGameObject(gameObject);
        drive.AngleRange.SetLimits(angle0, angle1);
        drive.AngleRange.SetTarget(angle0);

        //инициализируем следующие звенья
        nextbehavior.Init(position, angle);

        //поворачиваем вокруг вертикальной оси
        transform.RotateAround(position, Vector3.down, angle);
    }

    public void Kinematic(Vector3 position0, Vector3 axis0, float angle0delta, float angle1delta, float angle2delta)
    {
        transform.RotateAround(position0, axis0, angle0delta);
        GameObject next = GetComponent<HingeJoint>().connectedBody.gameObject;
        next.GetComponent<wheelhinge2manipulator2>().Kinematic(position0, axis0, angle0delta, angle1delta, angle2delta);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!GetComponent<Rigidbody>().isKinematic)
	    {
	        drive.Update();
	    }
    }
}
