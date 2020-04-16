using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class chassis : MonoBehaviour
{
    public DriveJoint drive = new DriveJoint();

    public float x = 0.0f;
    public float y = 0.0f;
    public float z = 0.0f;
    public float angle = 0.0f;
    public float height = 1.0f;
    public float width = 0.5f;
    public float angle0 = 0.0f;
    public float angle1 = 360.0f;
    public float kinematicangularvelocity = 100.0f;

    private float kinematicrestdeltaangle0 = 0.0f;
    private float kinematicrestdeltaangle1 = 0.0f;
    private float kinematicrestdeltaangle2 = 0.0f;

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
        drive.AngleRange.SetLimits(angle0, angle1);
        drive.AngleRange.SetTarget(angle0);

        //инициализируем следующие звенья
        nextbehavior.Init(position, angle);

        //поворачиваем вокруг вертикальной оси
        transform.RotateAround(position, Vector3.down, angle);
    }

    public void SetKinematicTargetDeltaAngles(float angle0delta, float angle1delta, float angle2delta)
    {
        kinematicrestdeltaangle0 = angle0delta;
        kinematicrestdeltaangle1 = angle1delta;
        kinematicrestdeltaangle2 = angle2delta;
    }

    public float GetKinematicRestDeltaAngle0()
    {
        return kinematicrestdeltaangle0;
    }

    public float GetKinematicRestDeltaAngle1()
    {
        return kinematicrestdeltaangle1;
    }

    public float GetKinematicRestDeltaAngle2()
    {
        return kinematicrestdeltaangle2;
    }

    public void Kinematic(float angle0delta, float angle1delta, float angle2delta)
    {
        GameObject next = GetComponent<HingeJoint>().connectedBody.gameObject;
        next.GetComponent<rotatingplatform>().Kinematic(angle0delta, angle1delta, angle2delta);
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void FixedUpdate()
    {
        if (GetComponent<HingeJoint>().connectedBody.GetComponent<Rigidbody>().isKinematic)
        {
            float angle0delta = Mathf.Sign(kinematicrestdeltaangle0) * Time.deltaTime * kinematicangularvelocity;
            float angle1delta = Mathf.Sign(kinematicrestdeltaangle1) * Time.deltaTime * kinematicangularvelocity;
            float angle2delta = Mathf.Sign(kinematicrestdeltaangle2) * Time.deltaTime * kinematicangularvelocity;

            angle0delta = Mathf.Sign(kinematicrestdeltaangle0) == Mathf.Sign(kinematicrestdeltaangle0 - angle0delta) ? angle0delta : kinematicrestdeltaangle0;
            angle1delta = Mathf.Sign(kinematicrestdeltaangle1) == Mathf.Sign(kinematicrestdeltaangle1 - angle1delta) ? angle1delta : kinematicrestdeltaangle1;
            angle2delta = Mathf.Sign(kinematicrestdeltaangle2) == Mathf.Sign(kinematicrestdeltaangle2 - angle2delta) ? angle2delta : kinematicrestdeltaangle2;

            kinematicrestdeltaangle0 -= angle0delta;
            kinematicrestdeltaangle1 -= angle1delta;
            kinematicrestdeltaangle2 -= angle2delta;

            Kinematic(angle0delta, angle1delta, angle2delta);
        }
        else
        {
            drive.Update();
        }
    }

}
