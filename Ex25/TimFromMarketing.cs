using System;

static class Badge
{
    public static string Print(int? id, string name, string? department)
    {
        string departmentString = department?.ToUpper() ?? "OWNER";
        string badgeInfo = id.HasValue ? $"[{id}] - {name} - {departmentString}" : $"{name} - {departmentString}";


        return badgeInfo;
    }
}
