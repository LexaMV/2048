using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class GameGrid : MonoBehaviour {

	public Сontroller controller;
	public SaveAndLoad saveAndLoad;

	[Serializable]
	public struct Map {
		public int number;
		public Color color;
	}
	public Map[] colors;
	public GameObject backGroundCubePrefab;
	public GameObject gameCubePrefab;
	public int height;
	public int weight;
	public static float POS_SCALE = 1f;
	public static float POS_OFFSET_X = 0;
	public static float POS_OFFSET_Y = 0;
	public Transform UIBackGroundBlock;
	public Transform UIGameBlock;
	private GameObject[, ] containerGameGridBackgound;
	private GameObject[, ] gameArray;

	public void LevelStart (int height, int weight) {
		this.height = height;
		this.weight = weight;
		// saveAndLoad.LoadRecordPoints();
		saveAndLoad.score.text = "0";
		CreateArrays (height, weight);
		CreateBackgroundGameGrid (height, weight);
		CreateGameBlock (height, weight, -5);
	}

	private Color GetColorToBlock (int a) {
		Color color = new Color ();
		foreach (var m in colors) {
			if (m.number == a) {
				color = m.color;
			}
		}

		return color;
	}

	public void CreateArrays (int height, int weight) {
		containerGameGridBackgound = new GameObject[height, weight];
		gameArray = new GameObject[height, weight];
	}

	public void CreateBackgroundGameGrid (int height, int weight) {

		for (int i = 0; i < height; i++) {
			for (int u = 0; u < weight; u++) {
				containerGameGridBackgound[i, u] = Instantiate (backGroundCubePrefab, FixTransformBlockXY (i, u), Quaternion.identity);
				containerGameGridBackgound[i, u].transform.SetParent (UIBackGroundBlock);
			}
		}
	}
	public void CreateGameBlock (int height, int weight, int z) {

		if (AllInGameArrayNoNull(gameArray)) {
			controller.endGame.SetActive(true);
			saveAndLoad.SaveRecordPoints();
		}
		else{
				int a = UnityEngine.Random.Range (0, height);
				int b = UnityEngine.Random.Range (0, weight);

				if (gameArray[a, b] == null) {
					gameArray[a, b] = Instantiate (gameCubePrefab, FixTransformBlockXYZ (a, b, z), Quaternion.identity);
					gameArray[a, b].transform.SetParent (UIGameBlock);
					int s = Convert.ToInt32 (gameArray[a, b].transform.Find ("Text").GetComponent<TextMeshPro> ().text);
					gameArray[a, b].GetComponent<SpriteRenderer> ().color = GetColorToBlock (s);
					gameArray[a, b].transform.DOScale (0.15f, 0.1f).SetLoops (2, LoopType.Yoyo);
				} else {
					CreateGameBlock (height, weight, -5);
				}
			}
		}
	
	public bool AllInGameArrayNoNull(GameObject[,] array){
    foreach(GameObject o in array){
		if(o == null){
			 return false;
			 break;
		}
	}
     return true;
	}

	public void GoBlockDown (bool withCheck) {
		for (int i = 0; i < height; i++) {
			for (int u = 0; u < weight; u++) {
				if (gameArray[i, u] == null && u + 1 != weight) {
					for (int o = u; o < weight; o++) {
						if (o + 1 != weight && gameArray[i, o + 1] != null) {
							GameObject gameObject = gameArray[i, u];
							gameArray[i, o + 1].transform.DOMove (new Vector3 (i, u, -5), 0f);
							gameArray[i, u] = gameArray[i, o + 1];
							gameArray[i, o + 1] = gameObject;
							break;
						}
					}
				}
			}
		}

		if (withCheck) {
			CheckDown ();
			CreateGameBlock (height, weight, -5);
		}

	}
	public void GoBlockUP (bool withCheck) {
		for (int i = 0; i < height; i++) {
			for (int u = weight - 1; u > -1; u--) {
				// Debug.Log ("i = " + i + " , " + " u = " + u);
				if (gameArray[i, u] == null && u - 1 != -1) {
					for (int o = u; o > -1; o--)
						if (o - 1 != -1 && gameArray[i, o - 1] != null) {
							GameObject gameObject = gameArray[i, u];
							gameArray[i, o - 1].transform.DOMove (new Vector3 (i, u, -5), 0f);
							gameArray[i, u] = gameArray[i, o - 1];
							gameArray[i, o - 1] = gameObject;
							break;
						}
				}
			}
		}
		if (withCheck) {
			CheckUP ();
			CreateGameBlock (height, weight, -5);
		}
	}

	public void GoBlockLeft (bool withCheck) {
		for (int u = 0; u < weight; u++) {
			for (int i = 0; i < height; i++) {
				// Debug.Log("i = " + i + " , " + " u = " + u);
				if (gameArray[i, u] == null && i + 1 != height) {
					for (int o = i; o < height; o++) {
						if (o + 1 != height && gameArray[o + 1, u] != null) {
							GameObject gameObject = gameArray[i, u];
							gameArray[o + 1, u].transform.DOMove (new Vector3 (i, u, -5), 0f);
							gameArray[i, u] = gameArray[o + 1, u];
							gameArray[o + 1, u] = gameObject;
							break;
						}
					}
				}
			}
		}

		if (withCheck) {
			CheckLeft ();
			CreateGameBlock (height, weight, -5);
		}
	}

	public void GoBlockRight (bool withCheck) {
		 for (int i = height - 1; i > -1; i--) {
		// for (int i = 0; i < height; i++) {
			for (int u = weight - 1; u > -1; u--) {
				// for (int u = weight - 1; u > -1; u--) {
				Debug.Log ("i = " + i + " , " + " u = " + u);
				if (gameArray[i, u] == null) {
					for (int o = i; o > -1; o--)
						if (o - 1 != -1 && gameArray[o - 1, u] != null) {
							GameObject gameObject = gameArray[i, u];
							gameArray[o - 1, u].transform.DOMove (new Vector3 (i, u, -5), 0f);
							gameArray[i, u] = gameArray[o - 1, u];
							gameArray[o - 1, u] = gameObject;
							break;
						}
				}
			}
		}
		if (withCheck) {
			CheckRight ();
			CreateGameBlock (height, weight, -5);
		}
	}

	private void CheckUP () {
		for (int i = 0; i < height; i++) {
			for (int u = weight - 1; u > -1; u--) {
				if (gameArray[i, u] != null && u - 1 != -1 && gameArray[i, u - 1] != null) {
					if (Convert.ToInt32 (gameArray[i, u].transform.Find ("Text").GetComponent<TextMeshPro> ().text.ToString ()) == Convert.ToInt32 (gameArray[i, u - 1].transform.Find ("Text").GetComponent<TextMeshPro> ().text.ToString ())) {
						MultiplicationBy2 (gameArray[i, u]);
						SetScore (gameArray[i, u]);
						SetColor (gameArray[i, u]);
						saveAndLoad.UpdateRecordPoint ();
						Destroy (gameArray[i, u - 1].gameObject);
						gameArray[i, u - 1] = null;
						GoBlockUP (false);
					}
				}
			}
		}
	}

	private void CheckRight () {
			for (int i = height - 1; i > -1; i--) {
			for (int u = weight - 1; u > -1; u--) {
				if (gameArray[i, u] != null && i - 1 != -1 && gameArray[i - 1, u] != null) {
					if (Convert.ToInt32 (gameArray[i, u].transform.Find ("Text").GetComponent<TextMeshPro> ().text.ToString ()) == Convert.ToInt32 (gameArray[i - 1, u].transform.Find ("Text").GetComponent<TextMeshPro> ().text.ToString ())) {
						MultiplicationBy2 (gameArray[i, u]);
						SetScore (gameArray[i, u]);
						SetColor (gameArray[i, u]);
						saveAndLoad.UpdateRecordPoint ();
						Destroy (gameArray[i - 1, u].gameObject);
						gameArray[i - 1, u] = null;
						GoBlockRight (false);
					}
				}
			}
		}
	}

	private void CheckLeft () {
		for (int u = 0; u < weight; u++) {
			for (int i = 0; i < height; i++) {
				if (gameArray[i, u] != null && i + 1 != height && gameArray[i + 1, u] != null) {
					if (Convert.ToInt32 (gameArray[i, u].transform.Find ("Text").GetComponent<TextMeshPro> ().text.ToString ()) == Convert.ToInt32 (gameArray[i + 1, u].transform.Find ("Text").GetComponent<TextMeshPro> ().text.ToString ())) {
						MultiplicationBy2 (gameArray[i, u]);
						SetScore (gameArray[i, u]);
						SetColor (gameArray[i, u]);
						saveAndLoad.UpdateRecordPoint ();
						Destroy (gameArray[i + 1, u].gameObject);
						gameArray[i + 1, u] = null;
						GoBlockLeft (false);
					}
				}
			}
		}
	}

	private void CheckDown () {
		for (int i = 0; i < height; i++) {
			for (int u = 0; u < weight; u++) {
				if (gameArray[i, u] != null && u + 1 != weight && gameArray[i, u + 1] != null) {
					if (Convert.ToInt32 (gameArray[i, u].transform.Find ("Text").GetComponent<TextMeshPro> ().text.ToString ()) == Convert.ToInt32 (gameArray[i, u + 1].transform.Find ("Text").GetComponent<TextMeshPro> ().text.ToString ())) {
						MultiplicationBy2 (gameArray[i, u]);
						SetScore (gameArray[i, u]);
						SetColor (gameArray[i, u]);
						saveAndLoad.UpdateRecordPoint ();
						Destroy (gameArray[i, u + 1].gameObject);
						gameArray[i, u + 1] = null;
						GoBlockDown (false);
					}
				}
			}
		}
	}

	private void MultiplicationBy2 (GameObject gameObject) {
		gameObject.transform.Find ("Text").GetComponent<TextMeshPro> ().text = (Convert.ToInt32 (gameObject.transform.Find ("Text").GetComponent<TextMeshPro> ().text.ToString ()) * 2).ToString ();
	}

	private void SetColor (GameObject gameObject) {
		int s = Convert.ToInt32 (gameObject.transform.Find ("Text").GetComponent<TextMeshPro> ().text);
		gameObject.GetComponent<SpriteRenderer> ().color = GetColorToBlock (s);
	}

	private void SetScore (GameObject gameObject) {
		int s = Convert.ToInt32 (gameObject.transform.Find ("Text").GetComponent<TextMeshPro> ().text);
		int a = Convert.ToInt32 (saveAndLoad.score.text);
		saveAndLoad.score.text = (s + a).ToString ();
	}
	public static Vector3 FixTransformBlockXY (int x, int y) {
		return new Vector3 ((x + POS_OFFSET_X) * POS_SCALE, (y + POS_OFFSET_Y) * POS_SCALE, 0);
	}

	public static Vector3 FixTransformBlockXYZ (int x, int y, int z) {
		return new Vector3 ((x - POS_OFFSET_X) * POS_SCALE, (y - POS_OFFSET_Y) * POS_SCALE, z);
	}

}