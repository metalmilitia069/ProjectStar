using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MouseToolTIpPanel : MonoBehaviour
{
    [Header("INSERT UI MANAGER SO :")]
    public UIManager_SO uiManager;

    [Range(0, 1)]
    public float TweenSpeed;
    public LeanTweenType TweenType;

    public bool isTweendIn = true;
    private bool TweenComplete = true;
    private bool TweenComplete1 = true;

    [Header("MOUSE TOOL TIP PANEL TEXTS :")]
    public Text tip1;
    public Text tip2;
    public Text tip3;
    public Text tip4;
    public Text tip5;
    public Text tip6;
    public Text tip7;

    // Start is called before the first frame update
    void Awake()
    {
        uiManager.mouseToolTIpPanel = this;
        TweenTipTexts();
        ToggleTween();
    }

    // Update is called once per frame
    void Update()
    {
        //TweenTipTexts();
    }   

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

    public void TweenOut()
    {
        transform.LeanScale(Vector3.zero, TweenSpeed).setEase(TweenType);
        isTweendIn = false;
    }

    public void TweenTipTexts()
    {
        
        LeanTween.colorText(tip1.gameObject.GetComponent<RectTransform>(), Color.black, 1f).setDelay(1f).setLoopPingPong();
        LeanTween.colorText(tip2.gameObject.GetComponent<RectTransform>(), Color.black, 1f).setDelay(1f).setLoopPingPong();
        LeanTween.colorText(tip3.gameObject.GetComponent<RectTransform>(), Color.black, 1f).setDelay(1f).setLoopPingPong();
        LeanTween.colorText(tip4.gameObject.GetComponent<RectTransform>(), Color.black, 1f).setDelay(1f).setLoopPingPong();
        LeanTween.colorText(tip5.gameObject.GetComponent<RectTransform>(), Color.black, 1f).setDelay(1f).setLoopPingPong();
        LeanTween.colorText(tip6.gameObject.GetComponent<RectTransform>(), Color.black, 1f).setDelay(1f).setLoopPingPong();
        LeanTween.colorText(tip7.gameObject.GetComponent<RectTransform>(), Color.black, 1f).setDelay(1f).setLoopPingPong();
        //transform.LeanColor(Color.yellow, Color.black)
    }
}
