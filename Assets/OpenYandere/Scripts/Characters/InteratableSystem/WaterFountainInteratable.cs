using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OpenYandere.Characters.SharedMind;
using OpenYandere.Characters.SharedTrackers;

namespace OpenYandere.Characters.Interactions.InteractableCompoents
{
    public class WaterFountainInteratable : InteratableCompoent
    {
        // Start is called before the first frame update

        private void Awake()
        {
            base.Awake();
            base.InteratableList.value.Add(0, new InteractableInfo(name: "DrinkWater",
                incentives: new Mind.Reaction(new EmotionTracker.emotionData(new Dictionary<EmotionTracker.emotionData.emotionKey, int>()
                {
                    {EmotionTracker.emotionData.emotionKey.joy,100 },
                    {EmotionTracker.emotionData.emotionKey.sanitiy,100 }
                })


                ),max:1));
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}
