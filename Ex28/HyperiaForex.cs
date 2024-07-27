using System;

public struct CurrencyAmount
{
    private decimal amount;
    private string currency;

    public CurrencyAmount(decimal amount, string currency)
    {
        this.amount = amount;
        this.currency = currency;
    }

    // TODO: implement equality operators
    public static bool operator ==(CurrencyAmount a, CurrencyAmount b) => a.currency == b.currency ? a.amount == b.amount : throw new ArgumentException();
    public static bool operator !=(CurrencyAmount a, CurrencyAmount b) => a.currency == b.currency ? a.amount != b.amount : throw new ArgumentException();

    // TODO: implement comparison operators
    public static bool operator <(CurrencyAmount a, CurrencyAmount b) => a.currency == b.currency ? a.amount < b.amount : throw new ArgumentException();
    public static bool operator >(CurrencyAmount a, CurrencyAmount b) => a.currency == b.currency ? a.amount > b.amount : throw new ArgumentException();
    public static bool operator <=(CurrencyAmount a, CurrencyAmount b) => a.currency == b.currency ? a.amount <= b.amount : throw new ArgumentException();
    public static bool operator >=(CurrencyAmount a, CurrencyAmount b) => a.currency == b.currency ? a.amount >= b.amount : throw new ArgumentException();

    // TODO: implement arithmetic operators
    public static CurrencyAmount operator +(CurrencyAmount a, CurrencyAmount b) => a.currency == b.currency ? new CurrencyAmount(a.amount + b.amount, a.currency) : throw new ArgumentException();
    public static CurrencyAmount operator -(CurrencyAmount a, CurrencyAmount b) => a.currency == b.currency ? new CurrencyAmount(a.amount - b.amount, a.currency) : throw new ArgumentException();
    public static CurrencyAmount operator *(CurrencyAmount a, decimal m) => new CurrencyAmount(a.amount * m, a.currency);
    public static CurrencyAmount operator /(CurrencyAmount a, decimal m) => new CurrencyAmount(a.amount / m, a.currency);

    // TODO: implement type conversion operators
    public static explicit operator double(CurrencyAmount a) => (double) a.amount;
    public static implicit operator decimal(CurrencyAmount a) => a.amount;
}
