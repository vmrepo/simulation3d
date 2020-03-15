using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lever : MonoBehaviour
{
    public DriveJoint drive = new DriveJoint();

    [SerializeField]
    public float width = 0.03f;
    public float height = 0.6f;

    public void Init()
    {
        //следующее звено
        HingeJoint hinge = GetComponent<HingeJoint>();
        GameObject next = hinge.connectedBody.gameObject;
        armhinge nextbehavior = hinge.connectedBody.GetComponent<armhinge>();

        //размещаем следующее звено
        next.transform.localScale = new Vector3(nextbehavior.diameter, nextbehavior.width, nextbehavior.diameter);
        next.transform.position = new Vector3(transform.position.x, transform.position.y + (height + nextbehavior.diameter) / 2, transform.position.z);

        //якорь шарнира
        hinge.anchor = new Vector3(0.0f, 0.5f + nextbehavior.diameter / height / 2, 0.0f);

        //настраиваем привод шарнира
        drive.AttachGameObject(gameObject);
        drive.SetAngleLimits(60, 120);
        drive.SetTargetAngle(60);

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
        drive.Update();
    }
}
