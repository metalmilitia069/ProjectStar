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

    [Header("BUTTONS RELATED TO THIS PANEL")]
    public AttackModeButton attackModeButton;

    public bool isAttackModeOn = false;

    // Start is called before the first frame update
    void Start()
    {
        InitialSetup();
        transform.LeanMoveY(outPos, TweenSpeed).setEase(TweenType);
    }

    // Update is called once per frame
    void Update()
    {        
        if (attackModeButton.charInput != null && attackModeButton.charInput.characterTurnVariables.actionPoints < 1)
        {
            if (!attackModeButton.charInput.GetComponent<CharacterInput>().characterMoveVariables._isCombatMode)
            {   
                OutPosTween();
                isAttackModeOn = false;
                attackModeButton.charInput = null;            
            }
        }
        else if (attackModeButton.charInput != null && attackModeButton.charInput.characterTurnVariables.actionPoints > 0)
        {
            if (attackModeButton.charInput.GetComponent<CharacterInput>().characterMoveVariables._isCombatMode)
            {
                isAttackModeOn = true;
            }
            else
            {
                //attackModeButton.charInput.UnMarkEnemy();
                isAttackModeOn = false;
            }

            //if (!isAttackModeOn)
            //{
            //    Debug.Log("CUUUUUUUUUUUU");
            //    attackModeButton.charInput.UnMarkEnemy();
            //}
        }

        

    }

    public void InitialSetup()
    {
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
        //AttackModeButton.charInput = null;
    }

    public void OutPosTween()
    {
        transform.LeanMoveY(outPos, TweenSpeed).setEase(TweenType);
        isTweendIn = true;
    }
}
