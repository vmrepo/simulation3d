using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class holder : MonoBehaviour
{
    [SerializeField]
    public float width = 0.03f;
    public float height = 0.14f;

    // Start is called before the first frame update
    void Start()
    {
        //устанавливаем размер
        transform.localScale = new Vector3(width, height, width);

        //размещаем leverhinge
        float h = GetComponent<HingeJoint>().connectedBody.GetComponent<leverhinge>().diameter;
        GetComponent<HingeJoint>().connectedBody.gameObject.transform.position = new Vector3(transform.position.x, transform.position.y + (height + h) / 2, transform.position.z);
        GetComponent<HingeJoint>().anchor = new Vector3(0.0f, 0.5f + h / height / 2, 0.0f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
