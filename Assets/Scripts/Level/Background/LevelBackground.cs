using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class LevelBackground : MonoBehaviour
{

    [Inject(Id = "Background")] private GameObject _prefab;
    private GameObject _backgroundObject;

    private void Awake()
    {
        if (_backgroundObject == null)
        {
            _backgroundObject = Instantiate(_prefab, transform);
        }
    }
}
