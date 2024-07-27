using System;

public class BankAccount
{
    private bool _open;
    private decimal _balance;
    private readonly object _lock = new object();
    
    public void Open() => ExecuteLockedVoid(() => _open = true);
    public void Close() => ExecuteLockedVoid(() => _open = false);

    public decimal Balance => 
        ExecuteLocked<decimal>(() =>
        {
            if (!_open) throw new InvalidOperationException("Cannot check balance on locked account.");     
            return _balance;
        });

    public void UpdateBalance(decimal change) => ExecuteLockedVoid(() => _balance += change);

    private void ExecuteLockedVoid(Action action)
    {
        lock (_lock) action.Invoke();
    }

    private T ExecuteLocked<T>(Func<T> func)
    {
        lock (_lock)
        {
            return func.Invoke();
        }
    }
}
