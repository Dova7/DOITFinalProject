namespace ATMOperations.Constants
{
    public struct Error
    {
        public const string IdLength = "Invalid ID number. ID number must contain 11 digits.";
        public const string PinLenght = "Invalid Pin. Pin must contain 4 digits.";
        public const string DuplicateUser = "User already exists. Please try logging in instead.";
        public const string InvalidFirstName = "Invalid first name. First name cannot be empty or contain digits.";
        public const string InvalidLastName = "Invalid last name. Last name cannot be empty or contain digits.";
        public const string InvalidId = "Invalid ID number. ID number cannot be empty or contain letters and symbols.";
        public const string InvalidPin = "Invalid Pin. Pin cannot be empty or contain letters and symbols.";
    }
}