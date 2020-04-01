using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camera : MonoBehaviour
{

    [SerializeField]
    public Transform target; //невидимая цель для камеры

    public float zoomSpeed = 5.0f; //скорость приближения камеры
    private Vector3 _offset; //смещение камеры относительно объекта
    public float mouse_sens = 1f;
    public Camera cam_holder;
    float x_axis, y_axis, z_axis, _rotY, _rotX; //мышь по x, y, зум, координаты для обзора

    void Start()
    {
        _rotY = transform.eulerAngles.y;
        _rotX = transform.eulerAngles.x;
        _offset = target.position - transform.position; //получает начальное смещение
    }

    void LookAtTarget()
    {
        Quaternion rotation = Quaternion.Euler(_rotY, _rotX, 0); //задает вращение камеры 
        transform.position = target.position - (rotation * _offset);
        transform.LookAt(target);
    }

    void LateUpdate()
    {
        float input = Input.GetAxis("Mouse ScrollWheel");
        if (input != 0) //если крутится колесико мыши
        {
            cam_holder.fieldOfView *= 1 - input;// *zoomSpeed; //зум
        }

        if (Input.GetMouseButton(0)) //левая кнопка мыши
        { //вращение вокруг объекта
            _rotX -= Input.GetAxis("Mouse X") * mouse_sens; //поворот камеры вокруг объекта и сохранение координат
            _rotY -= Input.GetAxis("Mouse Y") * mouse_sens;

            LookAtTarget();
        }

        if (Input.GetMouseButton(1)) //правая кнопка
        {
            //обзор вокруг объекта
            //смещение камеры по осям X и Y
            //x_axis = Input.GetAxis("Mouse X") * mouse_sens;
            //y_axis = Input.GetAxis("Mouse Y") * mouse_sens;

            //target.position = new Vector3(target.position.x + x_axis, target.position.y + y_axis, target.position.z);

            //LookAtTarget();
        }
        if (Input.GetMouseButton(2)) //колесико
        {
            //обзор вокруг камеры
            x_axis = Input.GetAxis("Mouse X") * mouse_sens;
            y_axis = Input.GetAxis("Mouse Y") * mouse_sens;
            //z_axis = Input.GetAxis("Mouse ScrollWheel") * wheel_sens;

            cam_holder.transform.Rotate(Vector3.up, x_axis, Space.World);
            cam_holder.transform.Rotate(Vector3.right, y_axis, Space.Self);
            //cam_holder.transform.localPosition = cam_holder.transform.localPosition * (1 - z_axis);
        }
    }
}
