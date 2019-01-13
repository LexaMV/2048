using System.Collections;
using System.Collections.Generic;
using Lean.Touch;
using UnityEngine;

public class Сontroller : MonoBehaviour
{

    #region GameObject control
	private GameObject backGroundBlocks;
	private GameObject gameBlocks;
	private GameObject camera;
	private Vector3 startingPositionCamera;
	#endregion

   #region Canvas control
  
    public GameGrid gameGrid;
	public GameObject endGame;
	private GameObject firstScreen;
	private GameObject secondScreen;
	private GameObject recordPanel;
	private GameObject scorePanel;
	private GameObject playButton;
	private GameObject firstScreenButton;
	private GameObject secondScreenButton;
	private GameObject backButton;
	private GameObject refreshButton;
    #endregion

    public string selectedField; 

	
	void Awake(){
		endGame = GameObject.Find("EndGame");
		camera = GameObject.Find("MainCamera");
		startingPositionCamera = camera.transform.position;
		backGroundBlocks = GameObject.Find("BackGroundBlocks");
		gameBlocks = GameObject.Find("GameBlocks");
		firstScreen = GameObject.Find("FirstScreen");
		secondScreen = GameObject.Find("SecondScreen");
		recordPanel = GameObject.Find("RecordPanel");
		scorePanel = GameObject.Find("ScorePanel");
		playButton = GameObject.Find("PlayButton");
		firstScreenButton = GameObject.Find("FirstScreenButton");
		secondScreenButton = GameObject.Find("SecondScreenButton");
		backButton = GameObject.Find("BackButton");
		refreshButton = GameObject.Find("RefreshButton");
	}
	void Start(){
		    endGame.SetActive(false);
		    backButton.SetActive(false);
			refreshButton.SetActive(false);
	        secondScreen.SetActive(false);
			recordPanel.SetActive(false);
	    	scorePanel.SetActive(false);
	}

	public void PlayGame(){
		GameSrceen();

		switch(selectedField){
			case "4x4":
			gameGrid.LevelStart(4,4);
			camera.transform.position = new Vector3(1.46f,2.82f,camera.transform.position.z);
			camera.GetComponent<Camera>().orthographicSize = 4.78f;
			break;
			case "5x5":
			gameGrid.LevelStart(5,5);// не работает!!!!?????SS
			break;
			case "6x6":
			gameGrid.LevelStart(6,6);
			camera.transform.position = new Vector3(2.52f,4.23f,camera.transform.position.z);
			camera.GetComponent<Camera>().orthographicSize = 6.52f;
			break;
			case "8x8":
			gameGrid.LevelStart(8,8);
			camera.transform.position = new Vector3(3.44f,5.6f,camera.transform.position.z);
			camera.GetComponent<Camera>().orthographicSize = 8.63f;
			break;
			case "3x5":
			gameGrid.LevelStart(3,5);
			camera.transform.position = new Vector3(0.91f,3.25f,camera.transform.position.z);
			camera.GetComponent<Camera>().orthographicSize = 4.05f;
			break;
			case "4x6":
			gameGrid.LevelStart(4,6);
			camera.transform.position = new Vector3(1.51f,3.96f,camera.transform.position.z);
			camera.GetComponent<Camera>().orthographicSize = 5.12f;
			break;
			case "5x8":
			gameGrid.LevelStart(5,8);
			camera.transform.position = new Vector3(1.88f,5.4f,camera.transform.position.z);
			camera.GetComponent<Camera>().orthographicSize = 6.41f;
			break;
			case "6x9":
			gameGrid.LevelStart(6,9);
			camera.transform.position = new Vector3(2.37f,6.28f,camera.transform.position.z);
			camera.GetComponent<Camera>().orthographicSize = 7.5f;
			break;
		}
	     
	}

	public void GameSrceen(){ // урезать до 3
		firstScreen.SetActive(false);
		secondScreen.SetActive(false);
		playButton.SetActive(false);
		firstScreenButton.SetActive(false);
		secondScreenButton.SetActive(false);
		recordPanel.SetActive(true);
		scorePanel.SetActive(true);
		backButton.SetActive(true);
		refreshButton.SetActive(true);
	}

	public void MenuSrceen(){  // урезать до 3

       foreach(Transform o in backGroundBlocks.transform){
		   Destroy(o.gameObject);
	   }

	   foreach(Transform o in gameBlocks.transform){
		   Destroy(o.gameObject);
	   }

		firstScreen.SetActive(true);
		// secondScreen.SetActive(true);
		playButton.SetActive(true);
		firstScreenButton.SetActive(true);
		secondScreenButton.SetActive(true);
		recordPanel.SetActive(false);
		scorePanel.SetActive(false);
		backButton.SetActive(false);
		refreshButton.SetActive(false);
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
