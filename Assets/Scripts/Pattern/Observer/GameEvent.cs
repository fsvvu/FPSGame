using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "GameEvent", fileName = "New Game Event")]
public class GameEvent : ScriptableObject, IGameEvent
{
    // List of subscribers. In real life, the list of subscribers can be
    // stored more comprehensively (categorized by event type, etc.).
    private HashSet<IGameObserver> gameObservers = new HashSet<IGameObserver>();

    // The subscription management methods.
    public void Subscribe(IGameObserver observer)
    {
        this.gameObservers.Add(observer);
        Debug.Log($"Add {observer}");
    }

    public void Unsubscribe(IGameObserver observer)
    {
        this.gameObservers.Remove(observer);
    }

    // Trigger an update in each subscriber.
    public void Notify()
    {
        foreach (var observer in gameObservers)
        {
            observer.Execute(this);
        }
    }

    // Usually, the subscription logic is only a fraction of what a Subject
    // can really do. Subjects commonly hold some important business logic,
    // that triggers a notification method whenever something important is
    // about to happen (or after it).
    public void SomeBusinessLogic()
    {
        //Some logic here
        this.Notify();
    }
}
