using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OpenYandere.Managers.Traits;
using OpenYandere.Characters;
using OpenYandere.DataClass;
internal class StudentDataManager : Singleton<StudentDataManager>
{
  public StudentGossipDataBase SGD { get; private set; }

    private void Awake()
    {
        if (SGD == null) SGD = StudentGossipDataBase.Instance;
    }
}
