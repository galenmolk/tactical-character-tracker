namespace Ebla.Utils
{
    public static class OptionUtils
    {
        private const string NO = "NO";
        private const string YES = "YES";

        public static string GetStringForBool(bool value)
        {
            return value ? YES : NO;
        }
    }
}
