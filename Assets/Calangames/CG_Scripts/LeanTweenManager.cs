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
        LeanTween.moveX(Mesh1, Mesh1.transform.position.x + 20f, 1f).setLoopPingPong();
        
        LeanTween.rotateAround(Mesh2, Vector3.up, -360, 8f).setLoopClamp();
        LeanTween.moveX(Mesh2, Mesh2.transform.position.x + 5f, 1f).setLoopPingPong();

        LeanTween.rotateAround(Mesh3, Vector3.up, -360, 25f).setLoopClamp();
        LeanTween.moveY(Mesh3, Mesh3.transform.position.y - 10f, 1f).setEase(LeanTweenType.easeInOutCirc).setLoopPingPong();
        LeanTween.moveX(Mesh3, Mesh3.transform.position.x + 5f, 1f).setEase(LeanTweenType.easeInOutCirc).setLoopPingPong();
        LeanTween.scale(Mesh3, new Vector3(24f, 4f, 47f), 0.5f).setLoopPingPong();

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
