using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OpenYandere.Managers.Traits;
using OpenYandere.Characters;
using OpenYandere.DataClass;
internal class StudentDataManager : Singleton<StudentDataManager>
{
    public List<Character> tempList = new();
    private Dictionary<Character, StudentInfo> studendDict;

    void Start()
    {
        studendDict = new Dictionary<Character, StudentInfo>();
        foreach (Character c in tempList)
        {

            studendDict.Add(c, new StudentInfo(s:"likes to do the wave!"));
        }
        
    }

    public Dictionary<Character,StudentInfo> getItems()
    {
        return studendDict;
    }

    public StudentInfo getinfo(Character c) { return studendDict[c];  }
    public Character getinfo(int i) { return tempList[i]; }
}
