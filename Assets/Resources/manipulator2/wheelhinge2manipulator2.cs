using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wheelhinge2manipulator2 : MonoBehaviour
{
    public float diameter = 0.08f;
    public float width = 0.023f;
    //remember for cylinder, width (y - scale) is half of real

    public void Init(Vector3 position, float angle)
    {
        //следующее звено
        FixedJoint fixedjoint = GetComponent<FixedJoint>();
        GameObject next = fixedjoint.connectedBody.gameObject;
        wheel2manipulator2 nextbehavior = fixedjoint.connectedBody.GetComponent<wheel2manipulator2>();

        //размещаем следующее звено
        next.transform.localScale = new Vector3(nextbehavior.diameter, nextbehavior.width, nextbehavior.diameter);
        next.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + (/*mul 2 for cylinder*/2 * width + /*mul 2 for cylinder*/2 * nextbehavior.width) / 2);

        //якорь шарнира
        fixedjoint.anchor = new Vector3(0.0f, 0.5f, 0.0f);

        //инициализируем следующие звенья
        nextbehavior.Init(position, angle);

        //поворачиваем вокруг вертикальной оси
        transform.RotateAround(position, Vector3.down, angle);
    }

    public void Kinematic(Vector3 position0, Vector3 axis0, float angle0delta, float angle1delta, float angle2delta)
    {
        transform.RotateAround(position0, axis0, angle0delta);
        transform.RotateAround(transform.position, transform.rotation * Vector3.up, angle1delta);
        GameObject next = GetComponent<FixedJoint>().connectedBody.gameObject;
        next.GetComponent<wheel2manipulator2>().Kinematic(position0, axis0, angle0delta, transform.position, transform.rotation * Vector3.up, angle1delta, angle2delta);
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
