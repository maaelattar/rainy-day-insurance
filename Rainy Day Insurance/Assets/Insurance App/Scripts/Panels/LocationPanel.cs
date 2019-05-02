using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class LocationPanel : MonoBehaviour, IPanel
{
     public RawImage mapImg;
     public InputField mapNotes;
     public Text caseNumberTitle;

     public string apiKey;
     public float latitude, longitude;

     public string url = "https://dev.virtualearth.net/REST/V1/Imagery/Map/Road/";
     
     private void OnEnable()
     {
          caseNumberTitle.text = "CASE NUMBER " + UIManager.Instance.activeCase.caseID;
     }


     IEnumerator Start()
     {
          if (!Input.location.isEnabledByUser) { 
               Debug.Log("Can not get location services");
          }

          Input.location.Start(10, 0.1f);

          int maxWait = 20;
          while (Input.location.status == LocationServiceStatus.Initializing && maxWait > 0)
          {
               yield return new WaitForSeconds(1);
               maxWait--;
          }

          if (maxWait < 1)
          {
               print("Timed out");
          }

          if (Input.location.status == LocationServiceStatus.Failed)
          {
               print("Unable to determine device location");
          }
          else
          {
               print("Location: " + Input.location.lastData.latitude + " " + Input.location.lastData.longitude + " " + Input.location.lastData.altitude + " " + Input.location.lastData.horizontalAccuracy + " " + Input.location.lastData.timestamp);
               latitude = Input.location.lastData.latitude;
               longitude = Input.location.lastData.longitude;
          }

          Input.location.Stop();
          StartCoroutine(GetLocationRoutine());
     }


     IEnumerator GetLocationRoutine()
     {
          url += $"{latitude},{longitude}/16?mapSize=350,350&key={apiKey}";

          using (UnityWebRequest map = UnityWebRequest.Get(url))
          {
               Debug.Log("Location url: " + url);
               yield return map.SendWebRequest();

               if (map.error != null)
               {
                    Debug.LogError("Map Error: " + map.error);
               }

               mapImg.texture = UIManager.Instance.LoadImage(map.downloadHandler.data);     
               UIManager.Instance.activeCase.mapImage = map.downloadHandler.data;
               

          }
     }
     public void ProcessInfo()
     {
          if (string.IsNullOrEmpty(mapNotes.text) == false)
          {
               UIManager.Instance.activeCase.locationNotes = mapNotes.text;
          }
     }
}
