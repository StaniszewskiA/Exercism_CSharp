class Lasagna
{
    const int expected_minutes = 40;
    const int time_per_layer = 2;
    
    public int ExpectedMinutesInOven() => expected_minutes;
    public int RemainingMinutesInOven(int remaining) => expected_minutes - remaining;
    public int PreparationTimeInMinutes(int layers) => layers * time_per_layer;
    public int ElapsedTimeInMinutes(int layers, int elapsed) => PreparationTimeInMinutes(layers) + elapsed;
}
