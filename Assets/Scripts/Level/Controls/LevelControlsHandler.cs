using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class LevelControlsHandler : MonoBehaviour
{
    [SerializeField] private KeyCode keyDoubler;
    [SerializeField] private UnityEvent action;

    public UnityEvent Action { get => action; set => action = value; }

    void Update()
    {
        if ((Input.touchCount > 0) && (Input.GetTouch(0).phase == TouchPhase.Began))
        {
            action.Invoke();
        }
        if (Input.GetKeyDown(keyDoubler))
        {
            action.Invoke();
        }
    }
}
