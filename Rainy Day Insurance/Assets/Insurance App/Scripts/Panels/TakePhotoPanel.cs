using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TakePhotoPanel : MonoBehaviour, IPanel
{
     public Text caseNumberTitle;
     public RawImage photoTaken;
     public InputField photoNotes;
     public GameObject overViewPanel;

     private string imgPath;

     private void OnEnable()
     {
          caseNumberTitle.text = "CASE NUMBER " + UIManager.Instance.activeCase.caseID;
     }
     public void ProcessInfo()
     {
          byte[] imgData = null;
          if (string.IsNullOrEmpty(imgPath) == false)
          {
               Texture2D img = NativeCamera.LoadImageAtPath(imgPath, 512, false);
               imgData = img.EncodeToPNG();
          }
          

          UIManager.Instance.activeCase.photoTaken = imgData;
          UIManager.Instance.activeCase.photoNotes = photoNotes.text;
          overViewPanel.SetActive(true);
     }

     

     public void TakePictureButton()
     {
          TakePicture(512);
     }
     private void TakePicture(int maxSize)
     {
          NativeCamera.Permission permission = NativeCamera.TakePicture((path) =>
          {
               Debug.Log("Image path: " + path);
               if (path != null)
               {
                    // Create a Texture2D from the captured image
                    Texture2D texture = NativeCamera.LoadImageAtPath(path, maxSize, false);
                    if (texture == null)
                    {
                         Debug.Log("Couldn't load texture from " + path);
                         return;
                    }

                    photoTaken.texture = texture;
                    photoTaken.gameObject.SetActive(true);
                    imgPath = path;
               }
          }, maxSize);

          Debug.Log("Permission result: " + permission);
     }

     private void RecordVideo()
     {
          NativeCamera.Permission permission = NativeCamera.RecordVideo((path) =>
          {
               Debug.Log("Video path: " + path);
               if (path != null)
               {
                    // Play the recorded video
                    Handheld.PlayFullScreenMovie("file://" + path);
               }
          });

          Debug.Log("Permission result: " + permission);
     }

}
