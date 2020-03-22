using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class chassis : MonoBehaviour
{
    public DriveJoint drive = new DriveJoint();

    [SerializeField]
    public float x = 0.0f;
    public float y = 0.0f;
    public float z = 0.0f;
    public float angle = 0.0f;
    public float height = 1.0f;
    public float width = 0.5f;

    public void Init()
    {
        Vector3 position = new Vector3(x, y, z);

        //ставим в начало координат на нижнюю грань и устанавливаем размеры
        //можно ставить в любое место, всё должно посчитаться
        transform.position = new Vector3(position.x, position.y + height / 2, position.z);
        transform.localScale = new Vector3(width, height, width);

        //следующее звено
        HingeJoint hinge = GetComponent<HingeJoint>();
        GameObject next = hinge.connectedBody.gameObject;
        rotatingplatform nextbehavior = hinge.connectedBody.GetComponent<rotatingplatform>();

        //размещаем следующее звено
        next.transform.localScale = new Vector3(nextbehavior.diameter, nextbehavior.width, nextbehavior.diameter);
        next.transform.position = new Vector3(transform.position.x, transform.position.y + (height + /*mul 2 for cylinder*/2 * nextbehavior.width) / 2, transform.position.z);

        //якорь шарнира
        hinge.anchor = new Vector3(0.0f, 0.5f, 0.0f);

        //настраиваем привод шарнира
        drive.AttachGameObject(gameObject);
        drive.SetAngleLimits(0, 360);
        drive.SetTargetAngle(0);

        //инициализируем следующие звенья
        nextbehavior.Init(angle);

        //поворачиваем вокруг вертикальной оси
        transform.RotateAround(Vector3.zero, Vector3.down, angle);
    }

    // Start is called before the first frame update
    void Start()
    {
        Init();
    }

    // Update is called once per frame
    void Update()
    {
        float delta = 30.0f;

        if (Input.GetKeyDown(KeyCode.Q))
        {
            DriveJoint drive = GameObject.Find("chassis").GetComponent<chassis>().drive;
            float a = drive.GetTargetAngle();
            drive.SetTargetAngle(a - delta);
        }

        if (Input.GetKeyDown(KeyCode.W))
        {
            DriveJoint drive = GameObject.Find("chassis").GetComponent<chassis>().drive;
            float a = drive.GetTargetAngle();
            drive.SetTargetAngle(a + delta);
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            DriveJoint drive = GameObject.Find("holder").GetComponent<holder>().drive;
            float a = drive.GetTargetAngle();
            drive.SetTargetAngle(a - delta);
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            DriveJoint drive = GameObject.Find("holder").GetComponent<holder>().drive;
            float a = drive.GetTargetAngle();
            drive.SetTargetAngle(a + delta);
        }

        if (Input.GetKeyDown(KeyCode.Z))
        {
            DriveJoint drive = GameObject.Find("lever").GetComponent<lever>().drive;
            float a = drive.GetTargetAngle();
            drive.SetTargetAngle(a - delta);
        }

        if (Input.GetKeyDown(KeyCode.X))
        {
            DriveJoint drive = GameObject.Find("lever").GetComponent<lever>().drive;
            float a = drive.GetTargetAngle();
            drive.SetTargetAngle(a + delta);
        }

        drive.Update();
    }
}
