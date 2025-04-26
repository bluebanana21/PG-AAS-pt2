using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    public Vector3 playePosition;
    
    public SerializeableDictionary<string, bool> NPCState;
    
    public GameData()
    {
        playePosition = Vector3.zero;
       
        NPCState = new SerializeableDictionary<string, bool>();
    }
}
