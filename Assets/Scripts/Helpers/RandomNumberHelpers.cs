using System.ComponentModel;
using UnityEngine;

namespace Assets.Scripts.Helpers
{
    public static class RandomNumberHelpers
    {
        public static float GetRandomNumber(float min, float max)
        {
            System.Random rand = new System.Random(System.Guid.NewGuid().GetHashCode());
            var num = min + (rand.NextDouble() * (max - min));

            return (float)num;
        }
        public static int GetRandomNumber(int min, int max)
        {
            System.Random rand = new System.Random(System.Guid.NewGuid().GetHashCode());
            return rand.Next(min, max);
        }
    }
}