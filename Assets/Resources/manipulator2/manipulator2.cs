﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class configmanipulator2
{
    public float x = 0.0f;
    public float y = 0.0f;
    public float z = 0.0f;
    public float angle = 0.0f;
    public bool Kinematic = false;
    public float KinematicAngularVelocity = 0.0f;//for compatible
    public bool UseGravity = false;
    public float ChassisHeight = 1.0f;
    public float ChassisWidth = 0.5f;
    public float RotatingplatformMass = 1.0f;
    public float RotatingplatformKinematicAngularVelocity = 100.0f;
    public float RotatingplatformACSProportional = 1.5f;
    public float RotatingplatformACSIntegral = 0.0f;
    public float RotatingplatformACSDifferential = 1.1f;
    public float RotatingplatformAngle0 = 0.0f;
    public float RotatingplatformAngle1 = 360.0f;
    public float RotatingplatformDiameter = 0.5f;
    public float RotatingplatformWidth = 0.01f * 2;/*mul 2 for cylinder*/
    public float LeverhingeMass = 1.0f;
    public float LeverhingeDiameter = 0.08f;
    public float LeverMass = 1.0f;
    public float LeverKinematicAngularVelocity = 100.0f;
    public float LeverACSProportional = 1.5f;
    public float LeverACSIntegral = 0.0f;
    public float LeverACSDifferential = 1.1f;
    public float LeverWidth = 0.03f;
    public float LeverLength = 0.6f;
    public float ArmMass = 1.0f;
    public float ArmKinematicAngularVelocity = 100.0f;
    public float ArmACSProportional = 1.5f;
    public float ArmACSIntegral = 0.0f;
    public float ArmACSDifferential = 1.1f;
    public float ArmAngle0 = 60.0f;
    public float ArmAngle1 = 120.0f;
    public float ArmhingeMass = 1.0f;
    public float ArmhingeDiameter = 0.08f;
    public float ArmhingeWidth = 0.0115f * 2;/*mul 2 for cylinder*/
    public float ArmWidth = 0.03f;
    public float ArmLength = 0.25f;
    public float HolderMass = 1.0f;
    public float HolderWidth = 0.03f;
    public float HolderLength = 0.14f;
    public float HoldersDistance = 2 * 0.0895f;
    public float WheelhingeMass = 1.0f;
    public float WheelhingeWidth = 0.023f * 2;/*mul 2 for cylinder*/
    public float WheelMass = 1.0f;
    public float WheelDiameter = 0.32f;
    public float WheelWidth = 0.0115f * 2;/*mul 2 for cylinder*/
    public float WheelLever = 0.12f;
    public float LeverAngle0 = 10.0f;
    public float LeverAngle1 = 60.0f;
    public float CaptureConnectorMass = 0.2f;
    public float CaptureConnectorDiameter = 0.1f;
    public float CaptureConnectorWidth = 0.02f;
    public float CaptureArmMass = 0.2f;
    public float CaptureArmKinematicAngularVelocity = 100.0f;
    public float CaptureArmACSProportional = 1.5f;
    public float CaptureArmACSIntegral = 0.0f;
    public float CaptureArmACSDifferential = 1.1f;
    public float CaptureArmAngle0 = 0.0f;
    public float CaptureArmAngle1 = 360.0f;
    public float CaptureArmDiameter = 0.03f;
    public float CaptureArmLength = 0.4f;
    public float CaptureClamphingeMass = 0.2f;
    public float CaptureClamphingeDiameter = 0.1f;
    public float CaptureClamphingeWidth = 0.03f;
    public float CaptureClampMass = 0.1f;
    public float CaptureClampKinematicAngularVelocity = 100.0f;
    public float CaptureClampACSProportional = 1.5f;
    public float CaptureClampACSIntegral = 0.0f;
    public float CaptureClampACSDifferential = 1.1f;
    public float CaptureClampAngle0 = 240.0f;
    public float CaptureClampAngle1 = 120.0f;
    public float CaptureClampDiameter = 0.06f;
    public float CaptureClampWidth = 0.028f;
    public float CaptureClampConnectorKinematicAngularVelocity = 100.0f;
    public float CaptureClampConnectorACSProportional = 1.5f;
    public float CaptureClampConnectorACSIntegral = 0.0f;
    public float CaptureClampConnectorACSDifferential = 1.1f;
    public float CaptureClampConnectorAngle0 = 0.0f;
    public float CaptureClampConnectorAngle1 = 360.0f;
    public bool FingerKinematic = true;
    public float FingerConnectorMass = 0.005f;
    public float FingerConnectorHeight = 0.001f;
    public float FingerRibHeight = 0.005f;
    public float FingerSectionMass = 0.005f;
    public float FingerSectionHeight = 0.015f;
    public float FingerSectionWidth = 0.025f;
    public float FingerSectionThick = 0.005f;
    public float FingerSectionKinematicAngularVelocity = 100.0f;
    public float FingerSectionACSProportional = 1.5f;
    public float FingerSectionACSIntegral = 0.0f;
    public float FingerSectionACSDifferential = 1.1f;
    public float FingerSectionAngle0 = 340.0f;
    public float FingerSectionAngle1 = 0.0f;
    public int FingerSectionCount = 4;
    public bool FingerDown = true;
}

