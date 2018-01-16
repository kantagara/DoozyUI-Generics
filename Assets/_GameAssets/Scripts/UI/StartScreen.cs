using System.Collections;
using System.Collections.Generic;
using Pool;
using UnityEngine;
using UnityEngine.UI;
using _GameAssets.Scripts;

public class StartScreen : MonoBehaviour
{

	public Button AddMessageButton;
	public Text MessageCount;
	
	
	private int _temp; 
	void Start()
	{
		_temp = 0;
		AddMessageButton.onClick.AddListener(AddMessageToList);
		DisplayUnreadMessages();
	}


	void AddMessageToList()
	{
		EventPool.Trigger(EventTypes.MessageArrived, "Title", "Message" + _temp++);
		DisplayUnreadMessages();
	}

	void DisplayUnreadMessages()
	{
		var messages = InfoPool.Request<List<Message>>("unread_messages");
		MessageCount.text = messages.Count > 9 ? "9+" : messages.Count.ToString();
	}

}
