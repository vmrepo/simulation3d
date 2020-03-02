using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class chassis : MonoBehaviour
{
    [SerializeField]
    public float height = 1.0f;
    public float width = 0.5f;

    // Start is called before the first frame update
    void Start()
    {
        Vector3 position = new Vector3(0.0f, 0.0f, 0.0f);

        //ставим в начало координат на нижнюю грань и устанвливаем размеры
        //можно ставить в любое место, всё должно автоматом посчитаться
        transform.position = new Vector3(position.x, position.y + height / 2, position.z);
        transform.localScale = new Vector3(width, height, width);

        //размещаем roatatingplatform
        float h = GetComponent<HingeJoint>().connectedBody.GetComponent<rotatingplatform>().height;
        GetComponent<HingeJoint>().connectedBody.gameObject.transform.position = new Vector3(transform.position.x, transform.position.y + (height + h) / 2, transform.position.z);
        GetComponent<HingeJoint>().anchor = new Vector3(0.0f, 0.5f, 0.0f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
