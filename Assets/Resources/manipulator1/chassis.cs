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
    public float angle0 = 0.0f;
    public float angle1 = 360.0f;

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
        drive.SetAngleLimits(angle0, angle1);
        drive.SetTargetAngle(angle0);

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
        drive.Update();
    }
}
