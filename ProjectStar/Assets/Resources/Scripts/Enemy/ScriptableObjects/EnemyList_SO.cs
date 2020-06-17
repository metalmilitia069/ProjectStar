using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AllEnemiesList", menuName = "ScriptableLists/Type: Enemy")]
public class EnemyList_SO : ScriptableObject
{
    private List<EnemyInput> listOfAllEnemies;

    public void AddCharacter(EnemyInput adChar)
    {
        listOfAllEnemies.Add(adChar);
    }

    public void RemoveCharacter(EnemyInput adChar)
    {
        listOfAllEnemies.Remove(adChar);
    }

    public List<EnemyInput> GetList()
    {
        return listOfAllEnemies;
    }
}
