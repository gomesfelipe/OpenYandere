using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using OpenYandere.Characters.SharedTrackers;
using OpenYandere.Characters;

[CanEditMultipleObjects]
[CustomEditor(typeof(RelationshipTracker))]
public class RelationshipInspector : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        RelationshipTracker component = (RelationshipTracker)target;

        var allRelation = component.getAllRelation();
        if (allRelation == null) return;


        

        //float yheight=0;

       

        foreach (KeyValuePair<Character, RelationshipTracker.relation> e in allRelation)
        {
            //yheight += 5;
            // EditorGUILayout.IntField( e.ToString(), component.getEmotion(e));
            EditorGUILayout.LabelField(e.Key.characterName+" : ID( "+e.Key.Id+" )");
            EditorGUILayout.LabelField(e.Value.getAllRelation());
            //EditorGUILayout.IntSlider(component.getEmotion(e), 1, 100);
            //EditorGUI.ProgressBar(new Rect(0.1f, 0.5f+yheight, 0.1f, 0.1f), 50 / 100, e.ToString());
        }
        
        

        
       
    }
}