using UnityEngine;
using UnityEngine.Events;
using System.Collections;
using System.Collections.Generic;

// CODE TAKEN FROM:
//      https://unity3d.com/learn/tutorials/topics/scripting/events-creating-simple-messaging-system and
//      https://forum.unity.com/threads/messaging-system-passing-parameters-with-the-event.331284/ (user: raglet)

[System.Serializable]
public class ThisEvent : UnityEvent<float>
{
}

public class EventManager : MonoBehaviour
{
    private Dictionary<string, ThisEvent> eventDictionary;

    private static EventManager eventManagerArgs;

    public static EventManager instance
    {
        get
        {
            if (!eventManagerArgs)
            {
                eventManagerArgs = FindObjectOfType(typeof(EventManager)) as EventManager;

                if (!eventManagerArgs)
                {
                    Debug.LogError("There needs to be one active EventManger script on a GameObject in your scene.");
                }
                else
                {
                    eventManagerArgs.Init();
                }
            }

            return eventManagerArgs;
        }
    }

    void Init()
    {
        if (eventDictionary == null)
        {
            eventDictionary = new Dictionary<string, ThisEvent>();
        }
    }

    public static void StartListening(string eventName, UnityAction<float> listener)
    {
        ThisEvent thisEvent = null;
        if (instance.eventDictionary.TryGetValue(eventName, out thisEvent))
        {
            thisEvent.AddListener(listener);
        }
        else
        {
            thisEvent = new ThisEvent();
            thisEvent.AddListener(listener);
            instance.eventDictionary.Add(eventName, thisEvent);
        }
    }

    public static void StopListening(string eventName, UnityAction<float> listener)
    {
        if (eventManagerArgs == null) return;
        ThisEvent thisEvent = null;
        if (instance.eventDictionary.TryGetValue(eventName, out thisEvent))
        {
            thisEvent.RemoveListener(listener);
        }
    }

    public static void TriggerEvent(string eventName, float value)
    {
        ThisEvent thisEvent = null;
        if (instance.eventDictionary.TryGetValue(eventName, out thisEvent))
        {
            thisEvent.Invoke(value);
        }
    }
}