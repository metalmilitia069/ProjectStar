using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterGeometryInjection : MonoBehaviour
{
    private void OnEnable()
    {
        GetComponent<CharacterGeometry>().CharacterGeometryVariables = ScriptableObject.CreateInstance<CharacterGeometry_SO>();
        GetComponent<CharacterGeometry>().CharacterGeometryVariables.name = "CharacterGeometryInstance";
    }
}
