using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class arm : MonoBehaviour
{
    [SerializeField]
    public float width = 0.03f;
    public float height = 0.6f;

    // Start is called before the first frame update
    void Start()
    {
        //устанавливаем размер
        transform.localScale = new Vector3(width, height, width);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
