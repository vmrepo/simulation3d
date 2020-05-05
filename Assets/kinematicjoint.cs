using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KinematicJoint
{
    private GameObject gameObject = null;
    private GameObject nextObject = null;
    private Vector3 positionInit = Vector3.zero;
    private Quaternion rotationInit = Quaternion.identity;
    private Quaternion rotationExtra = Quaternion.identity;

    public GameObject GetGameObject()
    {
        return gameObject;
    }

    public void AttachGameObject(GameObject obj, GameObject next = null)
    {
        gameObject = obj;
        nextObject = (next == null) ? gameObject.GetComponent<Joint>().connectedBody.gameObject : next;
        positionInit = Quaternion.Inverse(gameObject.transform.rotation) * (nextObject.transform.position - gameObject.transform.position);
        rotationInit = Quaternion.Inverse(gameObject.transform.rotation) * nextObject.transform.rotation;
    }

    public void Rotate(Quaternion rotation)
    {
        rotationExtra = rotation;
    }

    public void Update()
    {
        if (gameObject == null)
            return;

        if (nextObject.GetComponent<Rigidbody>().isKinematic)
        {
            nextObject.transform.position = gameObject.transform.rotation * positionInit + gameObject.transform.position;
            nextObject.transform.rotation = gameObject.transform.rotation * rotationInit * rotationExtra;
        }
    }
}