public class manipulator2 : device
{
    public configmanipulator2 config = new configmanipulator2();

    private bool isinited = false;
    private GameObject chassis = null;
    private GameObject rotatingplatform = null;
    private GameObject leverhinge = null;
    private GameObject lever = null;
    private GameObject armhinge = null;
    private GameObject arm = null;
    private GameObject holder1 = null;
    private GameObject wheelhinge1 = null;
    private GameObject wheel1 = null;
    private GameObject leverhinge1 = null;
    private GameObject lever1 = null;
    private GameObject armhinge1 = null;
    private GameObject holder2 = null;
    private GameObject wheelhinge2 = null;
    private GameObject wheel2 = null;

    private capture1 capture = null;
    private List<finger1> fingers = new List<finger1>();
    private thing thing = null;

    private AngleRange kinematicanglerange0 = new AngleRange();
    private AngleRange kinematicanglerange1 = new AngleRange();
    private AngleRange kinematicanglerange2 = new AngleRange();

    public override void Place()
    {
        if (!isinited)
        {
            chassis = GameObject.Instantiate(Resources.Load("manipulator2/chassis", typeof(GameObject)) as GameObject);
            rotatingplatform = GameObject.Instantiate(Resources.Load("manipulator2/rotatingplatform", typeof(GameObject)) as GameObject);
            leverhinge = GameObject.Instantiate(Resources.Load("manipulator2/leverhinge", typeof(GameObject)) as GameObject);
            lever = GameObject.Instantiate(Resources.Load("manipulator2/lever", typeof(GameObject)) as GameObject);
            armhinge = GameObject.Instantiate(Resources.Load("manipulator2/armhinge", typeof(GameObject)) as GameObject);
            arm = GameObject.Instantiate(Resources.Load("manipulator2/arm", typeof(GameObject)) as GameObject);
            holder1 = GameObject.Instantiate(Resources.Load("manipulator2/holder1", typeof(GameObject)) as GameObject);
            wheelhinge1 = GameObject.Instantiate(Resources.Load("manipulator2/wheelhinge1", typeof(GameObject)) as GameObject);
            wheel1 = GameObject.Instantiate(Resources.Load("manipulator2/wheel1", typeof(GameObject)) as GameObject);
            leverhinge1 = GameObject.Instantiate(Resources.Load("manipulator2/leverhinge1", typeof(GameObject)) as GameObject);
            lever1 = GameObject.Instantiate(Resources.Load("manipulator2/lever1", typeof(GameObject)) as GameObject);
            armhinge1 = GameObject.Instantiate(Resources.Load("manipulator2/armhinge1", typeof(GameObject)) as GameObject);
            holder2 = GameObject.Instantiate(Resources.Load("manipulator2/holder2", typeof(GameObject)) as GameObject);
            wheelhinge2 = GameObject.Instantiate(Resources.Load("manipulator2/wheelhinge2", typeof(GameObject)) as GameObject);
            wheel2 = GameObject.Instantiate(Resources.Load("manipulator2/wheel2", typeof(GameObject)) as GameObject);

            chassis.GetComponent<HingeJoint>().connectedBody = rotatingplatform.GetComponent<Rigidbody>();
            rotatingplatform.GetComponents<FixedJoint>()[0].connectedBody = holder1.GetComponent<Rigidbody>();
            rotatingplatform.GetComponents<FixedJoint>()[1].connectedBody = holder2.GetComponent<Rigidbody>();
            leverhinge.GetComponent<FixedJoint>().connectedBody = lever.GetComponent<Rigidbody>();
            lever.GetComponent<HingeJoint>().connectedBody = armhinge.GetComponent<Rigidbody>();
            armhinge.GetComponent<FixedJoint>().connectedBody = arm.GetComponent<Rigidbody>();
            arm.GetComponent<HingeJoint>().connectedBody = armhinge1.GetComponent<Rigidbody>();
            holder1.GetComponent<HingeJoint>().connectedBody = wheelhinge1.GetComponent<Rigidbody>();
            wheelhinge1.GetComponent<FixedJoint>().connectedBody = wheel1.GetComponent<Rigidbody>();
            wheel1.GetComponent<FixedJoint>().connectedBody = leverhinge1.GetComponent<Rigidbody>();
            lever1.GetComponents<HingeJoint>()[0].connectedBody = leverhinge1.GetComponent<Rigidbody>();
            lever1.GetComponents<HingeJoint>()[1].connectedBody = armhinge1.GetComponent<Rigidbody>();
            holder2.GetComponent<HingeJoint>().connectedBody = wheelhinge2.GetComponent<Rigidbody>();
            wheelhinge2.GetComponent<FixedJoint>().connectedBody = wheel2.GetComponent<Rigidbody>();
            wheel2.GetComponent<FixedJoint>().connectedBody = leverhinge.GetComponent<Rigidbody>();

            armhinge.GetComponent<armhingemanipulator2>().wheel1object = wheel1;
            armhinge.GetComponent<armhingemanipulator2>().armhinge1object = armhinge1;
            leverhinge1.GetComponent<leverhinge1manipulator2>().lever1object = lever1;
            wheel1.GetComponent<wheel1manipulator2>().lever1object = lever1;
            wheel1.GetComponent<wheel1manipulator2>().rotatingplatformobject = rotatingplatform;
            wheel2.GetComponent<wheel2manipulator2>().leverobject = lever;
            wheel2.GetComponent<wheel2manipulator2>().rotatingplatformobject = rotatingplatform;

            capture = new capture1();
            for (int i = 0; i < 4; i++)
                fingers.Add(new finger1());

            isinited = true;
        }

        {
            var b = chassis.GetComponent<chassismanipulator2>();
            b.capture = capture;
            b.fingers = fingers;
            b.drive.Proportional = config.RotatingplatformACSProportional;
            b.drive.Integral = config.RotatingplatformACSIntegral;
            b.drive.Differential = config.RotatingplatformACSDifferential;
            b.x = config.x;
            b.y = config.y;
            b.z = config.z;
            b.angle = config.angle;
            b.height = config.ChassisHeight;
            b.width = config.ChassisWidth;
            b.angle0 = config.RotatingplatformAngle0;
            b.angle1 = config.RotatingplatformAngle1;
        }

        {
            rotatingplatform.GetComponent<Rigidbody>().mass = config.RotatingplatformMass;
            var b = rotatingplatform.GetComponent<rotatingplatformmanipulator2>();
            b.diameter = config.RotatingplatformDiameter;
            b.width = config.RotatingplatformWidth / 2;/*div 2 for cylinder*/
        }

        {
            leverhinge.GetComponent<Rigidbody>().mass = config.LeverhingeMass;
            var b = leverhinge.GetComponent<leverhingemanipulator2>();
            b.diameter = config.LeverhingeDiameter;
        }

        {
            lever.GetComponent<Rigidbody>().mass = config.LeverMass;
            var b = lever.GetComponent<levermanipulator2>();
            b.drive.Proportional = config.ArmACSProportional;
            b.drive.Integral = config.ArmACSIntegral;
            b.drive.Differential = config.ArmACSDifferential;
            b.width = config.LeverWidth;
            b.length = config.LeverLength;
            b.angle0 = config.ArmAngle0;
            b.angle1 = config.ArmAngle1;
        }

        {
            armhinge.GetComponent<Rigidbody>().mass = config.ArmhingeMass;
            var b = armhinge.GetComponent<armhingemanipulator2>();
            b.diameter = config.ArmhingeDiameter;
            b.width = config.ArmhingeWidth / 2;/*div 2 for cylinder*/
        }

        {
            arm.GetComponent<Rigidbody>().mass = config.ArmMass;
            var b = arm.GetComponent<armmanipulator2>();
            b.width = config.ArmWidth;
            b.length = config.ArmLength;
        }

        {
            holder1.GetComponent<Rigidbody>().mass = config.HolderMass;
            var b = holder1.GetComponent<holder1manipulator2>();
            b.drive.Proportional = config.ArmACSProportional;
            b.drive.Integral = config.ArmACSIntegral;
            b.drive.Differential = config.ArmACSDifferential;
            b.width = config.HolderWidth;
            b.length = config.HolderLength;
            b.offset = config.HoldersDistance / 2;
            b.angle0 = config.ArmAngle0 + config.LeverAngle0;
            b.angle1 = config.ArmAngle1 + config.LeverAngle1;
        }

        {
            wheelhinge1.GetComponent<Rigidbody>().mass = config.WheelhingeMass;
            var b = wheelhinge1.GetComponent<wheelhinge1manipulator2>();
            b.diameter = config.LeverhingeDiameter;
            b.width = config.WheelhingeWidth / 2;/*div 2 for cylinder*/
        }

        {
            wheel1.GetComponent<Rigidbody>().mass = config.WheelMass;
            var b = wheel1.GetComponent<wheel1manipulator2>();
            b.diameter = config.WheelDiameter;
            b.width = config.WheelWidth / 2;/*div 2 for cylinder*/
            b.lever = config.WheelLever;
        }

        {
            leverhinge1.GetComponent<Rigidbody>().mass = config.LeverhingeMass;
            var b = leverhinge1.GetComponent<leverhinge1manipulator2>();
            b.diameter = config.LeverhingeDiameter;
        }

        {
            lever1.GetComponent<Rigidbody>().mass = config.LeverMass;
            var b = lever1.GetComponent<lever1manipulator2>();
            b.width = config.LeverWidth;
            b.length = config.LeverLength;
        }

        {
            armhinge1.GetComponent<Rigidbody>().mass = config.ArmhingeMass;
            var b = armhinge1.GetComponent<armhinge1manipulator2>();
            b.diameter = config.ArmhingeDiameter;
            b.width = config.ArmhingeWidth / 2;/*div 2 for cylinder*/
        }

        {
            holder2.GetComponent<Rigidbody>().mass = config.HolderMass;
            var b = holder2.GetComponent<holder2manipulator2>();
            b.drive.Proportional = config.LeverACSProportional;
            b.drive.Integral = config.LeverACSIntegral;
            b.drive.Differential = config.LeverACSDifferential;
            b.width = config.HolderWidth;
            b.length = config.HolderLength;
            b.offset = config.HoldersDistance / 2;
            b.angle0 = config.LeverAngle0;
            b.angle1 = config.LeverAngle1;
        }

        {
            wheelhinge2.GetComponent<Rigidbody>().mass = config.WheelhingeMass;
            var b = wheelhinge2.GetComponent<wheelhinge2manipulator2>();
            b.diameter = config.LeverhingeDiameter;
            b.width = config.WheelhingeWidth / 2;/*div 2 for cylinder*/
        }

        {
            wheel2.GetComponent<Rigidbody>().mass = config.WheelMass;
            var b = wheel2.GetComponent<wheel2manipulator2>();
            b.diameter = config.WheelDiameter;
            b.width = config.WheelWidth / 2;/*div 2 for cylinder*/
        }

        chassis.GetComponent<Rigidbody>().useGravity = false;
        rotatingplatform.GetComponent<Rigidbody>().useGravity = config.UseGravity;
        leverhinge.GetComponent<Rigidbody>().useGravity = config.UseGravity;
        lever.GetComponent<Rigidbody>().useGravity = config.UseGravity;
        armhinge.GetComponent<Rigidbody>().useGravity = config.UseGravity;
        arm.GetComponent<Rigidbody>().useGravity = config.UseGravity;
        holder1.GetComponent<Rigidbody>().useGravity = config.UseGravity;
        wheelhinge1.GetComponent<Rigidbody>().useGravity = config.UseGravity;
        wheel1.GetComponent<Rigidbody>().useGravity = config.UseGravity;
        leverhinge1.GetComponent<Rigidbody>().useGravity = config.UseGravity;
        lever1.GetComponent<Rigidbody>().useGravity = config.UseGravity;
        holder2.GetComponent<Rigidbody>().useGravity = config.UseGravity;
        wheelhinge2.GetComponent<Rigidbody>().useGravity = config.UseGravity;
        wheel2.GetComponent<Rigidbody>().useGravity = config.UseGravity;

        chassis.GetComponent<Rigidbody>().isKinematic = true;
        rotatingplatform.GetComponent<Rigidbody>().isKinematic = config.Kinematic;
        leverhinge.GetComponent<Rigidbody>().isKinematic = config.Kinematic;
        lever.GetComponent<Rigidbody>().isKinematic = config.Kinematic;
        armhinge.GetComponent<Rigidbody>().isKinematic = config.Kinematic;
        arm.GetComponent<Rigidbody>().isKinematic = config.Kinematic;
        holder1.GetComponent<Rigidbody>().isKinematic = config.Kinematic;
        wheelhinge1.GetComponent<Rigidbody>().isKinematic = config.Kinematic;
        wheel1.GetComponent<Rigidbody>().isKinematic = config.Kinematic;
        leverhinge1.GetComponent<Rigidbody>().isKinematic = config.Kinematic;
        lever1.GetComponent<Rigidbody>().isKinematic = config.Kinematic;
        holder2.GetComponent<Rigidbody>().isKinematic = config.Kinematic;
        wheelhinge2.GetComponent<Rigidbody>().isKinematic = config.Kinematic;
        wheel2.GetComponent<Rigidbody>().isKinematic = config.Kinematic;

        chassis.GetComponent<chassismanipulator2>().Init();

        {
            capture.config.UseGravity = config.UseGravity;
            capture.config.ConnectorMass = config.CaptureConnectorMass;
            capture.config.ConnectorDiameter = config.CaptureConnectorDiameter;
            capture.config.ConnectorWidth = config.CaptureConnectorWidth;
            capture.config.ArmKinematic = config.Kinematic;
            capture.config.ArmMass = config.CaptureArmMass;
            capture.config.ArmKinematicAngularVelocity = config.CaptureArmKinematicAngularVelocity;
            capture.config.ArmACSProportional = config.CaptureArmACSProportional;
            capture.config.ArmACSIntegral = config.CaptureArmACSIntegral;
            capture.config.ArmACSDifferential = config.CaptureArmACSDifferential;
            capture.config.ArmAngle0 = config.CaptureArmAngle0;
            capture.config.ArmAngle1 = config.CaptureArmAngle1;
            capture.config.ArmDiameter = config.CaptureArmDiameter;
            capture.config.ArmLength = config.CaptureArmLength;
            capture.config.ClamphingeMass = config.CaptureClamphingeMass;
            capture.config.ClamphingeDiameter = config.CaptureClamphingeDiameter;
            capture.config.ClamphingeWidth = config.CaptureClamphingeWidth;
            capture.config.ClampKinematic = config.Kinematic;
            capture.config.ClampMass = config.CaptureClampMass;
            capture.config.ClampKinematicAngularVelocity = config.CaptureClampKinematicAngularVelocity;
            capture.config.ClampACSProportional = config.CaptureClampACSProportional;
            capture.config.ClampACSIntegral = config.CaptureClampACSIntegral;
            capture.config.ClampACSDifferential = config.CaptureClampACSDifferential;
            capture.config.ClampAngle0 = config.CaptureClampAngle0;
            capture.config.ClampAngle1 = config.CaptureClampAngle1;
            capture.config.ClampDiameter = config.CaptureClampDiameter;
            capture.config.ClampWidth = config.CaptureClampWidth;
            capture.config.ClampConnectorKinematicAngularVelocity = config.CaptureClampConnectorKinematicAngularVelocity;
            capture.config.ClampConnectorACSProportional = config.CaptureClampConnectorACSProportional;
            capture.config.ClampConnectorACSIntegral = config.CaptureClampConnectorACSIntegral;
            capture.config.ClampConnectorACSDifferential = config.CaptureClampConnectorACSDifferential;
            capture.config.ClampConnectorAngle0 = config.CaptureClampConnectorAngle0;
            capture.config.ClampConnectorAngle1 = config.CaptureClampConnectorAngle1;
            capture.pivot.Object = arm;
            capture.pivot.position = Vector3.up * arm.transform.localScale.y / 2;
            capture.pivot.rotation = Quaternion.AngleAxis(180, Vector3.forward) * Quaternion.AngleAxis(90, Vector3.down);
            capture.Place();
        }

        {
            for (int i = 0; i < fingers.Count; i++)
            {
                fingers[i].config.Kinematic = config.FingerKinematic;
                fingers[i].config.ConnectorMass = config.FingerConnectorMass;
                fingers[i].config.ConnectorHeight = config.FingerConnectorHeight;
                fingers[i].config.RibHeight = config.FingerRibHeight;
                fingers[i].config.SectionMass = config.FingerSectionMass;
                fingers[i].config.SectionHeight = config.FingerSectionHeight;
                fingers[i].config.SectionWidth = config.FingerSectionWidth;
                fingers[i].config.SectionThick = config.FingerSectionThick;
                fingers[i].config.SectionKinematicAngularVelocity = config.FingerSectionKinematicAngularVelocity;
                fingers[i].config.SectionACSProportional = config.FingerSectionACSProportional;
                fingers[i].config.SectionACSIntegral = config.FingerSectionACSIntegral;
                fingers[i].config.SectionACSDifferential = config.FingerSectionACSDifferential;
                fingers[i].config.SectionAngle0 = config.FingerSectionAngle0;
                fingers[i].config.SectionAngle1 = config.FingerSectionAngle1;
                fingers[i].config.SectionCount = config.FingerSectionCount;

                GameObject clamp = capture.GetClamp();

                fingers[i].pivot.Object = clamp;

                switch (i)
                {
                    case 0:
                        fingers[i].pivot.position = Vector3.down * clamp.transform.localScale.y / 2 * clampcapture1.CylinderFullHeight +
                            Vector3.left * (config.CaptureClampDiameter / 2 - fingers[i].config.SectionThick / 2);
                        fingers[i].pivot.rotation = Quaternion.identity;
                        break;

                    case 1:
                        fingers[i].pivot.position = Vector3.down * clamp.transform.localScale.y / 2 * clampcapture1.CylinderFullHeight +
                            Vector3.right * (config.CaptureClampDiameter / 2 - fingers[i].config.SectionThick / 2);
                        fingers[i].pivot.rotation = Quaternion.AngleAxis(180, Vector3.up);
                        break;

                    case 2:
                        fingers[i].pivot.position = Vector3.down * clamp.transform.localScale.y / 2 * clampcapture1.CylinderFullHeight +
                            Vector3.forward * (config.CaptureClampDiameter / 2 - fingers[i].config.SectionThick / 2);
                        fingers[i].pivot.rotation = Quaternion.AngleAxis(90, Vector3.up);
                        break;

                    case 3:
                        fingers[i].pivot.position = Vector3.down * clamp.transform.localScale.y / 2 * clampcapture1.CylinderFullHeight +
                            Vector3.back * (config.CaptureClampDiameter / 2 - fingers[i].config.SectionThick / 2);
                        fingers[i].pivot.rotation = Quaternion.AngleAxis(-90, Vector3.up);
                        break;
                }

                fingers[i].oncaught = OnCaught;

                fingers[i].Place();
            }
        }

        if (config.Kinematic)
        {
            kinematicanglerange0.SetLimits(config.RotatingplatformAngle0, config.RotatingplatformAngle1);
            kinematicanglerange1.SetLimits(config.LeverAngle0, config.LeverAngle1);
            kinematicanglerange2.SetLimits(-90 + config.ArmAngle0, -90 + config.ArmAngle1);
            kinematicanglerange0.SetTarget(config.RotatingplatformAngle0);
            kinematicanglerange1.SetTarget(config.LeverAngle0);
            kinematicanglerange2.SetTarget(-90 + config.ArmAngle0);
            chassis.GetComponent<chassismanipulator2>().rotatingplatformkinematicangularvelocity = config.KinematicAngularVelocity != 0 ? config.KinematicAngularVelocity : config.RotatingplatformKinematicAngularVelocity;
            chassis.GetComponent<chassismanipulator2>().leverkinematicangularvelocity = config.KinematicAngularVelocity != 0 ? config.KinematicAngularVelocity : config.LeverKinematicAngularVelocity;
            chassis.GetComponent<chassismanipulator2>().armkinematicangularvelocity = config.KinematicAngularVelocity != 0 ? config.KinematicAngularVelocity : config.ArmKinematicAngularVelocity;
            chassis.GetComponent<chassismanipulator2>().Kinematic(kinematicanglerange0.GetTarget(), kinematicanglerange1.GetTarget(), kinematicanglerange2.GetTarget());
        }

        capture.SetPos(0, GetPos1() + GetPos2() - 180, GetPos5());//схват вниз
    }

