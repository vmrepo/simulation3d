using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class armhingemanipulator2 : MonoBehaviour
{
    public GameObject wheel1object = null;
    public GameObject armhinge1object = null;

    public float diameter = 0.08f;
    public float width = 0.0115f;
    //remember for cylinder, width (y - scale) is half of real

    public void Init(Vector3 position, float angle)
    {
        //следующее звено
        FixedJoint fixedjoint = GetComponent<FixedJoint>();
        GameObject next = fixedjoint.connectedBody.gameObject;
        armmanipulator2 nextbehavior = fixedjoint.connectedBody.GetComponent<armmanipulator2>();

        //потребуются звенья
        wheel1manipulator2 wheel1 = wheel1object.GetComponent<wheel1manipulator2>();
        armhinge1manipulator2 armhinge1 = armhinge1object.GetComponent<armhinge1manipulator2>();

        //размещаем следующее звено
        next.transform.localScale = new Vector3(nextbehavior.width, nextbehavior.length, nextbehavior.width);
        next.transform.position = new Vector3(transform.position.x - (nextbehavior.length / 2 - (wheel1.lever - armhinge1.diameter / 2)), transform.position.y, transform.position.z);

        //якорь шарнира
        fixedjoint.anchor = new Vector3(0.0f, 0.5f, 0.0f);

        //инициализируем следующие звенья
        nextbehavior.Init(position, angle);

        //поворачиваем вокруг вертикальной оси
        transform.RotateAround(position, Vector3.down, angle);
    }

    public void Kinematic(Vector3 position0, Vector3 axis0, float angle0delta, Vector3 position1, Vector3 axis1, float angle1delta, float angle2delta)
    {
        transform.RotateAround(position0, axis0, angle0delta);
        transform.RotateAround(position1, axis1, angle1delta);
        transform.RotateAround(transform.position, transform.rotation * Vector3.up, angle2delta);
        GameObject next = GetComponent<FixedJoint>().connectedBody.gameObject;
        next.GetComponent<armmanipulator2>().Kinematic(position0, axis0, angle0delta, position1, axis1, angle1delta, transform.position, transform.rotation * Vector3.up, angle2delta);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
