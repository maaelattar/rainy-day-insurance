using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OverviewPanel : MonoBehaviour, IPanel
{
     public Text caseNumberTitle;
     public Text nameTitle;
     public Text dateTitle;
     public RawImage locationMap;
     public Text locationNotes;
     public RawImage photoTaken;
     public Text photoNotes;

     private void OnEnable()
     {
          caseNumberTitle.text = "CASE NUMBER " + UIManager.Instance.activeCase.caseID;
          nameTitle.text = UIManager.Instance.activeCase.name;
          dateTitle.text = DateTime.Today.ToString();
          /*
          Texture2D reconstructedImage = new Texture2D(1, 1);
          reconstructedImage.LoadImage(UIManager.Instance.activeCase.photoTaken);
          Texture img = (Texture)reconstructedImage;
          */
          locationMap.texture = UIManager.Instance.LoadImage(UIManager.Instance.activeCase.mapImage);
          locationNotes.text = "LOCATION NOTES: \n " +  UIManager.Instance.activeCase.locationNotes;

          photoTaken.texture = UIManager.Instance.LoadImage(UIManager.Instance.activeCase.photoTaken);

          photoNotes.text = "PHOTO NOTES: \n " + UIManager.Instance.activeCase.photoNotes;
     }
     public void ProcessInfo()
     {

     }
     /*
     private Texture LoadImage(byte[] imgBytes)
     {
          Texture2D reconstructedImage = new Texture2D(1, 1);
          reconstructedImage.LoadImage(imgBytes);
          Texture img = (Texture)reconstructedImage;
          return img;
     }*/
}