    public override void Remove()
    {
        MonoBehaviour.Destroy(chassis);
        MonoBehaviour.Destroy(rotatingplatform);
        MonoBehaviour.Destroy(leverhinge);
        MonoBehaviour.Destroy(lever);
        MonoBehaviour.Destroy(armhinge);
        MonoBehaviour.Destroy(arm);
        MonoBehaviour.Destroy(holder1);
        MonoBehaviour.Destroy(wheelhinge1);
        MonoBehaviour.Destroy(wheel1);
        MonoBehaviour.Destroy(leverhinge1);
        MonoBehaviour.Destroy(lever1);
        MonoBehaviour.Destroy(armhinge1);
        MonoBehaviour.Destroy(holder2);
        MonoBehaviour.Destroy(wheelhinge2);
        MonoBehaviour.Destroy(wheel2);

        capture.Remove();
        for (int i = 0; i < fingers.Count; i++)
            fingers[i].Remove();

        isinited = false;
        chassis = null;
        rotatingplatform = null;
        leverhinge = null;
        lever = null;
        armhinge = null;
        arm = null;
        holder1 = null;
        wheelhinge1 = null;
        wheel1 = null;
        leverhinge1 = null;
        lever1 = null;
        armhinge1 = null;
        holder2 = null;
        wheelhinge2 = null;
        wheel2 = null;

        capture = null;
        fingers.Clear();
    }

