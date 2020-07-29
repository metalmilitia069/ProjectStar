using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameManager : MonoBehaviour
{
    [Header("INSERT SAVED PLAYER CHARACTERS SO:")]
    public SavedPlayerCharacters_SO savedPlayerCharacters_SO;
    [Header("INSERT LIST OF ALL CHARACTERS ON MAP SO:")]
    public CharacterList_SO ListOfAllcharacter_SO;
    [Header("INSERT SAVED ENEMIES SO:")]
    public SavedEnemies_SO savedEnemies_SO;
    [Header("INSERT LIST OF ALL ENEMIES ON MAP SO:")]
    public EnemyList_SO ListOfAllEnemies_SO;


    public TileList_SO TileList_SO;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //foreach (var item in TileList_SO.GetList())
        //{
        //    item.UpdateTileState();
        //}
    }
}
