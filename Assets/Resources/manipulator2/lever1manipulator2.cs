using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lever1manipulator2 : MonoBehaviour
{
    public float width = 0.03f;
    public float length = 0.6f;

    public void Init(Vector3 position, float angle)
    {
        {
            //соединённое звено
            HingeJoint hinge = GetComponents<HingeJoint>()[0];
            GameObject next = hinge.connectedBody.gameObject;
            leverhinge1manipulator2 nextbehavior = hinge.connectedBody.GetComponent<leverhinge1manipulator2>();

            //размещаем это звено, ориентируясь на соединённое
            transform.localScale = new Vector3(width, length, width);
            transform.position = new Vector3(nextbehavior.transform.position.x, nextbehavior.transform.position.y + (length + nextbehavior.diameter) / 2, nextbehavior.transform.position.z - /*mul 2 for cylinder*/2 * nextbehavior.transform.localScale.y / 2 + width / 2);

            //якорь шарнира
            hinge.anchor = new Vector3(0.0f, -0.5f - nextbehavior.diameter / length / 2, 0.0f);
        }

        //поворачиваем вокруг вертикальной оси
        //именно здесь т.к. эта цепочка звеньев смыкается с другой уже инициализированной цепочкой звеньев
        transform.RotateAround(position, Vector3.down, angle);

        {
            //ещё соединённое звено (из другой цепочки звеньев)
            HingeJoint hinge = GetComponents<HingeJoint>()[1];
            GameObject next = hinge.connectedBody.gameObject;
            armhinge1manipulator2 nextbehavior = hinge.connectedBody.GetComponent<armhinge1manipulator2>();

            //якорь шарнира
            hinge.anchor = new Vector3(0.0f, 0.5f + nextbehavior.diameter / length / 2, 0.0f);
        }
    }

    public void Kinematic(Vector3 position0, Vector3 axis0, float angle0delta, Vector3 position1, Vector3 axis1, float angle1delta, float angle2delta)
    {
        transform.RotateAround(position0, axis0, angle0delta);
        transform.RotateAround(position1, axis1, angle1delta);
        Vector3 vec0 = GetComponents<HingeJoint>()[0].connectedBody.transform.position;
        Vector3 vec1 = GetComponents<HingeJoint>()[1].connectedBody.transform.position;
        transform.position = vec0 + (vec1 - vec0) / 2;
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