    public void SetPos(float angle0, float angle1, float angle2, float angle3, float angle4, float angle5, bool gripped)
    {
        if (config.Kinematic)
        {
            float deltaangle0 = kinematicanglerange0.GetTarget() - chassis.GetComponent<chassismanipulator2>().GetKinematicRestDeltaAngle0();
            float deltaangle1 = kinematicanglerange1.GetTarget() - chassis.GetComponent<chassismanipulator2>().GetKinematicRestDeltaAngle1();
            float deltaangle2 = kinematicanglerange2.GetTarget() - chassis.GetComponent<chassismanipulator2>().GetKinematicRestDeltaAngle2();

            kinematicanglerange0.SetTarget(angle0);
            kinematicanglerange1.SetTarget(angle1);
            kinematicanglerange2.SetTarget(-90 + angle2);

            deltaangle0 = kinematicanglerange0.Delta(deltaangle0, kinematicanglerange0.GetTarget());
            deltaangle1 = kinematicanglerange0.Delta(deltaangle1, kinematicanglerange1.GetTarget());
            deltaangle2 = kinematicanglerange0.Delta(deltaangle2, kinematicanglerange2.GetTarget());

            chassis.GetComponent<chassismanipulator2>().SetKinematicTargetDeltaAngles(deltaangle0, deltaangle1, deltaangle2);
        }
        else
        {
            DriveJoint drive0 = chassis.GetComponent<chassismanipulator2>().drive;
            DriveJoint drive1 = lever.GetComponent<levermanipulator2>().drive;
            DriveJoint drive2 = holder1.GetComponent<holder1manipulator2>().drive;
            DriveJoint drive3 = holder2.GetComponent<holder2manipulator2>().drive;

            drive0.AngleRange.SetTarget(angle0);
            drive3.AngleRange.SetTarget(angle1);
            drive2.AngleRange.SetTarget(drive3.AngleRange.GetTarget() + drive1.AngleRange.GetTarget());
            drive1.AngleRange.SetTarget(-90 + angle2);
            drive2.AngleRange.SetTarget(drive3.AngleRange.GetTarget() + drive1.AngleRange.GetTarget());
        }

        if (config.FingerDown)
        {
            capture.SetPos(0, GetPos1() + GetPos2() - 180, angle5);
        }
        else
        {
            capture.SetPos(angle3, angle4, angle5);
        }

        SetGripper(gripped);
    }

