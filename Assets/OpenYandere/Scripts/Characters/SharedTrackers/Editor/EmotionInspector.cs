using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CanEditMultipleObjects]
[CustomEditor(typeof(EmotionTracker))]
public class EmotionInspector : Editor
{
    
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        EmotionTracker component = (EmotionTracker)target;
       
        if (component==null||component.data.value==null) return;// issue


        // EmotionTracker.emotionData.emotionKey allemotion = (EmotionTracker.emotionData.emotionKey[])Enum.GetValues(typeof(EmotionTracker.emotionData.emotionKey));



      

        foreach (EmotionTracker.emotionData.emotionKey e in Enum.GetValues(typeof(EmotionTracker.emotionData.emotionKey)))
        {
            
             // EditorGUILayout.IntField( e.ToString(), component.getEmotion(e));
            EditorGUILayout.LabelField(e.ToString().ToUpper());
             EditorGUILayout.IntSlider(component.getEmotion(e), 1, 100);
            //EditorGUI.ProgressBar(new Rect(0.1f, 0.5f+yheight, 0.1f, 0.1f), 50 / 100, e.ToString());
        }
        
        

        
       
    }
}