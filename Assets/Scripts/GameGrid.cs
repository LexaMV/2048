using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class GameGrid : MonoBehaviour {

public UI ui;

[Serializable]
 public struct Map {
     public int number;
     public Color color;
 }
    public Map[] colors;
	public GameObject cubik;
	public GameObject gameRectangle;
	public int height;
	public int weight;
	public static float POS_SCALE = 1f;
	public static float POS_OFFSET_X = 0;
	public static float POS_OFFSET_Y = 0;
	public GameObject backGroundBlocks;
	public GameObject gameBlocks;
	private GameObject[,] gameGridBackcgound;
	private GameObject[,] game;

	void Start () {
		CreateGameArray (height, weight);
		CreateBackgroundGameGrid (height, weight);
		CreateGameBlock (height, weight, -5);
	}

    private Color AddColorToBlock(int a)
    {
		Color color = new Color();
		foreach(var m in colors){
			if(m.number == a){
				color = m.color;
			}
		}

       return color;
    }

    public void CreateGameArray (int height, int weight) {
		gameGridBackcgound = new GameObject[height, weight];
		game = new GameObject[height, weight];
	}

	public void CreateBackgroundGameGrid (int height, int weight) {

		for (int i = 0; i < height; i++) {
			for (int u = 0; u < weight; u++) {
				gameGridBackcgound[i, u] = Instantiate (cubik, FixTransformBlockXY (i, u), Quaternion.identity);
				gameGridBackcgound[i,u].transform.SetParent(backGroundBlocks.transform);
			}
		}
	}
	public void CreateGameBlock (int height, int weight, int z) {

		int a = UnityEngine.Random.Range (0, height);
		int b = UnityEngine.Random.Range (0, weight);
		if (game[a, b] == null) {
			game[a, b] = Instantiate (gameRectangle, FixTransformBlockXYZ (a, b, z), Quaternion.identity);
			game[a, b].transform.SetParent(gameBlocks.transform);
			int s = Convert.ToInt32(game[a, b].transform.Find ("Text").GetComponent<TextMeshPro>().text); 
			game[a, b].GetComponent<SpriteRenderer> ().color = AddColorToBlock(s);
			game[a, b].transform.DOScale(0.15f, 0.1f).SetLoops(2,LoopType.Yoyo);
		}
		else {
			CreateGameBlock (height, weight, -5);
		}
	}


	public void GoBlockDown (bool yes) {
		for (int i = 0; i < height; i++) {
			for (int u = 0; u < weight; u++) {
				if (game[i, u] == null && u + 1 != weight) {
					for (int o = u; o < weight; o++) {
						if (o + 1 != weight && game[i, o + 1] != null) {
							GameObject gameObject = game[i, u];
							game[i, o + 1].transform.DOMove (new Vector3 (i, u, -5), 0f);
							game[i, u] = game[i, o + 1];
							game[i, o + 1] = gameObject;
							break;
						}
					}
				}
			}
		}

		if (yes) {
			CheckDown ();
			CreateGameBlock (height, weight, -5);
		}

	}
	public void GoBlockUP (bool yes) {
		for (int i = 0; i < height; i++) {
			for (int u = weight - 1; u > -1; u--) {
				Debug.Log ("i = " + i + " , " + " u = " + u);
				if (game[i, u] == null && u - 1 != -1) {
					for (int o = u; o > -1; o--)
						if (o - 1 != -1 && game[i, o - 1] != null) {
							GameObject gameObject = game[i, u];
							game[i, o - 1].transform.DOMove (new Vector3 (i, u, -5), 0f);
							game[i, u] = game[i, o - 1];
							game[i, o - 1] = gameObject;
							break;
						}
				}
			}
		}
		if (yes) {
			CheckUP ();
			CreateGameBlock (height, weight, -5);
		}
	}

	public void GoBlockLeft (bool yes) {
		for (int u = 0; u < height; u++) {
			for (int i = 0; i < height; i++) {
				// Debug.Log("i = " + i + " , " + " u = " + u);
				if (game[i, u] == null && i + 1 != weight) {
					for (int o = i; o < weight; o++) {
						if (o + 1 != weight && game[o + 1, u] != null) {
							GameObject gameObject = game[i, u];
							game[o + 1, u].transform.DOMove (new Vector3 (i, u, -5), 0f);
							game[i, u] = game[o + 1, u];
							game[o + 1, u] = gameObject;
							break;
						}
					}
				}
			}
		}

		if (yes) {
			CheckLeft ();
			CreateGameBlock (height, weight, -5);
		}
	}

	public void GoBlockRight (bool yes) {
		for (int u = weight - 1; u > -1; u--) {
			for (int i = weight - 1; i > -1; i--) {
			// for (int i = 0; i < height; i++) {
				Debug.Log ("i = " + i + " , " + " u = " + u);
				// if (game[i, u] == null && i - 1 != -1) {
					if (game[i, u] == null) {
					// for (int o = i; o > -1; o--)
					// for (int o = i; o < height; o++)
					 for (int o = i; o > -1; o--)
						if (o - 1 != -1 && game[o - 1, u] != null) {
							// if ( game[o - 1, u] != null) {
								// if ( game[o, u] != null) {
							GameObject gameObject = game[i, u];
							game[o - 1, u].transform.DOMove (new Vector3 (i, u, -5), 0f);
							game[i, u] = game[o - 1, u];
							// game[i, u].GetComponent<SpriteRenderer> ().color = myColor[0];
							game[o - 1, u] = gameObject;
							break;
						}
				}
			}
		}
		if (yes) {
			CheckRight ();
			CreateGameBlock (height, weight, -5);
		}
	}

	private void CheckUP () {
		for (int i = 0; i < height; i++) {
			for (int u = weight - 1; u > -1; u--) {
				if (game[i, u] != null && u - 1 != -1 && game[i, u - 1] != null) {
					if (Convert.ToInt32 (game[i, u].transform.Find ("Text").GetComponent<TextMeshPro> ().text.ToString ()) == Convert.ToInt32 (game[i, u - 1].transform.Find ("Text").GetComponent<TextMeshPro> ().text.ToString ())) {
						game[i, u].transform.Find ("Text").GetComponent<TextMeshPro> ().text = (Convert.ToInt32 (game[i, u].transform.Find ("Text").GetComponent<TextMeshPro> ().text.ToString ()) * 2).ToString ();
						int s = Convert.ToInt32(game[i, u].transform.Find ("Text").GetComponent<TextMeshPro>().text); 
						int a = Convert.ToInt32(ui.score.text);
						ui.score.text = (s+a).ToString();
						ui.UpdateRecordPoint();
			            game[i, u].GetComponent<SpriteRenderer> ().color = AddColorToBlock(s);
						Destroy (game[i, u - 1].gameObject);
						game[i, u - 1] = null;
						GoBlockUP (false);
					}
				}
			}
		}
	}

	private void CheckRight () {
		for (int u = weight - 1; u > -1; u--) {
			for (int i = 0; i < height; i++) {
				if (game[i, u] != null && i - 1 != -1 && game[i - 1, u] != null) {
					if (Convert.ToInt32 (game[i, u].transform.Find ("Text").GetComponent<TextMeshPro> ().text.ToString ()) == Convert.ToInt32 (game[i - 1, u].transform.Find ("Text").GetComponent<TextMeshPro> ().text.ToString ())) {
						game[i, u].transform.Find ("Text").GetComponent<TextMeshPro> ().text = (Convert.ToInt32 (game[i, u].transform.Find ("Text").GetComponent<TextMeshPro> ().text.ToString ()) * 2).ToString ();
						int s = Convert.ToInt32(game[i, u].transform.Find ("Text").GetComponent<TextMeshPro>().text); 
						int a = Convert.ToInt32(ui.score.text);
						ui.score.text = (s+a).ToString();
						ui.UpdateRecordPoint();
			            game[i, u].GetComponent<SpriteRenderer> ().color = AddColorToBlock(s);
						Destroy (game[i - 1, u].gameObject);
						game[i - 1, u] = null;
						GoBlockUP (false);
					}
				}
			}
		}
	}

	private void CheckLeft () {
		for (int u = 0; u < height; u++) {
			for (int i = 0; i < weight; i++) {
				if (game[i, u] != null && i + 1 != weight && game[i + 1, u] != null) {
					if (Convert.ToInt32 (game[i, u].transform.Find ("Text").GetComponent<TextMeshPro> ().text.ToString ()) == Convert.ToInt32 (game[i + 1, u].transform.Find ("Text").GetComponent<TextMeshPro> ().text.ToString ())) {
						game[i, u].transform.Find ("Text").GetComponent<TextMeshPro> ().text = (Convert.ToInt32 (game[i, u].transform.Find ("Text").GetComponent<TextMeshPro> ().text.ToString ()) * 2).ToString ();
						int s = Convert.ToInt32(game[i, u].transform.Find ("Text").GetComponent<TextMeshPro>().text); 
						int a = Convert.ToInt32(ui.score.text);
						ui.score.text = (s+a).ToString();
						ui.UpdateRecordPoint();
			            game[i, u].GetComponent<SpriteRenderer> ().color = AddColorToBlock(s);
						Destroy (game[i + 1, u].gameObject);
						game[i + 1, u] = null;
						GoBlockLeft (false);
					}
				}
			}
		}
	}

	private void CheckDown () {
		for (int i = 0; i < height; i++) {
			for (int u = 0; u < weight; u++) {
				if (game[i, u] != null && u + 1 != weight && game[i, u + 1] != null) {
					if (Convert.ToInt32 (game[i, u].transform.Find ("Text").GetComponent<TextMeshPro> ().text.ToString ()) == Convert.ToInt32 (game[i, u + 1].transform.Find ("Text").GetComponent<TextMeshPro> ().text.ToString ())) {
						game[i, u].transform.Find ("Text").GetComponent<TextMeshPro> ().text = (Convert.ToInt32 (game[i, u].transform.Find ("Text").GetComponent<TextMeshPro> ().text.ToString ()) * 2).ToString ();
						int s = Convert.ToInt32(game[i, u].transform.Find ("Text").GetComponent<TextMeshPro>().text); 
						int a = Convert.ToInt32(ui.score.text);
						ui.score.text = (s+a).ToString();
						ui.UpdateRecordPoint();
			            game[i, u].GetComponent<SpriteRenderer> ().color = AddColorToBlock(s);
						Destroy (game[i, u + 1].gameObject);
						game[i, u + 1] = null;
						GoBlockDown (false);
					}
				}
			}
		}
	}

	public static Vector3 FixTransformBlockXY (int x, int y) {
		return new Vector3 ((x + POS_OFFSET_X) * POS_SCALE, (y + POS_OFFSET_Y) * POS_SCALE, 0);
	}

	public static Vector3 FixTransformBlockXYZ (int x, int y, int z) {
		return new Vector3 ((x - POS_OFFSET_X) * POS_SCALE, (y - POS_OFFSET_Y) * POS_SCALE, z);
	}

}