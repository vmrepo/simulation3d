using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class armhingecapture1 : MonoBehaviour
{
    public int CylinderFullHeight = 2;//it is cylinder, remember for cylinder, local y (height) is half of real
    public float diameter = 0.1f;
    public float width = 0.005f;

    public void Init()
    {
        //следующее звено
        FixedJoint fixedjoint = GetComponent<FixedJoint>();
        GameObject next = fixedjoint.connectedBody.gameObject;
        armcapture1 nextbehavior = fixedjoint.connectedBody.GetComponent<armcapture1>();

        //размещаем следующее звено
        next.transform.localScale = new Vector3(nextbehavior.diameter, nextbehavior.length, nextbehavior.diameter);
        next.transform.position = transform.rotation * (Vector3.down * (width * CylinderFullHeight / 2 + nextbehavior.length * nextbehavior.CylinderFullHeight / 2)) + transform.position;
        next.transform.rotation = transform.rotation;

        //ось и якорь шарнира
        fixedjoint.axis = Vector3.down;
        fixedjoint.anchor = new Vector3(0.0f, 0.0f, 0.0f);

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
}
