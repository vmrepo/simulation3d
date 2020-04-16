using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mainscene : MonoBehaviour
{
    /*private manipulator1 manipulator0 = null;
    private manipulator2 manipulator1 = null;*/

    // Start is called before the first frame update
    void Start()
    {
#if !UNITY_EDITOR

        string[] args = System.Environment.GetCommandLineArgs();

        for (int i = 1; i < args.Length ; i++)
        {
            if (args[i].Substring(0, 6).ToLower() == "port0:")
            {
                int port;
                if (Int32.TryParse(args[i].Substring(6), out port))
                {
                    Server0.Port = port;
                }
                else
                {
                    Server0.Log("bad parmeter port");
                }
            }

            if (args[i].Substring(0, 8).ToLower() == "logfile:")
            {
                Server0.Logfile = args[i].Substring(8);
            }
        }

#endif
        /*manipulator0 = new manipulator1();
        manipulator0.config.x = -2;
        manipulator0.config.Kinematic = false;
        manipulator0.Place();

        manipulator1 = new manipulator2();
        manipulator1.config.x = 2;
        manipulator1.config.Kinematic = false;
        manipulator1.Place();*/

        GameObject.Find("Main Camera").GetComponent<camera>().targetposition = new Vector3(0, 0, 0);
        Server0.Start();
    }

    // Update is called once per frame
    void Update()
    {
        Server0.Update();

        if (Input.GetKeyDown(KeyCode.F10))
        {
            Application.Quit();
        }

        /*float delta = 10.0f;

        if (Input.GetKeyDown(KeyCode.Q))
        {
            manipulator0.SetPos(manipulator0.GetPos0() - delta, manipulator0.GetPos1(), manipulator0.GetPos2());
            manipulator1.SetPos(manipulator1.GetPos0() - delta, manipulator1.GetPos1(), manipulator1.GetPos2());
        }

        if (Input.GetKeyDown(KeyCode.W))
        {
            manipulator0.SetPos(manipulator0.GetPos0() + delta, manipulator0.GetPos1(), manipulator0.GetPos2());
            manipulator1.SetPos(manipulator1.GetPos0() + delta, manipulator1.GetPos1(), manipulator1.GetPos2());
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            manipulator0.SetPos(manipulator0.GetPos0(), manipulator0.GetPos1() - delta, manipulator0.GetPos2());
            manipulator1.SetPos(manipulator1.GetPos0(), manipulator1.GetPos1() - delta, manipulator1.GetPos2());
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            manipulator0.SetPos(manipulator0.GetPos0(), manipulator0.GetPos1() + delta, manipulator0.GetPos2());
            manipulator1.SetPos(manipulator1.GetPos0(), manipulator1.GetPos1() + delta, manipulator1.GetPos2());
        }

        if (Input.GetKeyDown(KeyCode.Z))
        {
            manipulator0.SetPos(manipulator0.GetPos0(), manipulator0.GetPos1(), manipulator0.GetPos2() - delta);
            manipulator1.SetPos(manipulator1.GetPos0(), manipulator1.GetPos1(), manipulator1.GetPos2() - delta);
        }

        if (Input.GetKeyDown(KeyCode.X))
        {
            manipulator0.SetPos(manipulator0.GetPos0(), manipulator0.GetPos1(), manipulator0.GetPos2() + delta);
            manipulator1.SetPos(manipulator1.GetPos0(), manipulator1.GetPos1(), manipulator1.GetPos2() + delta);
        }*/
    }

    void OnApplicationQuit()
    {
        Server0.Stop();
    }
}
