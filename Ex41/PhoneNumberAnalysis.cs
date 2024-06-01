using System;
using System.Text.RegularExpressions;

public static class PhoneNumber
{
    public static (bool IsNewYork, bool IsFake, string LocalNumber) Analyze(string phoneNumber)
    {
        var phoneRegex = new Regex(@"^\d{3}-\d{3}-\d{4}$");

        if(!phoneRegex.IsMatch(phoneNumber))
        {
            throw new ArgumentException("Invalid phone number format.");
        }

        var parts = phoneNumber.Split('-');
        var areaCode = parts[0];
        var prefix = parts[1];
        var lineNumber = parts[2];

        bool isNewYork = areaCode == "212";
        bool isFake = prefix == "555";
        string localNumber = lineNumber;

        return (isNewYork, isFake, localNumber);
    }

    public static bool IsFake((bool IsNewYork, bool IsFake, string LocalNumber) phoneNumberInfo) => phoneNumberInfo.IsFake;

}
