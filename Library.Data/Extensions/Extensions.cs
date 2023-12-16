public static class ListUtils
{
    public static List<T> ListOf<T>(params T[] items) => new (items);
}