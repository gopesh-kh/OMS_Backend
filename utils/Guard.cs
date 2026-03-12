namespace OMS_Backend.Utils
{
    public static class Guard
    {
        public static bool IsNull(object? obj)
        {
            return obj == null;
        }

        public static bool IsNullOrEmpty(string? value)
        {
            return string.IsNullOrWhiteSpace(value);
        }

        public static bool IsInvalidId(int id)
        {
            return id <= 0;
        }

        public static bool IsNegative(decimal value)
        {
            return value < 0;
        }

        public static bool IsNegative(int value)
        {
            return value < 0;
        }

        public static bool IsNullOrEmptyCollection<T>(ICollection<T>? collection)
        {
            return collection == null || collection.Count == 0;
        }
    }
}