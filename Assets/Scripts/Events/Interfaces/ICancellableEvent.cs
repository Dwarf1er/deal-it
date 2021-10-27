public interface ICancellableEvent : IEvent {
    void Cancel();

    bool IsCancelled();
}
