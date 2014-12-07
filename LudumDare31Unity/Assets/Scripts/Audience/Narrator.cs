using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using CraftingLegends.Core;
using UnityEngine.UI;

public class Narrator : MonoBehaviour {

	private Queue<string> _messages = new Queue<string>();

	private SentenceList _greetings;

	private List<Text> _textDisplays;

	private Animator _animator;

    void Awake() {
		_animator = GetComponent<Animator>();
		_animator.SetBool("show", false);
		_textDisplays = new List<Text>(GetComponentsInChildren<Text>());

		Init();

		StartCoroutine(UpdateMessages());
    }

	// ================================================================================
	//  public methods
	// --------------------------------------------------------------------------------

	public void GreetNewHero(PlayerCharacter player)
	{
		_messages.Clear();
		AddMessage(_greetings.Next());		
	}

	// ================================================================================
	//  private methods
	// --------------------------------------------------------------------------------

	private IEnumerator UpdateMessages()
	{
		while (true)
		{
			if (_messages.Count > 0)
			{
				string message = _messages.Dequeue();

				for (int i = 0; i < _textDisplays.Count; i++)
				{
					_textDisplays[i].text = message;
				}

				_animator.SetBool("show", true);

				yield return new WaitForSeconds(3.0f);

				_animator.SetBool("show", false);

				yield return new WaitForSeconds(0.5f);
			}

			yield return null;
		}
	}

	private void AddMessage(string message)
	{
		var parts = message.Split('*');

		foreach (var item in parts)
		{
			_messages.Enqueue(item);
		}
	}

	private void Init()
	{
		_greetings = new SentenceList()
		{
			"Behold, people. We have a new contestant",
			"Nice try.*Let's hope the next one endures a little longer.",
			"See this? Another human enters the frame.",
			"Don't fret. We have enough of those champions left.",
			"Well, another human enters the stage*Let's so how long this one fares."
		};
	}
}