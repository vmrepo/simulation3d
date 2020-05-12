using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DriveJoint
{
    public AngleRange AngleRange = new AngleRange();
    public float KinematicAngularVelocity = 100.0f;
    public float Proportional = 1.5f;
    public float Integral = 0.0f;
    public float Differential = 1.1f;

    private CommonJoint commonJoint = null;
    private GameObject pivotObject = null;
    private GameObject gameObject = null;
    private Quaternion rotationInit = Quaternion.identity;

    private float deltaSAngle = 0.0f;

    //оставлено, чтобы поддержать старый вариант, поддерживает только физический тип соединения
    //вручную создаётся Joint, ancjor и axis выставляются вручную, а не автоматически в CommonJoint    
    //axis обязана совпадать с локальным направлением вверх для шарнира(чаще цилиндра) т.е. Vector3.up
    //иначе мотор для физики неправильно будет управляться и изменятся направления в кинематике
    //у объекта должен быть единственный HingeJoint
    public void Attach(GameObject pivot, GameObject obj)
    {
        commonJoint = null;
        pivotObject = pivot;
        gameObject = obj;
        rotationInit = Quaternion.Inverse(pivotObject.transform.rotation) * gameObject.transform.rotation;
    }

    public void Attach(CommonJoint joint)
    {
        commonJoint = joint;
        pivotObject = joint.GetPivotObject();
        gameObject = joint.GetGameObject();
        rotationInit = Quaternion.Inverse(pivotObject.transform.rotation) * gameObject.transform.rotation;
    }

    public void Update()
    {
        Quaternion localRotation = Quaternion.Inverse(pivotObject.transform.rotation) * gameObject.transform.rotation;
        Quaternion rotation = Quaternion.Inverse(rotationInit) * localRotation;

        float angle = AngleRange.CheckRange(Vector3.Dot(rotation.eulerAngles, Vector3.up));

        float deltaAngle = AngleRange.Delta(angle, AngleRange.GetTarget());

        if (gameObject.GetComponent<Rigidbody>().isKinematic)
        {
            float step = Time.deltaTime * KinematicAngularVelocity * Mathf.Sign(deltaAngle);
            angle = Mathf.Abs(deltaAngle) < Mathf.Abs(step) ? AngleRange.GetTarget() : angle + step;
            commonJoint.KinematicRotate(Quaternion.AngleAxis(angle, Vector3.up));
            return;
        }

        HingeJoint hingeJoint = pivotObject.GetComponent<HingeJoint>();

        if (Mathf.Sign(deltaAngle) > 0)
            deltaSAngle += deltaAngle;
        else
            deltaSAngle -= deltaAngle;

        float deltaVelocity = -hingeJoint.velocity;

        if (Mathf.Sign(deltaVelocity) == Mathf.Sign(deltaAngle))
            deltaVelocity = -Mathf.Abs(deltaVelocity);
        else
            deltaVelocity = Mathf.Abs(deltaVelocity);

        float kP = Proportional;
        float kI = Integral;
        float kD = Differential;

        JointMotor motor = hingeJoint.motor;
        motor.targetVelocity = -Mathf.Sign(deltaAngle) * 1000000000;
        motor.force = kP * Mathf.Abs(deltaAngle) + kI * deltaSAngle + kD * deltaVelocity;
        motor.freeSpin = true;
        hingeJoint.motor = motor;
        hingeJoint.useMotor = true;
    }
}
