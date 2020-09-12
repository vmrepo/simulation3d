using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camera : MonoBehaviour
{
    public Vector3 targetposition;

    [SerializeField]
    public float mouseSensitivity = 0.5f;
    public float zoomSpeed = 5.0f;

    public void Init()
    {
        transform.LookAt(targetposition);
    }

    void Start()
    {
        transform.LookAt(targetposition);
    }

    void LateUpdate()
    {
        float input = Input.GetAxis("Mouse ScrollWheel"); //крутится колесико - движение по курсу
        if (input != 0) 
        {
            Vector3 v = (targetposition - transform.position).normalized;

            transform.position += v * input;
            targetposition += v * input;
        }

        if (Input.GetMouseButton(0)) //левая кнопка - смещение
        {
            float x_axis = -Input.GetAxis("Mouse X") * mouseSensitivity;
            float y_axis = -Input.GetAxis("Mouse Y") * mouseSensitivity;

            Vector3 v0 =  transform.rotation * Vector3.right * x_axis;
            Vector3 v1 = transform.rotation * Vector3.up * y_axis;

            Vector3 targetposition_ = targetposition;

            targetposition = new Vector3(targetposition.x + v0.x + v1.x, targetposition.y + v0.y + v1.y, targetposition.z + v0.z + v1.z);

            transform.position += targetposition - targetposition_;

            transform.LookAt(targetposition);
        }

        if (Input.GetMouseButton(1)) //правая кнопка - вращение
        {
            float x_axis = Input.GetAxis("Mouse X") * mouseSensitivity;
            float y_axis = Input.GetAxis("Mouse Y") * mouseSensitivity;

            Vector3 v0 = transform.rotation * Vector3.right * x_axis;
            Vector3 v1 = transform.rotation * Vector3.up * y_axis;

            targetposition = new Vector3(targetposition.x + v0.x + v1.x, targetposition.y + v0.y + v1.y, targetposition.z + v0.z + v1.z);

            transform.LookAt(targetposition);
        }
    }

    public bool IsdDepth = false;
    private Shader _shader;
    private Shader shader
    {
        get { return _shader != null ? _shader : (_shader = Shader.Find("Custom/RenderDepth")); }
    }

    private Material _material;
    private Material material
    {
        get
        {
            if (_material == null)
            {
                _material = new Material(shader);
                _material.hideFlags = HideFlags.HideAndDontSave;
            }
            return _material;
        }
    }

    private void OnRenderImage(RenderTexture src, RenderTexture dest)
    {
        if (shader != null && IsdDepth)
        {
            Graphics.Blit(src, dest, material);
        }
        else
        {
            Graphics.Blit(src, dest);
        }
    }
}
