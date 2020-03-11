namespace Capstone.Common
{
    public static class StringUtils
    {
        /// <summary>
        /// Checks if the passed string is null or blank, by checking if the string is equal to "" once it has been trimmed.
        /// </summary>
        /// <param name="ToCheck">The string to check if it's blank</param>
        /// <returns><code>true</code> if the string is null or blank, <code>false</code> otherwise</returns>
        public static bool IsBlank(string ToCheck)
        {
            bool isBlank = false;
            if (ToCheck is null || ToCheck.Trim().Equals(""))
            {
                isBlank = true;
            }
            return isBlank;
        }

        public static bool IsNotBlank(string ToCheck) => !IsBlank(ToCheck);
    }
}
