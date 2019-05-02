using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Case 
{
     public string caseID;
     public string name;
     public string date;
     public byte[] mapImage;
     public string locationNotes;
     public byte[] photoTaken;
     public string photoNotes;

     public Case()
     {
          caseID = GetUniqueID();
     }

     string GetUniqueID()
     {
          string[] split = DateTime.Now.TimeOfDay.ToString().Split(new Char[] { ':', '.' });
          string id = "";
          for (int i = 0; i < split.Length; i++)
          {
               id += split[i];
          }
         return id.Substring(6);
     }
}
