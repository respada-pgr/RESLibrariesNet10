using System;

namespace _1_LibraryClasses
{
    /// <summary>
    /// Thread wait helpers (fixed and random delays).
    /// <para><i>[ES] Ayudas de espera de hilo (retardos fijos y aleatorios).</i></para>
    /// </summary>
    public static class C_Hlp_Timer
    {
        /// <summary>Blocks the current thread for the given milliseconds (no-op if ≤ 0). / Bloquea el hilo actual los milisegundos indicados (no-op si ≤ 0).</summary>
        public static void Wait(int t_wait)
        {
            if (t_wait > 0)
            {
                System.Threading.Thread.Sleep(t_wait);
            }
        }

        /// <summary>Waits a random number of milliseconds. / Espera un número aleatorio de milisegundos.</summary>
        public static void RandomWait(int maxValue)
        {
            int t_wait = new Random().Next(maxValue);
            Wait(t_wait);
        }

        /// <summary>Waits a random number of milliseconds. / Espera un número aleatorio de milisegundos.</summary>
        public static void RandomWait(int minValue, int maxValue)
        {
            int t_wait = new Random().Next(minValue, maxValue);
            Wait(t_wait);
        }
    }
}
