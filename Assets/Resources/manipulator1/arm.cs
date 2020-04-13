using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class arm : MonoBehaviour
{
    public float width = 0.03f;
    public float length = 0.6f;

    public void Init(Vector3 position, float angle)
    {
        //поворачиваем вокруг вертикальной оси
        transform.RotateAround(position, Vector3.down, angle);
    }

    public void Kinematic(Vector3 position0, Vector3 axis0, float angle0delta, Vector3 position1, Vector3 axis1, float angle1delta, Vector3 position2, Vector3 axis2, float angle2delta)
    {
        transform.RotateAround(position0, axis0, angle0delta);
        transform.RotateAround(position1, axis1, angle1delta);
        transform.RotateAround(position2, axis2, angle2delta);
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
