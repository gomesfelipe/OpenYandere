using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OpenYandere.Characters.SharedTrackers;
public class EmotionTracker : Tracker
{
    public enum emotion
    {
        joy,
        sadness,
        irritation,
        fear,
        sanitiy
    }

    public Dictionary<emotion, int> emotions { get; private set; }
    
    void Awake()
    {
        emotions=new Dictionary<emotion,int> ();
    }

    private void Update()
    {

    }

    
    public void updateEmotion(emotion e, int i)
    {
        emotions[e] = i;
    }

   
}
