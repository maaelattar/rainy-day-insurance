using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
     private static UIManager instance;
     public static UIManager Instance {

          get
          {
               if (instance == null)
               {
                    Debug.LogError("The UI Manager instance is NULL");
               }
               return instance;
          }
     }

     public Case activeCase;
     public GameObject clientInfoPanel;
     public GameObject borderPanel;
     public GameObject selectPanel;

     private void Awake()
     {
          instance = this;
     }

     void Update()
     {
          OnDownloadComlete();
          OnUploadComlete();
     }

     

     void OnDownloadComlete()
     {
          if (FbManager.Instance.downloaded == true)
          {
               FbManager.Instance.downloaded = false;
               selectPanel.SetActive(true);
               
          }
     }
     
     void OnUploadComlete()
     {
          if (FbManager.Instance.uploaded == true)
          {
               FbManager.Instance.uploaded = false;
               LoadHome();
               
          }
     }


     public void CreateNewCase()
     {
          activeCase = new Case();
   
          clientInfoPanel.SetActive(true);
          borderPanel.SetActive(true);
     }

     public Texture LoadImage(byte[] imgBytes)
     {
          Texture2D reconstructedImage = new Texture2D(1, 1);
          reconstructedImage.LoadImage(imgBytes);
          Texture img = (Texture)reconstructedImage;
          return img;
     }

     public void SubmitButton()
     {
          byte[] data;
          using (MemoryStream ms = new MemoryStream())
          {
               BinaryFormatter bf = new BinaryFormatter();
               bf.Serialize(ms, activeCase);
               data = ms.ToArray();
          }

          
          FbManager.Instance.UploadFile(activeCase.caseID, data);
          

     }

     public void LoadHome()
     {
          SceneManager.LoadScene(0);
     }
}
