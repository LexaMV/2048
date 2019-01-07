using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Сontroller : MonoBehaviour
{
    public GameGrid gameGrid;
	private GameObject firstScreen;
	private GameObject secondScreen;
	
	void Awake(){
		firstScreen = GameObject.Find("FirstScreen");
		secondScreen = GameObject.Find("SecondScreen");
	}
	void Start(){
	        secondScreen.SetActive(false);
	}

	public void FirstScreenActive(){
		firstScreen.SetActive(true);
		secondScreen.SetActive(false);
	}

	public void SecondScreenActive(){
		firstScreen.SetActive(false);
		secondScreen.SetActive(true);
	}


    public void GameGridDown () {

		gameGrid.GoBlockDown (true);
		Debug.Log ("Down");

	}
	public void GameGridUP () {
		gameGrid.GoBlockUP (true);
		Debug.Log ("UP");

	}

	public void GameGridLeft () {
		gameGrid.GoBlockLeft (true);
		Debug.Log ("Left");

	}

		public void GameGridRight () {
		gameGrid.GoBlockRight (true);
		Debug.Log ("Right");

	}
    
}
