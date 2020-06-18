using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AllEnemiesList", menuName = "ScriptableLists/Type: Enemy")]
public class EnemyList_SO : ScriptableObject
{
    private List<GroupableEntities> listOfAllEnemies;

    public void AddCharacter(GroupableEntities adChar)
    {
        listOfAllEnemies.Add(adChar);
    }

    public void RemoveCharacter(GroupableEntities adChar)
    {
        listOfAllEnemies.Remove(adChar);
    }

    public List<GroupableEntities> GetList()
    {
        return listOfAllEnemies;
    }
}
