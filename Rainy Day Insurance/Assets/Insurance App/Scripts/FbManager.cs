using Firebase.Storage;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

public class FbManager : MonoBehaviour

{

     private static FbManager instance;
     public static FbManager Instance
     {

          get
          {
               if (instance == null)
               {
                    Debug.LogError("FbManager instance is null");
               }
               return instance;
          }

     }

     private void Awake()
     {
          Debug.Log("fbManager instance has been instantiated");
          instance = this;
          storage = FirebaseStorage.DefaultInstance;

     }

     FirebaseStorage storage;

     public bool downloaded = false;
     public bool uploaded = false;

     public void UploadFile(string fileName, byte[] data)
     {

          StorageReference fileRef = storage.RootReference.Child(fileName);


          fileRef.PutBytesAsync(data).ContinueWith((Task<StorageMetadata> task) =>
          {
               if (task.IsFaulted || task.IsCanceled)
               {
                    Debug.Log(task.Exception.ToString());
               }
               else
               {
                    Debug.Log("Finished uploading: " + fileName);
                    uploaded = true;
               }
          });
     }


     public void DownloadFile(string filename)
     {
          Debug.Log("Downloading: " + filename);
          StorageReference path_reference = storage.GetReference(filename);
          const long maxAllowedSize = 10 * 1024 * 1024;

          path_reference.GetBytesAsync(maxAllowedSize).ContinueWith((Task<byte[]> task) =>
          {
               if (task.IsFaulted || task.IsCanceled)
               {
                    Debug.Log(task.Exception.ToString());
                    Debug.Log("No case with this name");
                    
               }
               else
               {
                    Debug.Log("Finished downloading");
                    Stream stream = new MemoryStream(task.Result);
                    BinaryFormatter bf = new BinaryFormatter();
                    Case downloadedCase = (Case)bf.Deserialize(stream);
                    UIManager.Instance.activeCase = downloadedCase;
                    downloaded = true;
                    
               }

                    
               
          });


     }
}
