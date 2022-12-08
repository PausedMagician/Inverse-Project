using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseTarget : MonoBehaviour
{

    [Header("Clamps")]
    [SerializeField] float y_clamp = 0f; 

    void Update()
    {
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        worldPosition.x = 0;
        if (worldPosition.y < y_clamp)
        {
            worldPosition.y = y_clamp;
        }
        transform.position = worldPosition;
    }
}
