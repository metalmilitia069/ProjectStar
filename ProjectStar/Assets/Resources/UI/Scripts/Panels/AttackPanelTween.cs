using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackPanelTween : MonoBehaviour
{
    [Range(0, 1)]
    public float TweenSpeed;
    public LeanTweenType TweenType;


    public Canvas Canvas;
    private RectTransform rect;
    private float outPos;
    private float inPos;
    private bool isTweendIn = true;
    private bool TweenComplete = true;

    // Start is called before the first frame update
    void Start()
    {
        InitialSetup();
        transform.LeanMoveY(outPos, TweenSpeed).setEase(TweenType);
    }

    // Update is called once per frame
    void Update()
    {
        //InitialSetup();
    }

    public void InitialSetup()
    {
        //Canvas = GetComponentInParent<Canvas>();

        rect = GetComponent<RectTransform>();
        
        outPos = (RectTransformUtility.PixelAdjustRect(GetComponent<RectTransform>(), Canvas).height * -1) * (Canvas.GetComponent<RectTransform>().localScale.y);        
        inPos = 4 * (Canvas.GetComponent<RectTransform>().localScale.y);
        
    }

    public void ToggleTween()
    {
        //if (!TweenComplete)
        //{
        //    return;
        //}

        TweenComplete = false;
        transform.LeanMoveY(isTweendIn ? inPos : outPos, TweenSpeed).setEase(TweenType).setOnComplete(() => TweenComplete = true);
        
        isTweendIn = !isTweendIn;
    }

}
