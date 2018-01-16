using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Pool;
using UnityEngine;
using _GameAssets.Scripts;

public class GameManager : MonoBehaviour {

    public static GameManager Instance;

    #region Player Data
    List<Message> messages;
    #endregion

        
    void Awake()
    {
        if(Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
        messages = new List<Message>();
    }

    void OnEnable()
    {
        EventPool.StartListening<string, string>(EventTypes.MessageArrived, NewMessageArrived);
        InfoPool.Provide("unread_messages", GetUnreadMessages);
    }

    void OnDisable()
    {
        EventPool.StopListening<string, string>(EventTypes.MessageArrived, NewMessageArrived);
        InfoPool.Unprovide("unread_messages", GetUnreadMessages);
    }

    private void NewMessageArrived(string title, string body)
    {
        messages.Add(new Message{ Title = title, Body = body});
    }


    #region Info Pool Data   

    private List<Message> GetUnreadMessages()
    {
        return messages.FindAll(message => message.Read == false);
    }
    

    #endregion

}
