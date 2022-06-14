namespace EPharmacy.Domain.ValueObjects;

public static class CalculateAge
{
    public static int GetCurrentAge(DateTime DOB)
    {
        var today = DateTime.Today;
        var age = today.Year - DOB.Year;

        return IsLeapYear(DOB, today, age) ? age-- : age;
    }

    private static bool IsLeapYear(DateTime DOB, DateTime today, int age)
    {
        return (DOB.Date > today.AddYears(-age));
    }
}
