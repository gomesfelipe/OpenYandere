using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OpenYandere.Characters.SharedTrackers;
using System;
using OpenYandere.Characters;
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
        base.Awake();

        var allemotion = Enum.GetValues(typeof(emotion));
        emotions =new Dictionary<emotion,int> ();
        foreach (emotion e in allemotion )
        {
            emotions.Add(e, 1);         
        }
    }

    private void Update()
    {
        base.Update();
    }

    
    public void updateEmotion(emotion e, int i)
    {
        emotions[e] = i;
    }

    public int getEmotion(emotion e) { return emotions[e]; }
    public Dictionary<emotion,int> getAllEmotions() { return emotions; }
}
