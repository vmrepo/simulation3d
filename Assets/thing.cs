using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Cкрипт для предмета, который захватывает манипулятор.
// Подразумевается, что у предмета вкючён (Box)CapsuleCollider, имеется Rigidbody, isKinematic = true, CollisionDetection - Continuous Speculative
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
    Vector3 adaptCenterOfMass = Vector3.zero;
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
            adaptCenterOfMass = GetComponent<Rigidbody>().centerOfMass;
            adpatOffset = transform.localRotation * adaptCenterOfMass;
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
        gameObject.transform.position = new Vector3(x, y, z) - gameObject.GetComponent<Rigidbody>().centerOfMass; ;
        gameObject.transform.rotation = Quaternion.Euler(ex, ey, ez);
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
        transform.position = new Vector3(x, y, z) - GetComponent<Rigidbody>().centerOfMass;
        transform.rotation = Quaternion.Euler(ex, ey, ez);
        gameObject.GetComponent<Rigidbody>().isKinematic = kinematic;
        if (!kinematic)
            StartTimer();
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
