using System.Collections.Generic;
using UnityEngine;

public abstract class Subject : MonoBehaviour
{
    //subject for observer pattern. Can add/remover observers and send notifications
    //list of all observers
    private List<IObserver> observers = new List<IObserver>();

    //adds observer to list
    public void addObserver(IObserver observer)
    {
        observers.Add(observer);
    }
    
    //removes observer from list
    public void removeObserver(IObserver observer)
    {
        observers.Remove(observer);
    }

    //notifies observers of an event
    public void NotifyObserver(PlayerActions action)
    {
        observers.ForEach((observer) =>
        {
            observer.OnNotify(action);
        });
    }
}
