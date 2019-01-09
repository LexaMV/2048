using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class СhoiceField : MonoBehaviour
{
   public void Field(){
       GameObject.Find("MainCamera").GetComponent<Сontroller>().selectedField = gameObject.name;
   }
   
}
