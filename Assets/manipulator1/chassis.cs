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
    public float height = 1.0f;
    public float width = 0.5f;

    // Start is called before the first frame update
    void Start()
    {
        Vector3 position = new Vector3(x, y, z);

        //ставим в начало координат на нижнюю грань и устанвливаем размеры
        //можно ставить в любое место, всё должно автоматом посчитаться
        transform.position = new Vector3(position.x, position.y + height / 2, position.z);
        transform.localScale = new Vector3(width, height, width);

        //размещаем roatatingplatform
        float h = GetComponent<HingeJoint>().connectedBody.GetComponent<rotatingplatform>().height;
        GetComponent<HingeJoint>().connectedBody.gameObject.transform.position = new Vector3(transform.position.x, transform.position.y + (height + h) / 2, transform.position.z);
        GetComponent<HingeJoint>().anchor = new Vector3(0.0f, 0.5f, 0.0f);

        //настраиваем привод шарнира
        drive.AttachGameObject(gameObject);
        drive.SetAngleLimits(0, 360);
        drive.SetTargetAngle(0);
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
