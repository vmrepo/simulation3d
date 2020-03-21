using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class leverhinge : MonoBehaviour
{
    [SerializeField]
    public float diameter = 0.08f;
    public float width = 0.0115f;
    //remember for cylinder, width (y - scale) is half of real

    public void Init()
    {
        //следующее звено
        FixedJoint fixedjoint = GetComponent<FixedJoint>();
        GameObject next = fixedjoint.connectedBody.gameObject;
        lever nextbehavior = fixedjoint.connectedBody.GetComponent<lever>();

        //размещаем следующее звено
        next.transform.localScale = new Vector3(nextbehavior.width, nextbehavior.length, nextbehavior.width);
        next.transform.position = new Vector3(transform.position.x, transform.position.y + (diameter + nextbehavior.length) / 2, transform.position.z);

        //якорь шарнира
        fixedjoint.anchor = new Vector3(0.0f, 0.5f, 0.0f);

        //инициализируем следующее звено
        nextbehavior.Init();
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
