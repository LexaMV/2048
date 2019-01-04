using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Сontroller : MonoBehaviour
{
    public GameGrid gameGrid;
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
