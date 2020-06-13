using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "PlayerCharacterList", menuName = "ScriptableLists/Type: Player")]
public class CharacterList_SO : ScriptableObject
{
    private List<CharacterInput> listOfAllCharacters;

    public void AddTile(CharacterInput adTile)
    {
        listOfAllCharacters.Add(adTile);
    }

    public void RemoveTile(CharacterInput adTile)
    {
        listOfAllCharacters.Remove(adTile);
    }

    public List<CharacterInput> GetList()
    {
        return listOfAllCharacters;
    }
}
