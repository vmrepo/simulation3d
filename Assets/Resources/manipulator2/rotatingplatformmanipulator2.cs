using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotatingplatformmanipulator2 : MonoBehaviour
{
    public float diameter = 0.5f;
    public float width = 0.01f;
    //remember for cylinder, width (y - scale) is half of real

    public void Init(float angle)
    {
        //последовательность инициалиации цепочек звеньев имеет значение

        {
            //следующее звено
            FixedJoint fixedjoint = GetComponents<FixedJoint>()[1];
            GameObject next = fixedjoint.connectedBody.gameObject;
            holder2manipulator2 nextbehavior = fixedjoint.connectedBody.GetComponent<holder2manipulator2>();

            //размещаем следующее звено
            next.transform.localScale = new Vector3(nextbehavior.width, nextbehavior.length, nextbehavior.width);
            next.transform.position = new Vector3(transform.position.x, transform.position.y + (/*mul 2 for cylinder*/2 * width + nextbehavior.length) / 2, transform.position.z - nextbehavior.offset);

            //якорь шарнира
            fixedjoint.anchor = new Vector3(0.0f, 0.5f, 0.0f);

            //инициализируем следующие звенья
            nextbehavior.Init(angle);
        }

        {
            //ещё следующее звено
            FixedJoint fixedjoint = GetComponents<FixedJoint>()[0];
            GameObject next = fixedjoint.connectedBody.gameObject;
            holder1manipulator2 nextbehavior = fixedjoint.connectedBody.GetComponent<holder1manipulator2>();

            //размещаем следующее звено
            next.transform.localScale = new Vector3(nextbehavior.width, nextbehavior.length, nextbehavior.width);
            next.transform.position = new Vector3(transform.position.x, transform.position.y + (/*mul 2 for cylinder*/2 * width + nextbehavior.length) / 2, transform.position.z + nextbehavior.offset);

            //якорь шарнира
            fixedjoint.anchor = new Vector3(0.0f, 0.5f, 0.0f);

            //инициализируем следующие звенья
            nextbehavior.Init(angle);
        }

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
