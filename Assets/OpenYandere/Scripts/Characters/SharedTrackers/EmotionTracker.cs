using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OpenYandere.Characters.SharedTrackers;
using System;
using OpenYandere.Characters;
public class EmotionTracker : Tracker
{
    public struct emotionData
    {
        public enum emotionKey
        {
            joy,
            sadness,
            irritation,
            fear,
            sanitiy,
        }
        public Dictionary<emotionKey,int> value;
        public int max;
        public int min;
  
        public emotionData(Dictionary<emotionKey,int> v,int ma=0, int mi=0)
        {
            value = v;
            max = ma;
            min = mi;
        }
        public static emotionData operator +(emotionData a, emotionData b)
        {
          foreach(KeyValuePair<emotionKey,int> k in b.value)
            {
                a.value[k.Key] = Math.Clamp(a.value[k.Key] + k.Value, a.min, a.max);
            }
            return new emotionData(a.value);
        }

        public static emotionData operator *(emotionData a, emotionData b)
        {
            foreach (KeyValuePair<emotionKey, int> k in b.value)
            {
                a.value[k.Key]= Math.Clamp( a.value[k.Key] * k.Value,a.min,a.max);
            }
            return new emotionData(a.value);
        }

        public static emotionData operator /(emotionData a, emotionData b)
        {
            foreach (KeyValuePair<emotionKey, int> k in b.value)
            {
                a.value[k.Key] = Math.Clamp(a.value[k.Key] / k.Value, a.min, a.max);
            }
            return new emotionData(a.value);
        }
    }
    
    public emotionData data { get; private set; }
   
    void Awake()
    {
        base.Awake();

        var allemotion = Enum.GetValues(typeof(emotionData));
        data =new emotionData(new Dictionary<emotionData.emotionKey, int> {
            //some default value
            {emotionData.emotionKey.joy,10 },
            
        });
      
    }

    private void Update()
    {
        base.Update();
    }

    
    public void updateEmotion(emotionData.emotionKey e, int i)
    {
        data.value[e] += i;
    }
    public void updateEmotion(emotionData ed)
    {
        data += ed;
    }

    public int getEmotion(emotionData.emotionKey e) { return data.value[e]; }
    public emotionData getAllEmotions() { return data; }

 
}
