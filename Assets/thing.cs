using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Cкрипт для предмета, который захватывает манипулятор.
// Подразумевается, что у предмета вкючён BoxCollider, имеется Rigidbody, isKinematic = true, CollisionDetection - Continuous Speculative

public class thing : MonoBehaviour
{
    const float Timeout = 3.0f;
    private GameObject ownerObject = null;
    private Vector3 localPosition = Vector3.zero;
    private Quaternion localRotation = Quaternion.identity;
    private float waitTime = 0.0f;

    // адаптация, чтобы находится под захватом манипулятора (для отладки)
    bool isadapt = true;
    GameObject adapted = null;

    void adapt()
    {
        if (!isadapt && waitTime <= 0)
            isadapt = true;

        if (ownerObject == null && isadapt)
        {
            if (adapted == null)
                adapted = GameObject.Find("clamp(Clone)");
            transform.position = new Vector3(adapted.transform.position.x, transform.localScale.y / 2, adapted.transform.position.z);
            transform.rotation = Quaternion.identity;
        }
        else
            isadapt = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (ownerObject != null)
        {
            transform.position = ownerObject.transform.position + localPosition;
            transform.rotation = ownerObject.transform.rotation * localRotation;
        }

        if (waitTime > 0)
        {
            waitTime -= Time.deltaTime;

            if (waitTime <= 0)
            {
                gameObject.GetComponent<Rigidbody>().isKinematic = true;
            }
        }

        //adapt();
    }

    public void Attach(GameObject gameObject_)
    {
        ownerObject = gameObject_;
        gameObject.GetComponent<Rigidbody>().isKinematic = true;
        localPosition = transform.position - ownerObject.transform.position;
        localRotation = Quaternion.Inverse(ownerObject.transform.rotation) * transform.rotation;
    }

    public void Detach()
    {
        ownerObject = null;
        gameObject.GetComponent<Rigidbody>().isKinematic = false;
        gameObject.GetComponent<Rigidbody>().useGravity = true;
        waitTime = Timeout;
    }
}
