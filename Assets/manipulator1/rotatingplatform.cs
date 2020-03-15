using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotatingplatform : MonoBehaviour
{
    [SerializeField]
    public float diameter = 0.5f;
    public float height = 0.02f;

    public void Init()
    {
        //следующее звено
        FixedJoint fixedjoint = GetComponent<FixedJoint>();
        GameObject next = fixedjoint.connectedBody.gameObject;
        holder nextbehavior = fixedjoint.connectedBody.GetComponent<holder>();

        //размещаем следующее звено
        next.transform.localScale = new Vector3(nextbehavior.width, nextbehavior.height, nextbehavior.width);
        next.transform.position = new Vector3(transform.position.x, transform.position.y + (height + nextbehavior.height) / 2, transform.position.z);

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
