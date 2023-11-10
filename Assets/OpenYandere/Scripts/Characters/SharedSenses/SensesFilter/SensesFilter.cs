using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OpenYandere;

namespace OpenYandere.Characters.Sense
{
    public class SensesFilter
    {
        public enum Filter
        {
            friends,
            teacher,
            delinquent
        }
        public Filter filerMode { get; set; }
        public SensesFilter(Filter f)
        {
            filerMode = f;
        }


       public bool isTarget() { return true; }
    }
}
