using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OpenYandere.Characters.SharedTrackers;
using System;
using OpenYandere.Characters;
namespace OpenYandere.Characters.SharedTrackers
{
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
            public Dictionary<emotionKey, int> value;
            public int joy { get { return value[emotionKey.joy]; } set { this.value[emotionKey.joy] = value; } }
            public int sadness { get { return value[emotionKey.sadness]; } set { this.value[emotionKey.sadness] = value; } }
            public int irritation { get { return value[emotionKey.irritation]; } set { this.value[emotionKey.irritation] = value; } }
            public int fear { get { return value[emotionKey.fear]; } set { this.value[emotionKey.fear] = value; } }
            public int sanitiy { get { return value[emotionKey.sanitiy]; } set { this.value[emotionKey.sanitiy] = value; } }
            public int max;
            public int min;

            public emotionData(Dictionary<emotionKey, int> v, int ma = 0, int mi = 0)
            {
                Dictionary<emotionKey, int> newList = new Dictionary<emotionKey, int>();
                foreach (emotionKey e in Enum.GetValues(typeof(emotionKey)))
                {
                    newList[e] = v.TryGetValue(e, out int num) ? num : 50;
                }
                value = newList;
                max = ma;
                min = mi;
            }
            public static emotionData operator +(emotionData a, emotionData b)
            {
                foreach (KeyValuePair<emotionKey, int> k in b.value)
                {
                    a.value[k.Key] = Math.Clamp(a.value[k.Key] + k.Value, a.min, a.max);
                }
                return new emotionData(a.value);
            }

            public static emotionData operator *(emotionData a, emotionData b)
            {
                foreach (KeyValuePair<emotionKey, int> k in b.value)
                {
                    a.value[k.Key] = Math.Clamp(a.value[k.Key] * k.Value, a.min, a.max);
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

            // var allemotion = Enum.GetValues(typeof(emotionData));
            data = new emotionData(new Dictionary<emotionData.emotionKey, int> {
            //some default value
            {emotionData.emotionKey.joy,10 },
            {emotionData.emotionKey.fear,10 },
            {emotionData.emotionKey.irritation,10 },
            {emotionData.emotionKey.sanitiy,10 },
            {emotionData.emotionKey.sadness,10 },

        });

        }

        private void Update()
        {
            base.Update();
        }

        public override bool IsThatAppealingToMe(SharedMind.Mind.Reaction data)
        {
            if (data.emotionReaction.joy > 50)
            {
                return true;
            } else

            {
                return false;
            }
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



    }
}
