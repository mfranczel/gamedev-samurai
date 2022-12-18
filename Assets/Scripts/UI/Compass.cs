using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Xml.XPath;
using TMPro;
using UnityEngine;

public class Compass : MonoBehaviour
{
    public List<CompassElement> compassElements;
    
    public RectTransform northTransform;
    public RectTransform southTransform;
    public RectTransform westTransform;
    public RectTransform eastTransform;
    
    public RectTransform enemyTransform;
    public RectTransform questTransform;
    
    public Transform player;

    public Transform cameraObjectTransform;
    public RectTransform compassBarTransform;

    

    // Update is called once per frame
    void Update()
    {
        SetMarker(northTransform, Vector3.forward * 1000 );
        SetMarker(westTransform, Vector3.left * 1000);
        SetMarker(eastTransform, Vector3.right * 1000);
        SetMarker(southTransform, Vector3.back * 1000);
        
        foreach(CompassElement compassElement in compassElements)
        {
            SetMarker(compassElement.compassElementTransform, compassElement.objectToFollow.transform.position);
        }
    }
    
    private void SetMarker(RectTransform markerTransform, Vector3 worldPosition)
    {
        Vector2 playerPos = new Vector2(player.transform.forward.x, player.transform.forward.z);
        Vector2 worldFwd = new Vector2(worldPosition.x, worldPosition.z);
        
        float angle = -Vector2.SignedAngle(playerPos, worldFwd) / 90f;
        angle *= compassBarTransform.rect.width;
        angle = Mathf.Clamp(angle, -compassBarTransform.rect.width / 1.5f, compassBarTransform.rect.width / 1.5f);
        markerTransform.anchoredPosition = new Vector2(angle, 0);
    }

    public int AddCompassEnemy(GameObject go)
    {
        CompassElement compassElement = new CompassElement();
        enemyTransform.position.Set(0,0,0);
        compassElement.compassElementTransform = enemyTransform;
        compassElement.objectToFollow = go;
        compassElements.Add(compassElement);
        
        return compassElements.Count - 1;
    }
    
    public int AddCompassQuest(GameObject go)
    {
        CompassElement compassElement = new CompassElement();
        questTransform.position.Set(0,0,0);
        compassElement.compassElementTransform = questTransform;
        compassElement.objectToFollow = go;
        compassElements.Add(compassElement);
        
        return compassElements.Count - 1;
    }
    
    public bool RemoveCompassElement(int compassElementIndex)
    {
        if (compassElements.Count > compassElementIndex)
        {
            CompassElement compassElement = compassElements[compassElementIndex];
            compassElement.compassElementTransform.position.Set(0, 5, 0);
            compassElements.RemoveAt(compassElementIndex);
            return true;
        }
        
        return false;
    }
}
