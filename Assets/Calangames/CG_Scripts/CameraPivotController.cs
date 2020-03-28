using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPivotController : MonoBehaviour
{
    public GameObject player;

    void LateUpdate()
    {
        transform.position = player.transform.position;
    }
}
