using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class holder1manipulator2 : MonoBehaviour
{
    public DriveJoint drive = new DriveJoint();

    [SerializeField]
    public float width = 0.03f;
    public float height = 0.14f;
    public float offset = 0.0895f;

    public void Init()
    {
        //следующее звено
        HingeJoint hinge = GetComponent<HingeJoint>();
        GameObject next = hinge.connectedBody.gameObject;
        wheelhinge1manipulator2 nextbehavior = hinge.connectedBody.GetComponent<wheelhinge1manipulator2>();

        //размещаем следующее звено
        next.transform.localScale = new Vector3(nextbehavior.diameter, nextbehavior.width, nextbehavior.diameter);
        next.transform.position = new Vector3(transform.position.x, transform.position.y + (height + nextbehavior.diameter) / 2, transform.position.z);

        //якорь шарнира
        hinge.anchor = new Vector3(0.0f, 0.5f + nextbehavior.diameter / height / 2, 0.0f);

        //настраиваем привод шарнира
        drive.AttachGameObject(gameObject);
        drive.SetAngleLimits(40, 50);
        drive.SetTargetAngle(40);

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
        //drive.Update();
    }
}
