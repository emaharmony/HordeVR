using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour 
{
	int points = 0;
	[SerializeField]Text scoreBoard;

	void Start()
	{
		points = 0;
		UpdateDisplay ();
	}

	public void Add(int amt)
	{
		points += amt;
		UpdateDisplay();
	}

	void UpdateDisplay()
	{
        //green text float upward with number of points
		//scoreBoard.text = "Score: " + points;
	}
		

}
