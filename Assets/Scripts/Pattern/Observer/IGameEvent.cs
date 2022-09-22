public interface IGameEvent
{
    // Attach an observer to the subject.
    void Subscribe(IGameObserver observer);

    // Detach an observer from the subject.
    void Unsubscribe(IGameObserver observer);

    // Notify all observers about an event.
    void Notify();
}
