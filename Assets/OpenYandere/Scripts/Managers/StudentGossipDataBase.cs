using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OpenYandere.Managers.Traits;
using OpenYandere.Characters;
using OpenYandere.DataClass;

internal class StudentGossipDataBase : Singleton<StudentGossipDataBase>
{
    public List<StudentGossipInfo> ListOfGossip = new();
    public List<Character> ListOfStudent = new();
    private Dictionary<Character, StudentGossipInfo> studendDict;

    void Start()
    {
        studendDict = new Dictionary<Character, StudentGossipInfo>();
        int i = 0;
        foreach (StudentGossipInfo c in ListOfGossip)
        {
           
            studendDict.Add(ListOfStudent[i],c);
            i++;
        }

    }

    public Dictionary<Character, StudentGossipInfo> getItems()
    {
        return studendDict;
    }

    public StudentGossipInfo getinfo(Character c) { return studendDict[c]; }
    public Character getinfo(int i) { return ListOfStudent[i]; }
}
