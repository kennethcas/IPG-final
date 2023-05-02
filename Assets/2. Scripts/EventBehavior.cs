using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "New Event", menuName = "Event")]

public class EventBehavior : ScriptableObject
{
    public void TestEvent()
    {
        Debug.Log("test event successful!");
        //insert logic here
    }

    public void TestEvent02()
    {
        Debug.Log("test event o2 success");
        //insert logic
    }

    public void TestEvent03()
    {
        Debug.Log("test event 03 success");
        //logic here
    }

    public void TestEvent04()
    {
        Debug.Log("test event 03 success");
        //logic here
    }

}
