using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mainscene : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GameObject.Find("Main Camera").GetComponent<camera>().targetposition = new Vector3(0, 1, 0);
        Server0.Start();
    }

    void OnApplicationQuit()
    {
        Server0.Stop();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
