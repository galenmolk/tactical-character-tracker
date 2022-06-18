namespace JsonFactory
{
    public class Property<T>
    {
        public Property(string key, T value)
        {
            Key = key;
            Value = value;
        }

        public readonly string Key;
        public readonly T Value;
    }
}
