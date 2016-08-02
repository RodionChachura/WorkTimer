﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XamarinWorkTimer
{
    public static class g
    {
        public static string item = "item";
        public static string interval = "interval";
        public static string sum = "sum";
        public static string slider = "slider";
        public static string lastTime = "lastTime";
        public static string dateFormat = "dd/MM/yyyy";
        public static string timeFormat = "hh/mm/ss";

        public static string FromSecondsToString(int seconds)
        {
            TimeSpan sec = TimeSpan.FromSeconds(seconds);

            if (seconds < 3600)
                return sec.ToString(@"mm\:ss");
            else if (seconds == 3600)
                return "60:00";
            else
                return sec.ToString(@"hh\:mm\:ss");
        }
    }
}
