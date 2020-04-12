using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wheel1manipulator2 : MonoBehaviour
{
    public GameObject lever1object = null;
    public GameObject rotatingplatformobject = null;

    public float diameter = 0.32f;
    public float width = 0.0115f;
    public float lever = 0.12f;
    //remember for cylinder, width (y - scale) is half of real

    public void Init(Vector3 position, float angle)
    {
        //следующее звено
        FixedJoint fixedjoint = GetComponent<FixedJoint>();
        GameObject next = fixedjoint.connectedBody.gameObject;
        leverhinge1manipulator2 nextbehavior = fixedjoint.connectedBody.GetComponent<leverhinge1manipulator2>();

        //потребуются звенья
        lever1manipulator2 lever1 = lever1object.GetComponent<lever1manipulator2>();
        rotatingplatformmanipulator2 rotatingplatform = rotatingplatformobject.GetComponent<rotatingplatformmanipulator2>();

        //размещаем следующее звено
        float nexwidth = (transform.position.z - /*mul 2 for cylinder*/2 * width / 2) + lever1.width / 2 - rotatingplatform.transform.position.z;

        next.transform.localScale = new Vector3(nextbehavior.diameter, nexwidth / 2/*div 2 for cylinder*/, nextbehavior.diameter);
        next.transform.position = new Vector3(transform.position.x + lever, transform.position.y, transform.position.z - (/*mul 2 for cylinder*/2 * width + nexwidth) / 2);

        //якорь шарнира
        fixedjoint.anchor = new Vector3(0.0f, 0.5f, 0.0f);

        //инициализируем следующие звенья
        nextbehavior.Init(position, angle);

        //поворачиваем вокруг вертикальной оси
        transform.RotateAround(position, Vector3.down, angle);
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
