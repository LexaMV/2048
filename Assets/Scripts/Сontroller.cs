using System.Collections;
using System.Collections.Generic;
using Lean.Touch;
using UnityEngine;
using UnityEngine.Advertisements;

public class Сontroller : MonoBehaviour
{

    #region GameObject control
	public SaveAndLoad saveAndLoad;
	private GameObject backGroundBlocks;
	private GameObject gameBlocks;
	private GameObject camera;
	private Vector3 startingPositionCamera;
	#endregion

   #region Canvas control
    public GameObject directions;
    public GameGrid gameGrid;
	public GameObject endGame;
	public GameObject firstScreenMenu;
	public GameObject secondScreenMenu;
	public GameObject recordPanel;
	public GameObject scorePanel;
	public GameObject playButton;
	public GameObject firstScreenMenuButton;
	public GameObject secondScreenMenuButton;
    public GameObject backButton;
	public GameObject refreshButton;
    #endregion

    public string selectedField; 

	#region ADS control
	public int ADS;
	#endregion

	
	void Awake(){

        ADS = 0;
		camera = GameObject.Find("MainCamera");
		backGroundBlocks = GameObject.Find("BackGroundBlocks");
	    gameBlocks = GameObject.Find("GameBlocks");
	}
	void Start(){

            if(Advertisement.isSupported){
               Advertisement.Initialize("3056381",false);
			}

		    endGame.SetActive(false);
		    backButton.SetActive(false);
			refreshButton.SetActive(false);
	        secondScreenMenu.SetActive(false);
			recordPanel.SetActive(false);
	    	scorePanel.SetActive(false);
			directions.SetActive(false);
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
			gameGrid.LevelStart(5,5);
			camera.transform.position = new Vector3(1.97f,3.25f,camera.transform.position.z);
			camera.GetComponent<Camera>().orthographicSize = 5.45f;
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

	public void GameSrceen(){ 
	    refreshButton.SetActive(true);
	    directions.SetActive(true);
		firstScreenMenu.SetActive(false);
		secondScreenMenu.SetActive(false);
		playButton.SetActive(false);
		firstScreenMenuButton.SetActive(false);
		secondScreenMenuButton.SetActive(false);
		recordPanel.SetActive(true);
		scorePanel.SetActive(true);
		backButton.SetActive(true);
		refreshButton.SetActive(true);
	}

	public void BackToMenuSrceen(){  // урезать до 3

       foreach(Transform o in backGroundBlocks.transform){
           saveAndLoad.SaveRecordPoints();
		   Destroy(o.gameObject);
	   }

	   foreach(Transform o in gameBlocks.transform){
		   Destroy(o.gameObject);
	   }

		firstScreenMenu.SetActive(true);
		directions.SetActive(false);
		playButton.SetActive(true);
		firstScreenMenuButton.SetActive(true);
		secondScreenMenuButton.SetActive(true);
		recordPanel.SetActive(false);
		scorePanel.SetActive(false);
		backButton.SetActive(false);
		refreshButton.SetActive(false);
		endGame.SetActive(false);
	}

	public void FirstScreenActive(){
		firstScreenMenu.SetActive(true);
		secondScreenMenu.SetActive(false);
	}


	public void SecondScreenActive(){
		firstScreenMenu.SetActive(false);
		secondScreenMenu.SetActive(true);
	}



	public void EndGame(){
		 ADS++;

		 if(ADS == 3){
			if(Advertisement.IsReady()){
				Advertisement.Show();
				ADS = 0;
			}
		}

		 directions.SetActive(false);
		 refreshButton.SetActive(false);
		 DestroyAllGameObject();
		 endGame.SetActive(true);
		 saveAndLoad.SaveRecordPoints();


	}
    
	public void DestroyAllGameObject(){

           foreach(Transform o in gameBlocks.transform){
		   Destroy(o.gameObject);
	   }

	     foreach(Transform o in backGroundBlocks.transform){
		   Destroy(o.gameObject);
	   }
	}
    public void CleanGrid () {

		foreach(Transform o in gameBlocks.transform){
		   Destroy(o.gameObject);
	   }

	   gameGrid.CreateGameBlock(gameGrid.height,gameGrid.weight, -5);


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
