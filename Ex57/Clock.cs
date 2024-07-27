using System;

public class Clock
{
    private readonly DateTime time;   
    public Clock(int hours, int minutes)
    {
        var now = DateTime.Now;
        time = new DateTime(now.Year, now.Month, now.Day, 0, 0, 0);

        time = time.AddHours(hours);
        time = time.AddMinutes(minutes);
    }

    private Clock(DateTime time) => this.time = time;
    public override int GetHashCode() => base.GetHashCode();

    public Clock Add(int minutesToAdd)
    {
        var newTime = time.AddMinutes(minutesToAdd);
        return new Clock(newTime);
    }

    public Clock Subtract(int minutesToSubtract)
    {
        var newTime = time.AddMinutes(minutesToSubtract * -1);
        return new Clock(newTime);
    }

    public override string ToString() => time.ToString("HH:mm");
    public override bool Equals(object obj) => ToString() == obj.ToString();
}
