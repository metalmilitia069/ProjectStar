using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CrossSignGizmo : MonoBehaviour
{
    public Image part02;
    public Image part03;
    public Image part04;
    public Image part05;

    [Range(0, 1)]
    public float TweenSpeed;
    public LeanTweenType TweenType;

    private bool isTweendIn = true;
    private bool TweenComplete = true;

    private CanvasGroup canvasGroup2;
    private CanvasGroup canvasGroup3;
    private CanvasGroup canvasGroup4;
    private CanvasGroup canvasGroup5;

    // Start is called before the first frame update
    void Start()
    {
        canvasGroup2 = part02.GetComponent<CanvasGroup>();
        canvasGroup3 = part03.GetComponent<CanvasGroup>();
        canvasGroup4 = part04.GetComponent<CanvasGroup>();
        canvasGroup5 = part05.GetComponent<CanvasGroup>();

        AnimateGizmo();
    }

    public void AnimateGizmo()
    {
        LeanTween.alphaCanvas(canvasGroup2, 0, TweenSpeed).setEase(TweenType).setLoopPingPong();
        
        LeanTween.alphaCanvas(canvasGroup3, 0, TweenSpeed).setEase(TweenType).setLoopPingPong();
        LeanTween.alphaCanvas(canvasGroup4, 0, TweenSpeed + 1).setEase(TweenType).setLoopPingPong();
        LeanTween.alphaCanvas(canvasGroup5, 0, TweenSpeed + 2).setEase(TweenType).setLoopPingPong();

        //LeanTween.moveY(cu3, -3, 2).setEaseInOutCubic().setLoopPingPong();
    }



    public void ToggleTween()
    {
        if (!TweenComplete)
        {
            return;
        }

        TweenComplete = false;
        LeanTween.alphaCanvas(canvasGroup2, isTweendIn ? 0 : 1, TweenSpeed).setOnComplete(() => TweenComplete = true);
        

        isTweendIn = !isTweendIn;
    }

    // Update is called once per frame
    void Update()
    {
        //LeanTween.rotateY
        this.transform.LookAt(Camera.main.transform);
    }
}
