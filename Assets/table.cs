using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Cкрипт для стола (конвейра).
// Подразумевается, что у предмета вкючён один из двух: BoxCollider или CapsuleCollider, имеется Rigidbody, isKinematic = true, CollisionDetection - Continuous Speculative
// MeshCollider - нельзя т.к. глючит на событиях столкновения.

public class table : MonoBehaviour
{
    const float Timeout = 3.0f;
    private float timer = 0.0f;
    private float scale = 1.0f;

    static public table Create(string name, float x, float y, float z, float ex, float ey, float ez, float scale_, bool kinematic)
    {
        GameObject gameObject = GameObject.Instantiate(Resources.Load("tables/" + name, typeof(GameObject)) as GameObject);
        gameObject.GetComponent<Rigidbody>().isKinematic = kinematic;
        gameObject.transform.localScale = Vector3.Scale(gameObject.transform.localScale, Vector3.one * scale_);

        Vector3 localCenter = Vector3.zero;
        if (gameObject.GetComponent<BoxCollider>() != null)
            localCenter = Vector3.Scale(gameObject.transform.localScale, gameObject.GetComponent<BoxCollider>().center);
        if (gameObject.GetComponent<CapsuleCollider>() != null)
            localCenter = Vector3.Scale(gameObject.transform.localScale, gameObject.GetComponent<CapsuleCollider>().center);

        gameObject.transform.rotation = Quaternion.Euler(ex, ey, ez);
        gameObject.transform.position = new Vector3(x, y, z) - gameObject.transform.rotation * localCenter;

        gameObject.GetComponent<table>().scale = scale_;

        if (!kinematic)
            gameObject.GetComponent<table>().StartTimer();
        return gameObject.GetComponent<table>();
    }

    public void Remove()
    {
        Destroy(gameObject);
    }

    public void SetPos(float x, float y, float z, float ex, float ey, float ez, float scale_, bool kinematic)
    {
        transform.localScale = Vector3.Scale(transform.localScale, Vector3.one * scale_);

        Vector3 localCenter = Vector3.zero;
        if (GetComponent<BoxCollider>() != null)
            localCenter = Vector3.Scale(transform.localScale, GetComponent<BoxCollider>().center);
        if (GetComponent<CapsuleCollider>() != null)
            localCenter = Vector3.Scale(transform.localScale, GetComponent<CapsuleCollider>().center);

        transform.rotation = Quaternion.Euler(ex, ey, ez);
        transform.position = new Vector3(x, y, z) - transform.rotation * localCenter;

        scale = scale_;

        gameObject.GetComponent<Rigidbody>().isKinematic = kinematic;
        if (!kinematic)
            StartTimer();
    }

    public List<Vector3> GetPos()
    {
        List<Vector3> ret = new List<Vector3>();

        if (GetComponent<BoxCollider>() != null)
        {
            BoxCollider box = GetComponent<BoxCollider>();
            ret.Add(transform.position + Vector3.Scale(transform.localScale, box.center));
            ret.Add(transform.rotation.eulerAngles);
            ret.Add(Vector3.Scale(transform.localScale, box.size));
            ret.Add(Vector3.one * scale);
        }

        if (GetComponent<CapsuleCollider>() != null)
        {
            CapsuleCollider capsule = GetComponent<CapsuleCollider>();
            ret.Add(transform.position + Vector3.Scale(transform.localScale, capsule.center));
            ret.Add(transform.rotation.eulerAngles);
            switch (capsule.direction)
            {
                case 0:
                    ret.Add(Vector3.Scale(transform.localScale, new Vector3(capsule.height, capsule.radius, capsule.radius)));
                    break;

                case 1:
                    ret.Add(Vector3.Scale(transform.localScale, new Vector3(capsule.radius, capsule.height, capsule.radius)));
                    break;

                case 2:
                    ret.Add(Vector3.Scale(transform.localScale, new Vector3(capsule.radius, capsule.radius, capsule.height)));
                    break;
            }
            ret.Add(Vector3.one * scale);
        }

        return ret;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        WaitTimer();
    }

    public void StartTimer()
    {
        timer = Timeout;
    }

    private void WaitTimer()
    {
        if (timer > 0)
        {
            timer -= Time.deltaTime;

            if (timer <= 0)
            {
                gameObject.GetComponent<Rigidbody>().isKinematic = true;
            }
        }
    }
}
