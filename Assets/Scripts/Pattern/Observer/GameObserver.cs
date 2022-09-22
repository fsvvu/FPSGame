using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameObserver : MonoBehaviour, IGameObserver
{
    [SerializeField] protected GameEvent gameEvent;
    [SerializeField] protected UnityEvent unityEvent;

    public virtual void Awake()
    {
        gameEvent?.Subscribe(this);
        Debug.Log("Game Observer Awake");
    }

    public virtual void OnDestroy()
    {
        gameEvent?.Unsubscribe(this);
    }

    public virtual void Execute(IGameEvent gameEvent)
    {
        Debug.Log($"Execute {this} in base class");
    }

    public virtual void RaiseUnityEvent()
    {
        unityEvent.Invoke();
    }
}
