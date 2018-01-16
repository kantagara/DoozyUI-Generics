using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Message{

    public string ID;
    public string Title;
    public string Body;
    public bool Read;

    public Message()
    {
        ID = System.Guid.NewGuid().ToString();
    }

}
