﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class armmanipulator2 : MonoBehaviour
{
    public float width = 0.03f;
    public float length = 0.9f;

    public void Init(Vector3 position, float angle)
    {
        //следующее звено
        HingeJoint hinge = GetComponent<HingeJoint>();
        GameObject next = hinge.connectedBody.gameObject;
        armhinge1manipulator2 nextbehavior = hinge.connectedBody.GetComponent<armhinge1manipulator2>();

        //размещаем следующее звено
        next.transform.localScale = new Vector3(nextbehavior.diameter, nextbehavior.width, nextbehavior.diameter);
        next.transform.position = new Vector3(transform.position.x + (length + nextbehavior.diameter) / 2, transform.position.y, transform.position.z);

        //якорь шарнира
        hinge.anchor = new Vector3(0.0f, -0.5f - nextbehavior.diameter / length / 2, 0.0f);

        //инициализируем следующие звенья
        nextbehavior.Init(position, angle);

        //поворачиваем вокруг вертикальной оси
        transform.RotateAround(position, Vector3.down, angle);
    }

    public void Kinematic(Vector3 position0, Vector3 axis0, float angle0delta, Vector3 position1, Vector3 axis1, float angle1delta, Vector3 position2, Vector3 axis2, float angle2delta)
    {
        transform.RotateAround(position0, axis0, angle0delta);
        transform.RotateAround(position1, axis1, angle1delta);
        transform.RotateAround(position2, axis2, angle2delta);
        GameObject next = GetComponent<HingeJoint>().connectedBody.gameObject;
        next.GetComponent<armhinge1manipulator2>().Kinematic(position0, axis0, angle0delta, position1, axis1, angle1delta, position2, axis2, angle2delta);
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
