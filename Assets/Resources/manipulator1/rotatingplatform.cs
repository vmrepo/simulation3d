﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotatingplatform : MonoBehaviour
{
    public float diameter = 0.5f;
    public float width = 0.01f;
    //remember for cylinder, width (y - scale) is half of real

    public void Init(Vector3 position, float angle)
    {
        //следующее звено
        FixedJoint fixedjoint = GetComponent<FixedJoint>();
        GameObject next = fixedjoint.connectedBody.gameObject;
        holder nextbehavior = fixedjoint.connectedBody.GetComponent<holder>();

        //размещаем следующее звено
        next.transform.localScale = new Vector3(nextbehavior.width, nextbehavior.length, nextbehavior.width);
        next.transform.position = new Vector3(transform.position.x, transform.position.y + (/*mul 2 for cylinder*/2 * width + nextbehavior.length) / 2, transform.position.z);

        //якорь шарнира
        fixedjoint.anchor = new Vector3(0.0f, 0.5f, 0.0f);

        //инициализируем следующие звенья
        nextbehavior.Init(position, angle);

        //поворот вокруг вертикальной оси
        transform.RotateAround(position, Vector3.down, angle);
    }

    public void Kinematic(float angle0delta, float angle1delta, float angle2delta)
    {
        transform.RotateAround(transform.position, Vector3.up, angle0delta);
        GameObject next = GetComponent<FixedJoint>().connectedBody.gameObject;
        next.GetComponent<holder>().Kinematic(transform.position, Vector3.up, angle0delta, angle1delta, angle2delta);
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
