﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wheel2manipulator2 : MonoBehaviour
{
    public GameObject leverobject = null;
    public GameObject rotatingplatformobject = null;

    public float diameter = 0.32f;
    public float width = 0.0115f;
    //remember for cylinder, width (y - scale) is half of real

    public void Init(Vector3 position, float angle)
    {
        //следующее звено
        FixedJoint fixedjoint = GetComponent<FixedJoint>();
        GameObject next = fixedjoint.connectedBody.gameObject;
        leverhingemanipulator2 nextbehavior = fixedjoint.connectedBody.GetComponent<leverhingemanipulator2>();

        //потребуются звенья
        levermanipulator2 lever = leverobject.GetComponent<levermanipulator2>();
        rotatingplatformmanipulator2 rotatingplatform = rotatingplatformobject.GetComponent<rotatingplatformmanipulator2>();

        //размещаем следующее звено
        float nexwidth = rotatingplatform.transform.position.z + lever.width / 2 - (transform.position.z + /*mul 2 for cylinder*/2 * width / 2);
        next.transform.localScale = new Vector3(nextbehavior.diameter, nexwidth / 2/*div 2 for cylinder*/, nextbehavior.diameter);
        next.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + (/*mul 2 for cylinder*/2 * width + nexwidth) / 2);

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
        GameObject next = GetComponent<FixedJoint>().connectedBody.gameObject;
        next.GetComponent<leverhingemanipulator2>().Kinematic(position0, axis0, angle0delta, position1, axis1, angle1delta, angle2delta);
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
