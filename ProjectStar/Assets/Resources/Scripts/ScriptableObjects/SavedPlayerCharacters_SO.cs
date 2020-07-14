using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SavedCharacters", menuName = "ScriptableLists/Type: SavedCharacters")]
public class SavedPlayerCharacters_SO : ScriptableObject
{
    public List<CharacterInput> listOfAllSavedCharacters = new List<CharacterInput>();
    public List<CharacterInput> listOfAllMissionSavedCharacters = new List<CharacterInput>();
    //public List<CharacterInput> listOfWHATEVER = new List<CharacterInput>();

    public void AddSavedCharacter(CharacterInput adChar)
    {
        listOfAllSavedCharacters.Add(adChar);
    }

    public void RemoveSavedCharacter(CharacterInput adChar)
    {
        listOfAllSavedCharacters.Remove(adChar);
    }

    public List<CharacterInput> GetSavedList()
    {
        return listOfAllSavedCharacters;
    }

    //#################################################################################################

    public void AddMissionCharacter(CharacterInput adChar)
    {
        listOfAllMissionSavedCharacters.Add(adChar);
    }

    public void RemoveMissionCharacter(CharacterInput adChar)
    {
        listOfAllMissionSavedCharacters.Remove(adChar);
    }

    public List<CharacterInput> GetMissionList()
    {
        return listOfAllMissionSavedCharacters;
    }

    private void OnDisable()
    {
        listOfAllSavedCharacters.Clear();
        listOfAllMissionSavedCharacters.Clear();
    }




}
