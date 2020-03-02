using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class leverhinge : MonoBehaviour
{
    [SerializeField]
    public float diameter = 0.08f;
    public float width = 0.023f;

    // Start is called before the first frame update
    void Start()
    {
        //устанавливаем размер
        transform.localScale = new Vector3(diameter, width, diameter);

        //размещаем lever
        float h = GetComponent<FixedJoint>().connectedBody.GetComponent<lever>().height;
        GetComponent<FixedJoint>().connectedBody.gameObject.transform.position = new Vector3(transform.position.x, transform.position.y + (diameter + h) / 2, transform.position.z);
        GetComponent<FixedJoint>().anchor = new Vector3(0.0f, 0.5f, 0.0f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
