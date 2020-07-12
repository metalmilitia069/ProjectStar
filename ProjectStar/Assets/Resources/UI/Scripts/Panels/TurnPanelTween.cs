using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnPanelTween : MonoBehaviour
{
    [Range(0, 1)]
    public float TweenSpeed;
    public LeanTweenType TweenType;

    private bool isTweendIn = true;
    private bool TweenComplete = true;

    public void ToggleTween()
    {
        //if (!TweenComplete)
        //{
        //    return;
        //}

        TweenComplete = false;
        transform.LeanScale(isTweendIn ? Vector3.zero : new Vector3(0.8f, 0.8f, 0.8f), TweenSpeed).setEase(TweenType).setOnComplete(() => TweenComplete = true);
        isTweendIn = !isTweendIn;
    }

    // Start is called before the first frame update
    void Start()
    {
        ToggleTween();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
