using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum JointPhysics
{
    Fixed,
    Hinge
}

public class CommonJoint
{
    private GameObject pivotObject = null;
    private GameObject gameObject = null;
    private Joint jointPhysics = null;
    private Vector3 positionInit = Vector3.zero;
    private Quaternion rotationInit = Quaternion.identity;
    private Quaternion rotationExtra = Quaternion.identity;

    public GameObject GetPivotObject()
    {
        return pivotObject;
    }

    public GameObject GetGameObject()
    {
        return gameObject;
    }

    public void Config(GameObject pivot, GameObject obj, bool kinematic, JointPhysics physics, Vector3 axis, Vector3 anchor)
    {
        pivotObject = pivot;
        gameObject = obj;

        gameObject.GetComponent<Rigidbody>().isKinematic = kinematic;

        if (jointPhysics != null)
        {
            MonoBehaviour.Destroy(jointPhysics);
            jointPhysics = null;
        }

        if (kinematic)
        {
            positionInit = Quaternion.Inverse(pivotObject.transform.rotation) * (gameObject.transform.position - pivotObject.transform.position);
            rotationInit = Quaternion.Inverse(pivotObject.transform.rotation) * gameObject.transform.rotation;
        }
        else
        {
            if (physics == JointPhysics.Fixed)
            {
                jointPhysics = pivotObject.AddComponent<FixedJoint>();
            }

            if (physics == JointPhysics.Hinge)
            {
                jointPhysics = pivotObject.AddComponent<HingeJoint>();
            }

            jointPhysics.connectedBody = gameObject.GetComponent<Rigidbody>();
            jointPhysics.axis = axis;
            jointPhysics.anchor = anchor;
        }
    }

    public void KinematicRotate(Quaternion rotation)
    {
        if (gameObject.GetComponent<Rigidbody>().isKinematic)
        {
            rotationExtra = rotation;
        }
    }

    public void KinematicUpdate()
    {
        if (gameObject.GetComponent<Rigidbody>().isKinematic)
        {
            gameObject.transform.position = pivotObject.transform.rotation * positionInit + pivotObject.transform.position;
            gameObject.transform.rotation = pivotObject.transform.rotation * rotationInit * rotationExtra;
        }
    }
}
