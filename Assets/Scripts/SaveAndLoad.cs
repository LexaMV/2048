using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SaveAndLoad : MonoBehaviour
{
  public int intRecord;
  public TextMeshProUGUI record;
  public TextMeshProUGUI score;

     public void SaveRecordPoints() {

         intRecord = Convert.ToInt32(record.text);
     //     ES2.Save(intRecord,"RecordScore");
         PlayerPrefs.SetInt("RecordScore", intRecord);
         PlayerPrefs.Save();
     }

     public void LoadRecordPoints() {
          intRecord = PlayerPrefs.GetInt("RecordScore");
          // intRecord = ES2.Load<int>("RecordScore");
          record.text = intRecord.ToString();
     }

      public void UpdateRecordPoint() {
          if (Convert.ToInt32(record.text) < Convert.ToInt32(score.text)) {
               record.text = score.text;
          }
     }


}
