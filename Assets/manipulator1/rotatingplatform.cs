using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotatingplatform : MonoBehaviour
{
    [SerializeField]
    public float diameter = 0.5f;
    public float height = 0.02f;

    // Start is called before the first frame update
    void Start()
    {
        //устанавливаем размер
        transform.localScale = new Vector3(diameter, height, diameter);

        //размещаем holder
        float h = GetComponent<FixedJoint>().connectedBody.GetComponent<holder>().height;
        GetComponent<FixedJoint>().connectedBody.gameObject.transform.position = new Vector3(transform.position.x, transform.position.y + (height + h) / 2, transform.position.z);
        GetComponent<FixedJoint>().anchor = new Vector3(0.0f, 0.5f, 0.0f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
