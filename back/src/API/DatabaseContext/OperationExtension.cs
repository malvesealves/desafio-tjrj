namespace API.DatabaseContext
{
    public static class OperationExtension
    {
        public static void UpdateIfNotNull<T>(ref T target, T? source) where T : class
        {
            if (source != null)
                target = source;
        }

        public static void UpdateIfNotNull<T>(ref T target, T? source) where T : struct
        {
            if (source.HasValue)
                target = source.Value;
        }
    }
}
