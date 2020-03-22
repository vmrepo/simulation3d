using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class armhingemanipulator2 : MonoBehaviour
{
    [SerializeField]
    public float diameter = 0.08f;
    public float width = 0.0115f;
    //remember for cylinder, width (y - scale) is half of real

    public void Init(float angle)
    {
        //следующее звено
        FixedJoint fixedjoint = GetComponent<FixedJoint>();
        GameObject next = fixedjoint.connectedBody.gameObject;
        armmanipulator2 nextbehavior = fixedjoint.connectedBody.GetComponent<armmanipulator2>();

        //потребуются звенья
        wheel1manipulator2 wheel1 = GameObject.Find("wheel1").GetComponent<wheel1manipulator2>();
        armhinge1manipulator2 armhinge1 = GameObject.Find("armhinge1").GetComponent<armhinge1manipulator2>();

        //размещаем следующее звено
        next.transform.localScale = new Vector3(nextbehavior.width, nextbehavior.length, nextbehavior.width);
        next.transform.position = new Vector3(transform.position.x - (nextbehavior.length / 2 - (wheel1.lever - armhinge1.diameter / 2)), transform.position.y, transform.position.z);

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
