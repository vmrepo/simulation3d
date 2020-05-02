using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KinematicJoint
{
    private GameObject gameObject = null;
    private GameObject nextObject = null;
    private bool isInit = false;
    private Vector3 prevPosition = Vector3.zero;
    private Quaternion prevRotation = Quaternion.identity;

    public void AttachGameObject(GameObject obj)
    {
        gameObject = obj;
        nextObject = gameObject.GetComponent<Joint>().connectedBody.gameObject;
    }

    public void Update()
    {
        if (gameObject == null || nextObject == null)
            return;

        if (!isInit)
        {
            prevPosition = gameObject.transform.position;
            prevRotation = gameObject.transform.rotation;
            isInit = true;
        }

        Quaternion rotation = Quaternion.Inverse(prevRotation) * gameObject.transform.rotation;
        Vector3 position = rotation * (nextObject.transform.position - prevPosition);

        nextObject.transform.position = gameObject.transform.position + position;
        nextObject.transform.rotation = nextObject.transform.rotation * rotation;

        prevPosition = gameObject.transform.position;
        prevRotation = gameObject.transform.rotation;
    }
}
