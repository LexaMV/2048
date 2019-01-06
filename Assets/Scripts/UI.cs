using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UI : MonoBehaviour
{
  public int intRecord;
  public TextMeshProUGUI record;
  public TextMeshProUGUI score;

     void SaveRecordPoints() {

         intRecord = Convert.ToInt32(record.text);
         PlayerPrefs.SetInt("RecordScore", intRecord);
         PlayerPrefs.Save();
     }

     void LoadRecordPoints() {
          intRecord = PlayerPrefs.GetInt("RecordScore");
          record.text = intRecord.ToString();
     }

      public void UpdateRecordPoint() {
          if (Convert.ToInt32(record.text) < Convert.ToInt32(score.text)) {
               record.text = score.text;
          }
     }


}
