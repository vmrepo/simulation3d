using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Cкрипт для стола (конвейра).
// Подразумевается, что у предмета вкючён (Box)CapsuleCollider, имеется Rigidbody, isKinematic = true, CollisionDetection - Continuous Speculative
// MeshCollider - нельзя т.к. глючит на событиях столкновения.

public class table : MonoBehaviour
{
    const float Timeout = 3.0f;
    private float timer = 0.0f;

    static public table Create(string name, float x, float y, float z, float ex, float ey, float ez, bool kinematic)
    {
        GameObject gameObject = GameObject.Instantiate(Resources.Load("tables/" + name, typeof(GameObject)) as GameObject);
        gameObject.GetComponent<Rigidbody>().isKinematic = kinematic;
        gameObject.transform.position = new Vector3(x, y, z) - gameObject.GetComponent<Rigidbody>().centerOfMass; ;
        gameObject.transform.rotation = Quaternion.Euler(ex, ey, ez);
        if (!kinematic)
            gameObject.GetComponent<table>().StartTimer();
        return gameObject.GetComponent<table>();
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
