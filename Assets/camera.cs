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
    private Quaternion _rotation;

    public void Init()
    {
        transform.LookAt(targetposition);
        _rotation = Quaternion.identity;
        _offset = targetposition - transform.position;
    }

    void Start()
    {
        Init();
    }

    void LookAtTarget()
    {
        transform.position = targetposition - (_rotation * _offset);
        transform.LookAt(targetposition);
    }

    void LateUpdate()
    {
        float input = Input.GetAxis("Mouse ScrollWheel"); //крутится колесико
        if (input != 0) 
        {
            Vector3 v = (targetposition - transform.position).normalized;

            transform.position += v * input;
            targetposition += v * input;
        }

        if (Input.GetMouseButton(0)) //левая кнопка
        {
            //вращение
            float rotX = Input.GetAxis("Mouse X") * mouseSensRotatiton;
            float rotY = Input.GetAxis("Mouse Y") * mouseSensRotatiton;

            _rotation *= Quaternion.Euler(rotY, rotX, 0);
            
            LookAtTarget();
        }

        if (Input.GetMouseButton(1)) //правая кнопка
        {
            //смещение
            float x_axis = -Input.GetAxis("Mouse X") * mouseSensTranslatiton;
            float y_axis = -Input.GetAxis("Mouse Y") * mouseSensTranslatiton;

            Vector3 v0 =  transform.rotation * Vector3.right * x_axis;
            Vector3 v1 = transform.rotation * Vector3.up * y_axis;
            targetposition = new Vector3(targetposition.x + v0.x + v1.x, targetposition.y + v0.y + v1.y, targetposition.z + v0.z + v1.z);

            LookAtTarget();
        }
    }
}
