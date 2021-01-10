namespace Malefics.Extensions
{
    public static class Pair
    {
        public static T First<T, S>(this (T, S) pair) => pair.Item1;
        public static S Second<T, S>(this (T, S) pair) => pair.Item2;
    }
}
