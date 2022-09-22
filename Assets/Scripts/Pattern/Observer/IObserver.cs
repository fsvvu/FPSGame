public interface IGameObserver
{
    // Receive update from subject
    void Execute(IGameEvent gameEvent);

    void RaiseUnityEvent();
}
