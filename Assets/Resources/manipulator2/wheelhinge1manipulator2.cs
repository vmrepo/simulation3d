using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wheelhinge1manipulator2 : MonoBehaviour
{
    [SerializeField]
    public float diameter = 0.08f;
    public float width = 0.023f;
    //remember for cylinder, width (y - scale) is half of real

    public void Init(float angle)
    {
        //следующее звено
        FixedJoint fixedjoint = GetComponent<FixedJoint>();
        GameObject next = fixedjoint.connectedBody.gameObject;
        wheel1manipulator2 nextbehavior = fixedjoint.connectedBody.GetComponent<wheel1manipulator2>();

        //размещаем следующее звено
        next.transform.localScale = new Vector3(nextbehavior.diameter, nextbehavior.width, nextbehavior.diameter);
        next.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z - (/*mul 2 for cylinder*/2 * width + /*mul 2 for cylinder*/2 * nextbehavior.width) / 2);

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
