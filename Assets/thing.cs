using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Cкрипт для предмета, который захватывает манипулятор.
// Подразумевается, что у предмета вкючён один из двух: BoxCollider или CapsuleCollider, имеется Rigidbody, isKinematic = true, CollisionDetection - Continuous Speculative
// MeshCollider - нельзя т.к. глючит на событиях столкновения.
// Если Rigidbody предмета при захвате не находится в состоянии isKinematic = true будут артефакты.
// Таймер при падении должен успеть включить isKinematic до следующего захвата, но чаще всего так и будет.

public class thing : MonoBehaviour
{
    const float Timeout = 3.0f;
    private float timer = 0.0f;
    public CommonJoint joint = new CommonJoint();

    // адаптация, чтобы находится под захватом манипулятора (для отладки)
    GameObject adaptTo = null;
    bool adaptIs = false;
    Vector3 adpatOffset = Vector3.zero;
    void adapt()
    {
        if (adaptTo == null)
        {
            adaptTo = GameObject.Find("clamp(Clone)");
        }

        if (!adaptIs && timer <= 0)
        {
            adaptIs = true;
            adpatOffset = transform.rotation * Vector3.zero;
        }

        if (joint.GetPivotObject() == null && adaptIs)
        {
            transform.position = new Vector3(adaptTo.transform.position.x - adpatOffset.x, transform.position.y, adaptTo.transform.position.z - adpatOffset.z);
        }
        else
        {
            adaptIs = false;
        }
    }

    static public thing Create(string name, float x, float y, float z, float ex, float ey, float ez, bool kinematic)
    {
        GameObject gameObject = GameObject.Instantiate(Resources.Load("things/" + name, typeof(GameObject)) as GameObject);
        gameObject.GetComponent<Rigidbody>().isKinematic = kinematic;

        Vector3 localCenter = Vector3.zero;
        if (gameObject.GetComponent<BoxCollider>() != null)
            localCenter = Vector3.Scale(gameObject.transform.localScale, gameObject.GetComponent<BoxCollider>().center);
        if (gameObject.GetComponent<CapsuleCollider>() != null)
            localCenter = Vector3.Scale(gameObject.transform.localScale, gameObject.GetComponent<CapsuleCollider>().center);

        gameObject.transform.rotation = Quaternion.Euler(ex, ey, ez);
        gameObject.transform.position = new Vector3(x, y, z) - gameObject.transform.rotation * localCenter;

        if (!kinematic)
            gameObject.GetComponent<thing>().StartTimer();
        return gameObject.GetComponent<thing>();
    }

    public void Remove()
    {
        Destroy(gameObject);
    }

    public void SetPos(float x, float y, float z, float ex, float ey, float ez, bool kinematic)
    {
        Vector3 localCenter = Vector3.zero;
        if (GetComponent<BoxCollider>() != null)
            localCenter = Vector3.Scale(transform.localScale, GetComponent<BoxCollider>().center);
        if (GetComponent<CapsuleCollider>() != null)
            localCenter = Vector3.Scale(transform.localScale, GetComponent<CapsuleCollider>().center);

        transform.rotation = Quaternion.Euler(ex, ey, ez);
        transform.position = new Vector3(x, y, z) - transform.rotation * localCenter;

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
        //adapt();
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

    public void KinematicUpdate()
    {
        joint.KinematicUpdate();
    }
}
