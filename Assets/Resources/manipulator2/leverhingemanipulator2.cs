using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class leverhingemanipulator2 : MonoBehaviour
{
    public float diameter = 0.08f;
    //remember for cylinder, width (y - scale) is half of real

    public void Init(float angle)
    {
        //следующее звено
        FixedJoint fixedjoint = GetComponent<FixedJoint>();
        GameObject next = fixedjoint.connectedBody.gameObject;
        levermanipulator2 nextbehavior = fixedjoint.connectedBody.GetComponent<levermanipulator2>();

        //размещаем следующее звено
        next.transform.localScale = new Vector3(nextbehavior.width, nextbehavior.length, nextbehavior.width);
        next.transform.position = new Vector3(transform.position.x, transform.position.y + (diameter + nextbehavior.length) / 2, transform.position.z + /*mul 2 for cylinder*/2 * transform.localScale.y / 2 - nextbehavior.width / 2);

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
