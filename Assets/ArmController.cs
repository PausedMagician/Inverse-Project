using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmController : MonoBehaviour
{
    [Header("Parts")]
    public Transform lower_arm;
    public Transform upper_arm;
    public Transform end_point;
    public Transform target;
    public Transform root_base;

    [Header("Variables")]
    public float l1;
    public float l2;
    public float handAngle = 0f;
    public float scrollSensitivity = 5f;

    [Header("Speed Settings")]
    public bool slerp = true;
    public float armSpeed = 0.25f;

    void Start()
    {
        l1 = Vector3.Distance(lower_arm.position, upper_arm.position);
        l1 = upper_arm.position.y - lower_arm.position.y;
        l2 = Vector3.Distance(upper_arm.position, end_point.position);
        l2 = end_point.position.y - upper_arm.position.y;
    }

    void Update()
    {
        InputHandler();

        float[] angleList = GetFloats();
        float joint_angle = Mathf.Rad2Deg * angleList[1];
        float other_angle1 = Mathf.Rad2Deg * angleList[2];
        float other_angle2 = Mathf.Rad2Deg * angleList[3];

        MoveArm(joint_angle, other_angle1, other_angle2);
    }

    public void MoveArm(float joint_angle, float other_angle1, float other_angle2) {
        float extra_angle = Vector3.Angle(transform.forward, new Vector3(target.position.x - root_base.position.x, target.position.y - root_base.position.y, target.position.z - root_base.position.z));

        if (target.position.z < root_base.position.z) {
            joint_angle *= -1;
            other_angle1 *= -1;
            other_angle2 *= -1;
        }

        Quaternion target_lower;
        Quaternion target_upper;

        if (Vector3.Distance(root_base.position, target.position) < l1+l2) {
            target_lower = Quaternion.Euler(90 - other_angle2 - extra_angle, 0, 0);
            target_upper = Quaternion.Euler(180 - joint_angle, 0, 0);
        } else {
            target_lower = Quaternion.Euler(90 - extra_angle, 0, 0);
            target_upper = Quaternion.Euler(180 - 180, 0, 0);
        }

        if (!slerp) {
            lower_arm.localRotation = target_lower;
            upper_arm.localRotation = target_upper;
        } else {
            lower_arm.localRotation = Quaternion.Slerp(lower_arm.localRotation, target_lower, armSpeed * Time.deltaTime * 20);
            upper_arm.localRotation = Quaternion.Lerp(upper_arm.localRotation, target_upper, armSpeed * Time.deltaTime * 20);
        }
    }

    public float[] GetFloats() {
        float c_len = Vector3.Distance(root_base.position, target.position);
        float joint_angle = Mathf.Acos((Mathf.Pow(l1, 2) + Mathf.Pow(l2, 2) - Mathf.Pow(c_len, 2))/(2*l1*l2));
        float other_angle1 = Mathf.Acos((Mathf.Pow(c_len, 2) + Mathf.Pow(l2, 2) - Mathf.Pow(l1, 2))/(2*c_len*l2));
        float other_angle2 = Mathf.Acos((Mathf.Pow(l1, 2) + Mathf.Pow(c_len, 2) - Mathf.Pow(l2, 2))/(2*l1*c_len));
        float[] list = {c_len, joint_angle, other_angle1, other_angle2};
        return list;
    }

    public void InputHandler() {
        handAngle += -Input.mouseScrollDelta.y * scrollSensitivity;
        target.transform.rotation = Quaternion.Lerp(target.transform.rotation, Quaternion.Euler(handAngle, 0, 0), 0.1f);
    }

    private void OnDrawGizmos() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(target.position, 0.5f);
    }
}
