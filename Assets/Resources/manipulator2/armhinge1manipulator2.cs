using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class armhinge1manipulator2 : MonoBehaviour
{
    public float diameter = 0.08f;
    public float width = 0.0115f;
    //remember for cylinder, width (y - scale) is half of real

    public void Init(Vector3 position, float angle)
    {
        //поворачиваем вокруг вертикальной оси
        transform.RotateAround(position, Vector3.down, angle);
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