    public float GetPos0()
    {
        if (config.Kinematic)
        {
            return kinematicanglerange0.GetTarget();
        }
        else
        {
            return chassis.GetComponent<chassismanipulator2>().drive.AngleRange.GetTarget();
        }
    }

    public float GetPos1()
    {
        if (config.Kinematic)
        {
            return kinematicanglerange1.GetTarget();
        }
        else
        {
            return holder2.GetComponent<holder2manipulator2>().drive.AngleRange.GetTarget();
        }
    }

    public float GetPos2()
    {
        float angle;

        if (config.Kinematic)
        {
            angle = kinematicanglerange2.GetTarget() + 90;
        }
        else
        {
            angle = lever.GetComponent<levermanipulator2>().drive.AngleRange.GetTarget() + 90;
        }

        angle = angle % 360;
        if (angle < 0)
            angle += 360;

        return angle;
    }

    public float GetPos3()
    {
        return capture.GetPos0();
    }

    public float GetPos4()
    {
        return capture.GetPos1();
    }

    public float GetPos5()
    {
        return capture.GetPos2();
    }

    public void SetGripper(bool gripped)
    {
        for (int i = 0; i < fingers.Count; i++)
        {
            fingers[i].Clench(gripped);
        }

        if (!gripped && thing != null)
        {
            chassis.GetComponent<chassismanipulator2>().thing = null;
            thing.joint.Break();
            thing.gameObject.GetComponent<Rigidbody>().isKinematic = false;
            thing.gameObject.GetComponent<Rigidbody>().useGravity = true;
            thing.StartTimer();
            thing = null;
        }
    }

    public bool IsGripped()
    {
        // проверка, что зацепили все пальцы
        bool gripped = true;
        for (int i = 0; i < fingers.Count; i++)
            gripped &= fingers[i].IsCaught();

        // проверка, что зацепил хотя бы один палец
        //bool gripped = false;
        //for (int i = 0; i < fingers.Count; i++)
        //    gripped |= fingers[i].IsCaught();

        return gripped;
    }

    private void OnCaught(thing thing_)
    {
        if (!IsGripped() || thing != null)
            return;

        thing = thing_;
        thing.joint.Config(capture.GetClamp(), thing.gameObject, true, JointPhysics.Fixed);
        chassis.GetComponent<chassismanipulator2>().thing = thing;
    }
}
