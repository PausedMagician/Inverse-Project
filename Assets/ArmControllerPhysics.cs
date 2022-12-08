using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmControllerPhysics : ArmController
{

    void Update()
    {

        InputHandler();

        float c_len = Vector3.Distance(root_base.position, target.position);
        float joint_angle = Mathf.Acos((Mathf.Pow(l1, 2) + Mathf.Pow(l2, 2) - Mathf.Pow(c_len, 2))/(2*l1*l2));
        float other_angle1 = Mathf.Acos((Mathf.Pow(c_len, 2) + Mathf.Pow(l2, 2) - Mathf.Pow(l1, 2))/(2*c_len*l2));
        float other_angle2 = Mathf.Acos((Mathf.Pow(l1, 2) + Mathf.Pow(c_len, 2) - Mathf.Pow(l2, 2))/(2*l1*c_len));
        joint_angle = Mathf.Rad2Deg * joint_angle;
        other_angle1 = Mathf.Rad2Deg * other_angle1;
        other_angle2 = Mathf.Rad2Deg * other_angle2;

        float extra_angle = Vector3.Angle(transform.forward, new Vector3(target.position.x - root_base.position.x, target.position.y - root_base.position.y, target.position.z - root_base.position.z));

        if (target.position.y < root_base.position.y) {
            extra_angle *= -1;
            other_angle1 *= -1;
            other_angle2 *= -1;
        }

        float target_lower;
        float target_upper;

        if (Vector3.Distance(root_base.position, target.position) < l1+l2) {
            target_lower = 90 - other_angle2 - extra_angle;
            target_upper = joint_angle - 180f;
            if(!(target.position.y < root_base.position.y)) {target_upper *= -1f;}
        } else {
            target_lower = 90 - extra_angle;
            target_upper = 180 - 180;
        }

        Debug.Log(target_lower);
        Debug.Log(target_upper);

        HingeJoint lowerHinge = lower_arm.gameObject.GetComponent<HingeJoint>();
        JointSpring lowerSpring = lowerHinge.spring;
        lowerSpring.targetPosition = target_lower;
        lowerHinge.spring = lowerSpring;

        HingeJoint upperHinge = upper_arm.gameObject.GetComponent<HingeJoint>();
        JointSpring upperSpring = lowerHinge.spring;
        upperSpring.targetPosition = target_upper;
        upperHinge.spring = upperSpring;


    }

    private void OnDrawGizmos() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(target.position, 0.5f);
    }
}
