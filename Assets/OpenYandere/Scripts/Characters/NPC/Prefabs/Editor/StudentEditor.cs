using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using OpenYandere.Characters.NPC.Prefabs;
using OpenYandere.Characters.NPC;

#if UNITY_EDITOR
using UnityEditor;

[CustomEditor(typeof(Student))]
public class StudentEditor : UnityEditor.Editor
{
    public override void OnInspectorGUI()
    {
        // Desenha o Inspetor padrão
        DrawDefaultInspector();

        NPC student = (NPC)target;

        // Aqui você pode adicionar controles personalizados para editar sua rotina, atividades, etc.
        if (GUILayout.Button("Add New Activity"))
        {
            //insert cool Activity Adding
            // Adicionar nova atividade ao Student aqui...
        }
    }
}
#endif
