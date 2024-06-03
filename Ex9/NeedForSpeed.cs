using System;

class RemoteControlCar
{
    private int speed;
    private int batteryDrain;
    private int battery;
    private int distanceDriven;

    private const int maxBattery = 100;

    public RemoteControlCar(int speed, int batteryDrain)
    {
        this.speed = speed;
        this.batteryDrain = batteryDrain;
        this.battery = maxBattery;
        this.distanceDriven = 0;
    }

    public bool BatteryDrained() => this.battery < this.batteryDrain;
    
    public int DistanceDriven() => this.distanceDriven;

    public void Drive()
    {
        if (!BatteryDrained())
        {
            this.battery -= this.batteryDrain;
            this.distanceDriven += this.speed;
        }
    }

    public static RemoteControlCar Nitro() => new RemoteControlCar(50, 4);
}

class RaceTrack
{
    private int distance; 
        
    public RaceTrack(int distance)
    {
        this.distance = distance;
    }

    public bool TryFinishTrack(RemoteControlCar car)
    {
        while (!car.BatteryDrained())
        {
            car.Drive();
        }
        return car.DistanceDriven() >= this.distance;
    }
}
