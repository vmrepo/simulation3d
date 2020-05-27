using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// физическое или кинематическое соединение секций, соединение может быть подвижным или фиксированным
// для управления подвижным соединением нужно ещё ставить DriveJoint
// подвижное соединение ассоиируется только с объектом, который должен вращаться вокруг своей локальной оси Vector3.up - чаще всего цилиндр
// именно цилиндр вращается, а следующее звено соединяется к нему фиксированно
// в случае физического варианта соединения будет создан HingeJoint у звена, к которому подсоединяется цилиндр

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

    public void Config(GameObject pivot, GameObject obj, bool kinematic, JointPhysics physics)
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
                jointPhysics.axis = Vector3.up;
                jointPhysics.anchor = Vector3.zero;
            }

            if (physics == JointPhysics.Hinge)
            {
                jointPhysics = pivotObject.AddComponent<HingeJoint>();
                jointPhysics.axis = Quaternion.Inverse(pivotObject.transform.rotation) * gameObject.transform.rotation * Vector3.up;
                Vector3 localposition = Quaternion.Inverse(pivotObject.transform.rotation) * (gameObject.transform.position - pivotObject.transform.position);
                Vector3 inversescale = new Vector3(1 / pivotObject.transform.localScale.x, 1 / pivotObject.transform.localScale.y, 1 / pivotObject.transform.localScale.z);
                jointPhysics.anchor = Vector3.Scale(localposition, inversescale);
            }

            jointPhysics.connectedBody = gameObject.GetComponent<Rigidbody>();
        }
    }

    public void Break()
    {
        pivotObject = null;
        gameObject = null;

        if (jointPhysics != null)
        {
            MonoBehaviour.Destroy(jointPhysics);
            jointPhysics = null;
        }
    }

    public void KinematicRotate(Quaternion rotation)
    {
        if (gameObject != null && gameObject.GetComponent<Rigidbody>().isKinematic)
        {
            rotationExtra = rotation;
        }
    }

    public void KinematicUpdate()
    {
        if (gameObject != null && gameObject.GetComponent<Rigidbody>().isKinematic)
        {
            gameObject.transform.position = pivotObject.transform.rotation * positionInit + pivotObject.transform.position;
            gameObject.transform.rotation = pivotObject.transform.rotation * rotationInit * rotationExtra;
        }
    }
}
