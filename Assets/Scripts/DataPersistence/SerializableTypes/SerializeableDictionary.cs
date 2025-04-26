using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SerializeableDictionary<TKey, TValue> : Dictionary<TKey, TValue>, ISerializationCallbackReceiver
{
    [SerializeField] private List<TKey> keys = new List<TKey>();
    [SerializeField] private List<TValue> values= new List<TValue>();

    public void OnBeforeSerialize()
    {
        keys.Clear();
        values.Clear();
        foreach (KeyValuePair<TKey, TValue> pair in this)
        {
            keys.Add(pair.Key);
            values.Add(pair.Value);
        }
    }

    public void OnAfterDeserialize() { 
        this.Clear();

        if (Keys.Count != Values.Count)
        {
            Debug.Log("tried to deserialize a SerializeableDictionary, but the amount of keys (" +
                keys.Count + ") does not match the number of values ( " +
                values.Count + ") Which indicates that something went wrong");
        }

        for (int i = 0; i < keys.Count; i++)
        {
            this.Add(keys[i], values[i]);
        }
    }
}
