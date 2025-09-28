using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractiveBackground : MonoBehaviour
{
    [SerializeField] MenuBallSpawner _ballSpawner;
    [SerializeField] private Transform _tractor;
    [SerializeField] private GameObject _sideBorders;
    private IEnumerator cleaningCoroutine = null;

    public void InstatiateBall()
    {
        _ballSpawner.Spawn();
        if (_ballSpawner.List.Count > 25)
        {
            TractorCleaning();
        }
    }
    private void TractorCleaning()
    {
        if (cleaningCoroutine == null)
        {
            cleaningCoroutine = TractorCleaningCoroutine();
            StartCoroutine(cleaningCoroutine);
        }
    }

    private IEnumerator TractorCleaningCoroutine()
    {
        _sideBorders.SetActive(false);
        yield return _tractor.DOMoveX(-15f, 5f).WaitForCompletion();
        _tractor.position = new Vector3(15f, _tractor.position.y, _tractor.position.z);
        _ballSpawner.ClearList();
        cleaningCoroutine = null;
        _sideBorders.SetActive(true);
    }
}
