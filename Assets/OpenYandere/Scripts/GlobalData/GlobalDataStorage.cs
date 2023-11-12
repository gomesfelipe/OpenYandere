using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OpenYandere.Managers.Traits;
using OpenYandere.Characters;
using OpenYandere.Characters.NPC.Prefabs;

public  class GlobalDataStorage : MonoBehaviour
{
    private static Dictionary<int, string> gossipDictionary= new Dictionary<int, string>();

    

    public static class Prefab_stuff

    {
        public static Dictionary<int, Character> PrefabID_Dictionary = new Dictionary<int, Character>();
        
        public static PrefabID makeNewID()
        {
            while (true)
            {
                int num = Mathf.RoundToInt(Random.Range(0,100000));
                if (!PrefabID_Dictionary.TryGetValue(num ,out _))
                {
                   PrefabID i= ScriptableObject.CreateInstance<PrefabID>();
                    i.ID = num;
                    return i;
                }
            }
        }
        public static Character IDtoCharacter(PrefabID values)
        {
          
            return PrefabID_Dictionary[values.ID];
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        Character[] list = FindObjectsByType<Character>(FindObjectsSortMode.None);

        for (int i = 0; i < list.Length; i++)
        {
            if (list[i].ID == null)
            {
                list[i].ID = Prefab_stuff.makeNewID();
            }
            Prefab_stuff.PrefabID_Dictionary.Add(list[i].ID.ID, list[i]);
        }

       
    }
    

    // Update is called once per frame
    void Update()
    {
       
    }
}
