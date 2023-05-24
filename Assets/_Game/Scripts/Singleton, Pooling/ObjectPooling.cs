using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooling : Singleton<ObjectPooling>
{
    public Dictionary<GameObject, List<GameObject>> _dicGameObject = new Dictionary<GameObject, List<GameObject>>();

    public GameObject GetGameObject(GameObject prefab)
    {

        List<GameObject> _itemPools = new List<GameObject>();
        if (!_dicGameObject.ContainsKey(prefab))
        {
            _dicGameObject.Add(prefab, _itemPools);
        }
        else
        {
            _itemPools = _dicGameObject[prefab];
        }

        foreach (GameObject g in _itemPools)
        {
            if (g.activeSelf) continue;
            return g;
        }

        GameObject obj = Instantiate(prefab);
        obj.SetActive(false);
        _dicGameObject[prefab].Add(obj);
        return obj;

    }
}
