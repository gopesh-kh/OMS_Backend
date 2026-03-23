namespace OMS_Backend.Utils
{
    public static class Guard
    {
        public static bool IsNull(object? obj) => obj is null;

        public static bool IsNullOrWhiteSpace(string? value) => string.IsNullOrWhiteSpace(value);

        public static bool IsInvalidId(int id) => id <= 0;

        public static bool IsNegative(int value) => value < 0;

        public static bool IsNegative(decimal value) => value < 0;

        public static bool IsNullOrEmptyCollection<T>(ICollection<T>? collection) =>
            collection == null || collection.Count == 0;

        public static bool IsNullOrEmptyEnumerable<T>(IEnumerable<T>? collection) =>
            collection == null || !collection.Any();
    }
}