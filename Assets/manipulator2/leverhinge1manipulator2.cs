using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class leverhinge1manipulator2 : MonoBehaviour
{
    [SerializeField]
    public float diameter = 0.08f;
    //remember for cylinder, width (y - scale) is half of real

    public void Init(float angle)
    {
        //подсоединён к звену, его нужно инициализировать
        lever1manipulator2 lever1 = GameObject.Find("lever1").GetComponent<lever1manipulator2>();
        lever1.Init(angle);

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
        
    }
}
