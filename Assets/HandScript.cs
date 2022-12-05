using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandScript : MonoBehaviour
{

    

    [Header("Keybinds")]
    [SerializeField] KeyCode GrabKey = KeyCode.Mouse0;

    [SerializeField] GameObject parent;

    public bool usingHinge = false;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.rotation = Find();
        HingeJoint hingeJoint = gameObject.GetComponent<HingeJoint>();
        if(hingeJoint != null) {
            JointSpring springJoint = hingeJoint.spring;
            springJoint.targetPosition = Find().x;
            hingeJoint.spring = springJoint;
            usingHinge = true;
        } else {
            transform.rotation = Find();
            usingHinge = false;
        }
    }

    Quaternion Find() {
        Quaternion toReturn;
        if((toReturn = parent.GetComponent<ArmController>().target.rotation) != null) {return toReturn;}
        if((toReturn = parent.GetComponent<ArmControllerCross>().target.rotation) != null) {return toReturn;}
        if((toReturn = parent.GetComponent<ArmControllerCrossPhysics>().target.rotation) != null) {return toReturn;}
        return toReturn;
    }
}
