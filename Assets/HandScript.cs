using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandScript : MonoBehaviour
{
    [SerializeField] GameObject parent;
    void FixedUpdate()
    {
        transform.rotation = Find();
    }
    Quaternion Find() {
        Quaternion toReturn;
        if((toReturn = parent.GetComponent<ArmController>().target.rotation) != null) {return toReturn;}
        if((toReturn = parent.GetComponent<ArmControllerPhysics>().target.rotation) != null) {return toReturn;}
        return toReturn;
    }
}
