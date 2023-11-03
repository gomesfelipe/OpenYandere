using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OpenYandere.DataClass
{

    public class StudentInfo
    {
        public StudentInfo(int h = 123, string c = "dramma", string s = "???") { this.height = h; this.club = c; this.secert = s; }
        public int height { get; private set; }
        public string club { get; private set; }

        public string secert { get; private set; }
    }
}
