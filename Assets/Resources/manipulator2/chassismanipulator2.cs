﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class chassismanipulator2 : MonoBehaviour
{
    //нужно соблюдать:
    //holder1.length == holder2.length
    //holder1.width == holder2.width
    //holder1.offset == holder2.offset
    //wheelhinge1.diameter == wheelhinge2.diameter == leverhinge.diameter == leverhinge1.diameter
    //wheelhinge1.width == wheelhinge2.width
    //wheel1.diameter == wheel2.diameter
    //wheel1.width == wheel2.width
    //lever.width == lever1.width
    //lever.length == lever1.length
    //armhinge.diameter == armhinge1.diameter
    //armhinge.width == armhinge1.width
    //holder1.angle0 = holder2.angle0 + lever.angle0
    //holder1.angle1 = holder2.angle1 + lever.angle1

    public DriveJoint drive = new DriveJoint();

    public float x = 0.0f;
    public float y = 0.0f;
    public float z = 0.0f;
    public float angle = 0.0f;
    public float height = 1.0f;
    public float width = 0.5f;
    public float angle0 = 0.0f;
    public float angle1 = 360.0f;

    public void Init()
    {
        Vector3 position = new Vector3(x, y, z);

        //ставим в начало координат на нижнюю грань и устанавливаем размеры
        //можно ставить в любое место, всё должно посчитаться
        transform.position = new Vector3(position.x, position.y + height / 2, position.z);
        transform.localScale = new Vector3(width, height, width);

        //следующее звено
        HingeJoint hinge = GetComponent<HingeJoint>();
        GameObject next = hinge.connectedBody.gameObject;
        rotatingplatformmanipulator2 nextbehavior = hinge.connectedBody.GetComponent<rotatingplatformmanipulator2>();

        //размещаем следующее звено
        next.transform.localScale = new Vector3(nextbehavior.diameter, nextbehavior.width, nextbehavior.diameter);
        next.transform.position = new Vector3(transform.position.x, transform.position.y + (height + /*mul 2 for cylinder*/2 * nextbehavior.width) / 2, transform.position.z);

        //якорь шарнира
        hinge.anchor = new Vector3(0.0f, 0.5f, 0.0f);

        //настраиваем привод шарнира
        drive.AttachGameObject(gameObject);
        drive.AngleRange.SetLimits(angle0, angle1);
        drive.AngleRange.SetTarget(angle0);

        //инициализируем следующие звенья
        nextbehavior.Init(position, angle);

        //поворачиваем вокруг вертикальной оси
        transform.RotateAround(position, Vector3.down, angle);
    }

    public void Kinematic(float angle0delta, float angle1delta, float angle2delta)
    {
        GameObject next = GetComponent<HingeJoint>().connectedBody.gameObject;
        next.GetComponent<rotatingplatformmanipulator2>().Kinematic(angle0delta, angle1delta, angle2delta);
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (!GetComponent<HingeJoint>().connectedBody.GetComponent<Rigidbody>().isKinematic)
        {
            drive.Update();
        }
    }
}
