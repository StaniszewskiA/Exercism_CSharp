using System;

public class SpaceAge
{
    private readonly double seconds;
    private const double EarthOrbitalPeriodInSeconds = 31557600;
    private const double MercuryOrbitalPeriod = 0.2408467;
    private const double VenusOrbitalPeriod = 0.61519726;
    private const double MarsOrbitalPeriod = 1.8808158;
    private const double JupiterOrbitalPeriod = 11.862615;
    private const double SaturnOrbitalPeriod = 29.447498;
    private const double UranusOrbitalPeriod = 84.016846;
    private const double NeptuneOrbitalPeriod = 164.79132;
    
    public SpaceAge(int seconds)
    {
        this.seconds = seconds;
    }

    public double OnEarth() => seconds / EarthOrbitalPeriodInSeconds;
    
    public double OnMercury() => OnEarth() / MercuryOrbitalPeriod;
    
    public double OnVenus() => OnEarth() / VenusOrbitalPeriod;
    
    public double OnMars() => OnEarth() / MarsOrbitalPeriod;
    
    public double OnJupiter() => OnEarth() / JupiterOrbitalPeriod;

    public double OnSaturn() => OnEarth() / SaturnOrbitalPeriod;

    public double OnUranus() => OnEarth() / UranusOrbitalPeriod;

    public double OnNeptune() => OnEarth() / NeptuneOrbitalPeriod;
}