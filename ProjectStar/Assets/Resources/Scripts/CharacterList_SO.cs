using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "PlayerCharacterList", menuName = "ScriptableLists/Type: Player")]
public class CharacterList_SO : ScriptableObject
{
    private List<CharacterInput> listOfAllCharacters;

    public void AddCharacter(CharacterInput adChar)
    {
        listOfAllCharacters.Add(adChar);
    }

    public void RemoveCharacter(CharacterInput adChar)
    {
        listOfAllCharacters.Remove(adChar);
    }

    public List<CharacterInput> GetList()
    {
        return listOfAllCharacters;
    }
}
