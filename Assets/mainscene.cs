using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mainscene : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GameObject.Find("Main Camera").GetComponent<camera>().targetposition = new Vector3(0, 0, 0);
        Server0.Start();
    }

    // Update is called once per frame
    void Update()
    {
        Server0.Update();
    }

    void OnApplicationQuit()
    {
        Server0.Stop();
    }
}
