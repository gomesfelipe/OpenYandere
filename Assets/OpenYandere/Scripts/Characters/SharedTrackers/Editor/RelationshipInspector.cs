using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using OpenYandere.Characters.SharedTrackers;
using OpenYandere.Characters;
using OpenYandere.Managers.Traits;
using static OpenYandere.Characters.SharedTrackers.RelationshipTracker;

[CanEditMultipleObjects]
[CustomEditor(typeof(RelationshipTracker))]
public class RelationshipInspector : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        
        RelationshipTracker component = (RelationshipTracker)target;

        relationshipData allRelation = component.getAllRelation();
        if (allRelation == null) return;


        //float yheight=0;

        foreach (KeyValuePair<PrefabID, RelationshipTracker.relation> e in allRelation.data)
        {

            Character person;
            if (!GlobalDataStorage.Prefab_stuff.PrefabID_Dictionary.TryGetValue(e.Key.ID, out person)) continue;

            EditorGUILayout.LabelField(  person.characterName + " : ID( "+e.Key.ID+" )");
            EditorGUILayout.LabelField(e.Value.getAllRelation());
            //EditorGUILayout.IntSlider(component.getEmotion(e), 1, 100);
            //EditorGUI.ProgressBar(new Rect(0.1f, 0.5f+yheight, 0.1f, 0.1f), 50 / 100, e.ToString());
        }
        
        

        
       
    } 
}