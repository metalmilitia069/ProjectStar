using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SavedEnemies", menuName = "ScriptableLists/Type: SavedEnemies")]
public class SavedEnemies_SO : ScriptableObject
{
    public List<EnemyInput> listOfAllSavedEnemies = new List<EnemyInput>();
    public List<EnemyInput> listOfAllMissionSavedEnemies = new List<EnemyInput>();
    //public List<CharacterInput> listOfWHATEVER = new List<CharacterInput>();

    public void AddSavedEnemy(EnemyInput adChar)
    {
        listOfAllSavedEnemies.Add(adChar);
    }

    public void RemoveSavedEnemy(EnemyInput adChar)
    {
        listOfAllSavedEnemies.Remove(adChar);
    }

    public List<EnemyInput> GetSavedList()
    {
        return listOfAllSavedEnemies;
    }

    //#################################################################################################

    public void AddMissionEnemy(EnemyInput adChar)
    {
        listOfAllMissionSavedEnemies.Add(adChar);
    }

    public void RemoveMissionEnemy(EnemyInput adChar)
    {
        listOfAllMissionSavedEnemies.Remove(adChar);
    }

    public List<EnemyInput> GetMissionList()
    {
        return listOfAllMissionSavedEnemies;
    }
}
