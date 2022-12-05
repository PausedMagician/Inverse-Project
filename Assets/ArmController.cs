using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmController : MonoBehaviour
{
    public Transform lower_arm, upper_arm, end_point, target, root_base;

    public float RotationSpeed = 1f;

    Ray ray;
    Vector3 dir;


    public float l1, l2;

    // Start is called before the first frame update
    void Start()
    {
        l1 = Vector3.Distance(lower_arm.position, upper_arm.position);
        l1 = upper_arm.position.y - lower_arm.position.y;
        l2 = Vector3.Distance(upper_arm.position, end_point.position);
        l2 = end_point.position.y - upper_arm.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        float c_len = Vector3.Distance(root_base.position, target.position);
        float joint_angle = Mathf.Acos((Mathf.Pow(l1, 2) + Mathf.Pow(l2, 2) - Mathf.Pow(c_len, 2))/(2*l1*l2));
        float other_angle1 = Mathf.Acos((Mathf.Pow(c_len, 2) + Mathf.Pow(l2, 2) - Mathf.Pow(l1, 2))/(2*c_len*l2));
        float other_angle2 = Mathf.Acos((Mathf.Pow(l1, 2) + Mathf.Pow(c_len, 2) - Mathf.Pow(l2, 2))/(2*l1*c_len));
        joint_angle = Mathf.Rad2Deg * joint_angle;
        other_angle1 = Mathf.Rad2Deg * other_angle1;
        other_angle2 = Mathf.Rad2Deg * other_angle2;
        //float other_angle = ((180-joint_angle)/2);

        float extra_angle = Vector3.Angle(transform.forward, new Vector3(target.position.x - root_base.position.x, target.position.y - root_base.position.y, target.position.z - root_base.position.z));

        if (target.position.y < root_base.position.y) {
            extra_angle *= -1;
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

        //Debug.Log($"joint: {joint_angle}Deg \n arm: {other_angle}Deg");
        // joint_angle = Mathf.Deg2Rad * joint_angle;
        // other_angle = Mathf.Deg2Rad * other_angle;
        // Debug.Log($"joint: {joint_angle}Rad \n arm: {other_angle}Rad");

        lower_arm.localRotation = target_lower;
        upper_arm.localRotation = target_upper;

    }

    private void OnDrawGizmos() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(target.position, 0.5f);
    }
}
