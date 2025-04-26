using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDataPersistence
{
    // Start is called before the first frame update
    void LoadData(GameData data);

    void SaveData(ref GameData data);
}
