using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPivotController : MonoBehaviour
{
    void LateUpdate()
    {
        transform.position = Crowd.instance.transform.position;
    }
}
