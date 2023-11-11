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

        if (component.getAllEmotions() == null) return;


        var allemotion = Enum.GetValues(typeof(EmotionTracker.emotionData));

        float yheight=0;

       

        foreach (EmotionTracker.emotionData e in allemotion)
        {
            yheight += 5;
            // EditorGUILayout.IntField( e.ToString(), component.getEmotion(e));
            EditorGUILayout.LabelField(e.ToString().ToUpper());
            EditorGUILayout.IntSlider(component.getEmotion(e), 1, 100);
            //EditorGUI.ProgressBar(new Rect(0.1f, 0.5f+yheight, 0.1f, 0.1f), 50 / 100, e.ToString());
        }
        
        

        
       
    }
}