using System;

namespace _2_LibraryUtils.Timer
{
    public static class C_Timer
    {
        public static void Wait(int t_wait)
        {
            if (t_wait > 0)
            {
                System.Threading.Thread.Sleep(t_wait);
            }
        }

        public static void RandomWait(int maxValue)
        {
            int t_wait = new Random().Next(maxValue);
            Wait(t_wait);
        }

        public static void RandomWait(int minValue, int maxValue)
        {
            int t_wait = new Random().Next(minValue, maxValue);
            Wait(t_wait);
        }
    }
}
