using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandScript : MonoBehaviour
{

    [Header("Settings")]
    [Range(0.1f, 10f)]
    [SerializeField] float Sensitivity = 5f;

    [Header("Keybinds")]
    [SerializeField] KeyCode GrabKey = KeyCode.Mouse0;

    [SerializeField] ArmControllerCross parent;

    [SerializeField] float angle = 0f;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        angle += -Input.mouseScrollDelta.y * Sensitivity;

        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(angle, 0, 0), 0.1f);
    }
}
