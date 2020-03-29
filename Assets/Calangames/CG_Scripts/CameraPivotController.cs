using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPivotController : MonoBehaviour
{
    private void Start()
    {
        AudioManager.instance.Play("Level Song");
    }

    void LateUpdate()
    {
        transform.position = Crowd.instance.transform.position;
    }
}
