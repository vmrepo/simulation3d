using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotatingplatformmanipulator2 : MonoBehaviour
{
    [SerializeField]
    public float diameter = 0.5f;
    public float width = 0.01f;
    //remember for cylinder, width (y - scale) is half of real

    public void Init()
    {
        {
            //следующее звено
            FixedJoint fixedjoint = GetComponents<FixedJoint>()[0];
            GameObject next = fixedjoint.connectedBody.gameObject;
            holdermanipulator2 nextbehavior = fixedjoint.connectedBody.GetComponent<holdermanipulator2>();

            //размещаем следующее звено
            next.transform.localScale = new Vector3(nextbehavior.width, nextbehavior.height, nextbehavior.width);
            next.transform.position = new Vector3(transform.position.x, transform.position.y + (/*mul 2 for cylinder*/2 * width + nextbehavior.height) / 2, transform.position.z);

            //якорь шарнира
            fixedjoint.anchor = new Vector3(0.0f, 0.5f, 0.0f);

            //инициализируем следующее звено
            nextbehavior.Init();
        }

        {
            //ещё следующее звено
            FixedJoint fixedjoint = GetComponents<FixedJoint>()[1];
            GameObject next = fixedjoint.connectedBody.gameObject;
            holder1manipulator2 nextbehavior = fixedjoint.connectedBody.GetComponent<holder1manipulator2>();

            //размещаем следующее звено
            next.transform.localScale = new Vector3(nextbehavior.width, nextbehavior.height, nextbehavior.width);
            next.transform.position = new Vector3(transform.position.x, transform.position.y + (/*mul 2 for cylinder*/2 * width + nextbehavior.height) / 2, transform.position.z + nextbehavior.offset);

            //якорь шарнира
            fixedjoint.anchor = new Vector3(0.0f, 0.5f, 0.0f);

            //инициализируем следующее звено
            nextbehavior.Init();
        }

        {
            //ещё следующее звено
            FixedJoint fixedjoint = GetComponents<FixedJoint>()[2];
            GameObject next = fixedjoint.connectedBody.gameObject;
            holder2manipulator2 nextbehavior = fixedjoint.connectedBody.GetComponent<holder2manipulator2>();

            //размещаем следующее звено
            next.transform.localScale = new Vector3(nextbehavior.width, nextbehavior.height, nextbehavior.width);
            next.transform.position = new Vector3(transform.position.x, transform.position.y + (/*mul 2 for cylinder*/2 * width + nextbehavior.height) / 2, transform.position.z - nextbehavior.offset);

            //якорь шарнира
            fixedjoint.anchor = new Vector3(0.0f, 0.5f, 0.0f);

            //инициализируем следующее звено
            nextbehavior.Init();
        }
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
