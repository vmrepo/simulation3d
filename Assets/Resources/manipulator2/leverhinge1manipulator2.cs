using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class leverhinge1manipulator2 : MonoBehaviour
{
    public GameObject lever1object = null;

    public float diameter = 0.08f;
    //remember for cylinder, width (y - scale) is half of real

    public void Init(Vector3 position, float angle)
    {
        //подсоединён к звену, его нужно инициализировать
        lever1manipulator2 lever1 = lever1object.GetComponent<lever1manipulator2>();
        lever1.Init(position, angle);

        //поворачиваем вокруг вертикальной оси
        transform.RotateAround(position, Vector3.down, angle);
    }

    public void Kinematic(Vector3 position0, Vector3 axis0, float angle0delta, Vector3 position1, Vector3 axis1, float angle1delta, float angle2delta)
    {
        transform.RotateAround(position0, axis0, angle0delta);
        transform.RotateAround(position1, axis1, angle1delta + angle2delta);
        lever1object.GetComponent<lever1manipulator2>().Kinematic(position0, axis0, angle0delta, position1, axis1, angle1delta, angle2delta);
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
