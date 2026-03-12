namespace OMS_Backend.utils
{
    public class PasswordChecker
    {
        public static bool IsPasswordStrong(string password)
        {
            int minLength = 8;
            
            if (string.IsNullOrEmpty(password) || password.Length < minLength)
                return false;

            if (!password.Any(char.IsUpper))
                return false;

            if (!password.Any(char.IsLower))
                return false;

            if (!password.Any(char.IsDigit))
                return false;

            if (password.All(char.IsLetterOrDigit))
                return false;

            return true; 
        }
    }
}
