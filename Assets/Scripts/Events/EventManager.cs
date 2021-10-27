using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class EventManager : MonoBehaviour {
    private Dictionary<object, HashSet<object>> subscriberEventHandlers;
    private Dictionary<object, object> eventHandlerSubcribers;
    private static EventManager eventManager;

    protected void Awake() {
        if(eventManager) Destroy(this);
        eventManager = this;

        this.subscriberEventHandlers = new Dictionary<object, HashSet<object>>();
        this.eventHandlerSubcribers = new Dictionary<object, object>();
    }

    protected void Start() {}

    /// Gets singleton instance of class.
    public static EventManager Get() {
        return eventManager;
    }
    
    /// Subscribe to a type of event.
    public EventManager Subscribe<T>(Action<T> eventHandler) where T: IEvent {
        object subscriber = eventHandler.Target;

        if(!(subscriber is ISubscriber)) throw new TypeLoadException("Subscriber needs to be instance of ISubscriber.");

        if(!this.subscriberEventHandlers.ContainsKey(subscriber)) {
            this.subscriberEventHandlers[subscriber] = new HashSet<object>();
        }

        this.subscriberEventHandlers[subscriber].Add(eventHandler);
        this.eventHandlerSubcribers[eventHandler] = eventHandler.Target;
        
        return this;
    }

    private void BroadcastCollection<T, U>(T eventObject, ICollection<U> eventHandlers) where T: IEvent {
        Type actionType = typeof(Action<>).MakeGenericType(new Type[]{ eventObject.GetType() });
        MethodInfo actionInvoke = actionType.GetMethod("Invoke");
        object[] parameters = new object[]{ eventObject };

        foreach(object eventHandler in eventHandlers) {
            if(!(eventHandler.GetType() == actionType)) continue;
            actionInvoke.Invoke(eventHandler, parameters);
        }
    }

    private void BroadcastSender<T>(T eventObject) where T: IEvent {
        foreach(object subscriber in subscriberEventHandlers.Keys) {
            if(!(subscriber is ISubscriber)) continue;

            ISubscriber castedSubscriber = (ISubscriber)subscriber;

            if(castedSubscriber.HasDistance()) {
                Transform transform = castedSubscriber.GetTransform();
                if(Vector3.Distance(eventObject.GetPosition(), transform.position) > eventObject.GetRange()) continue;
            }

            HashSet<object> eventHandlers = subscriberEventHandlers[subscriber];

            BroadcastCollection(eventObject, eventHandlers);
        }
    }

    private IEnumerator BroadcastDelayedEnumerator<T>(T eventObject, float delay) where T: IEvent {
        if(eventObject is ICancellableEvent) {
            ICancellableEvent cancellableEvent = (ICancellableEvent) eventObject;

            float increment = delay / 10.0f;
            for(float i = 0; i < delay; i += increment) {
                yield return new WaitForSeconds(increment);

                if(cancellableEvent.IsCancelled()) break;
            }
        } else {
            yield return new WaitForSeconds(delay);
        }

        BroadcastNoDelay<T>(eventObject);
    }

    private void BroadcastNoDelay<T>(T eventObject) where T: IEvent {
        if(eventObject is IStartEvent) {
            IStartEvent startEvent = (IStartEvent)eventObject;
            BroadcastSender(eventObject);
            Broadcast(startEvent.GetEndEvent());
        } else {
            BroadcastSender(eventObject);
        }
    }

    public void BroadcastWithDelay<T>(T eventObject, float delay) where T: IEvent {
        StartCoroutine(this.BroadcastDelayedEnumerator(eventObject, delay));
    }

    /// Sends an event to subscribers.
    public void Broadcast<T>(T eventObject) where T: IEvent {
        if(eventObject is IDelayedEvent) {
            IDelayedEvent delayedEvent = (IDelayedEvent)eventObject;
            BroadcastWithDelay<T>(eventObject, delayedEvent.GetDelay());
        } else {
            BroadcastNoDelay<T>(eventObject);
        }
    }

    /// Unsubscribe from event type.
    public void UnSubscribe<T>(Action<T> eventHandler) where T: IEvent {
        object subscriber = eventHandler.Target;
        this.subscriberEventHandlers[subscriber].Remove(eventHandler);
        this.eventHandlerSubcribers.Remove(eventHandler);
    }

    /// Unsubscribe all event types for a subscriber.
    /// NOTE: Needs to be used when a subscriber is destroyed!
    public void UnSubcribeAll(object subscriber) {
        if(!subscriberEventHandlers.ContainsKey(subscriber)) return;

        foreach(object eventHandler in subscriberEventHandlers[subscriber]) {
            this.eventHandlerSubcribers.Remove(eventHandler);
        }

        this.subscriberEventHandlers.Remove(subscriber);
    }
}
