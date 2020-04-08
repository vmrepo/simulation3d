using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camera : MonoBehaviour
{
    [SerializeField]
    public Vector3 targetposition; //невидимая цель для камеры
    public float zoomSpeed = 5.0f; //скорость приближения камеры
    public float mouseSensRotatiton = 0.6f;
    public float mouseSensTranslatiton = 0.2f;
    public Camera cam_holder;

    private Vector3 _offset;
    private float _rotY, _rotX;

    public void Init()
    {
        _rotY = 0.0f;
        _rotX = 0.0f;
        _offset = targetposition - transform.position; //получает начальное смещение
        LookAtTarget();
    }

    void Start()
    {
        Init();
    }

    void LookAtTarget()
    {
        Quaternion rotation = Quaternion.Euler(_rotY, _rotX, 0); //задает вращение камеры 
        transform.position = targetposition - (rotation * _offset);
        transform.LookAt(targetposition);
    }

    void LateUpdate()
    {
        float input = Input.GetAxis("Mouse ScrollWheel"); //крутится колесико
        if (input != 0) 
        {
            transform.Translate(Vector3.forward * input);

            Init();
        }

        if (Input.GetMouseButton(0)) //левая кнопка мыши
        { //вращение вокруг объекта
            _rotX += -Input.GetAxis("Mouse X") * mouseSensRotatiton; //поворот камеры вокруг объекта и сохранение координат
            _rotY += -Input.GetAxis("Mouse Y") * mouseSensRotatiton;

            LookAtTarget();
        }

        if (Input.GetMouseButton(1)) //правая кнопка
        {
            //обзор вокруг объекта
            //смещение камеры

            float x_axis = -Input.GetAxis("Mouse X") * mouseSensTranslatiton;
            float y_axis = -Input.GetAxis("Mouse Y") * mouseSensTranslatiton;

            Vector3 v0 =  transform.rotation * Vector3.right * x_axis;
            Vector3 v1 = transform.rotation * Vector3.up * y_axis;
            targetposition = new Vector3(targetposition.x + v0.x + v1.x, targetposition.y + v0.y + v1.y, targetposition.z + v0.z + v1.z);

            LookAtTarget();
        }

        /*if (Input.GetMouseButton(2)) //нажимается колесико
        {
            //обзор вокруг камеры
            float x_axis = -Input.GetAxis("Mouse X") * mouse_sens;
            float y_axis = -Input.GetAxis("Mouse Y") * mouse_sens;

            cam_holder.transform.Rotate(Vector3.up, x_axis, Space.World);
            cam_holder.transform.Rotate(Vector3.right, y_axis, Space.Self);
        }*/
    }
}
