using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName ="PrefabID",menuName ="PrefabID/ID",order =1)]
public class PrefabID : ScriptableObject
{ 
    public int ID;
    [Header("RANDOM: This will generate a Random ID! Game wont remember this student so only activiate it for testing!")]
    public bool RANDOMID;
   

  
}
