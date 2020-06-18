using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "PlayerCharacterList", menuName = "ScriptableLists/Type: Player")]
public class CharacterList_SO : ScriptableObject
{
    private List<GroupableEntities> listOfAllCharacters;

    public void AddCharacter(GroupableEntities adChar)
    {
        listOfAllCharacters.Add(adChar);
    }

    public void RemoveCharacter(GroupableEntities adChar)
    {
        listOfAllCharacters.Remove(adChar);
    }

    public List<GroupableEntities> GetList()
    {
        return listOfAllCharacters;
    }

    public void SortCharactersById()
    {

    }
}
