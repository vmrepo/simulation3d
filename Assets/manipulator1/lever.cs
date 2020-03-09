﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lever : MonoBehaviour
{
    public DriveJoint drive = new DriveJoint();

    [SerializeField]
    public float width = 0.03f;
    public float height = 0.6f;

    // Start is called before the first frame update
    void Start()
    {
        //устанавливаем размер
        transform.localScale = new Vector3(width, height, width);

        //размещаем armhinge
        float h = GetComponent<HingeJoint>().connectedBody.GetComponent<armhinge>().diameter;
        GetComponent<HingeJoint>().connectedBody.gameObject.transform.position = new Vector3(transform.position.x, transform.position.y + (height + h) / 2, transform.position.z);
        GetComponent<HingeJoint>().anchor = new Vector3(0.0f, 0.5f + h / height / 2, 0.0f);

        //настраиваем привод шарнира
        drive.AttachGameObject(gameObject);
        drive.SetAngleLimits(60, 120);
        drive.SetTargetAngle(10);
    }

    // Update is called once per frame
    void Update()
    {
        drive.Update();
    }
}
