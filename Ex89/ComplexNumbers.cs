using System;
using System.Numerics;

public struct ComplexNumber
{
    private double real;
    private double imag;
    
    public ComplexNumber(double real, double imaginary)
    {
        this.real = real;
        this.imag = imaginary;
    }

    public double Real() => this.real;

    public double Imaginary() => this.imag;

    public ComplexNumber Mul(ComplexNumber other)
    {
        double newReal = this.real * other.real - this.imag * other.imag;
        double newImag = this.imag * other.real + this.real * other.imag;

        return new ComplexNumber(newReal, newImag);
    }

    public ComplexNumber Mul(double other) =>
        this.Scale(other);

    public ComplexNumber Add(ComplexNumber other) =>
        new ComplexNumber(this.real + other.real, this.imag + other.imag);

    public ComplexNumber Add(double other) =>
        new ComplexNumber(this.real + other, this.imag);

    public ComplexNumber Sub(ComplexNumber other) =>
        new ComplexNumber(this.real - other.real, this.imag - other.imag);

    public ComplexNumber Div(ComplexNumber other) 
    {
        var factor = other.Mul(other.Conjugate());
        if (Math.Abs(factor.imag) != 0) throw new Exception("Something is wrong");

        return this.Mul(other.Conjugate()).Scale(1.0 / factor.real);
    }

    public ComplexNumber Div(double other) =>
        this.Scale(1.0 / other);

    public double Abs() =>
        Math.Sqrt(this.Mul(this.Conjugate()).real);

    public ComplexNumber Conjugate() =>
        new ComplexNumber(this.real, -1 * this.imag);
    
    public ComplexNumber Exp()
    {
        double realPart = Math.Exp(this.real);
        ComplexNumber imagPart = new ComplexNumber(Math.Cos(this.imag), Math.Sin(this.imag));

        return imagPart.Scale(realPart);
    }

    #region Private
    private ComplexNumber Scale(double factor) =>
        new ComplexNumber(this.real * factor, this.imag * factor);

    #endregion
}