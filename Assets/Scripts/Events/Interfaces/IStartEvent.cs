public interface IStartEvent : IEvent {
    IEndEvent GetEndEvent();
}
