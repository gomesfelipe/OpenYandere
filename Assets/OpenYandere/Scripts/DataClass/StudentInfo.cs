using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OpenYandere.DataClass
{
    [CreateAssetMenu(fileName = "GossipData", menuName = "Student/GossipData")]
    public class StudentGossipInfo:ScriptableObject
    {
        // public StudentGossipInfo(int h = 123, string c = "dramma", string s = "???") { this.height = h; this.club = c; this.secert = s; }
        public int height;
        public string club;
        public string secert;
    }
}
