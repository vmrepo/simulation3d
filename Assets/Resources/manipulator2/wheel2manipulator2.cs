﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wheel2manipulator2 : MonoBehaviour
{
    public GameObject leverobject = null;
    public GameObject rotatingplatformobject = null;

    [SerializeField]
    public float diameter = 0.32f;
    public float width = 0.0115f;
    //remember for cylinder, width (y - scale) is half of real

    public void Init(float angle)
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
        nextbehavior.Init(angle);

        //поворачиваем вокруг вертикальной оси
        transform.RotateAround(Vector3.zero, Vector3.down, angle);
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