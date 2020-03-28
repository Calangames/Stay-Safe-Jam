using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeanTweenManager : MonoBehaviour
{
    public GameObject Mesh1;
    public GameObject Mesh2;
    public GameObject Mesh3;

    // Start is called before the first frame update
    void Start()
    {
        LeanTween.moveX(Mesh1, Mesh1.transform.position.x + 5f, 1f).setEase(LeanTweenType.easeInOutCirc).setLoopPingPong();
        
        LeanTween.rotateAround(Mesh2, Vector3.up, -360, 80f).setLoopClamp();
        LeanTween.moveX(Mesh2, Mesh2.transform.position.x + 1f, 3f).setEase(LeanTweenType.easeInOutCirc).setLoopPingPong();

        LeanTween.rotateAround(Mesh3, Vector3.up, -360, 80f).setLoopClamp();
        LeanTween.moveY(Mesh3, Mesh3.transform.position.y + 5f, 3f).setEase(LeanTweenType.easeInOutCirc).setLoopPingPong();
        LeanTween.moveX(Mesh3, Mesh3.transform.position.x + 5f, 3f).setEase(LeanTweenType.easeInOutCirc).setLoopPingPong();
        LeanTween.scale(Mesh3, new Vector3(1.3922f, 1.273f, 1.07517507f), 1f).setLoopPingPong();

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
