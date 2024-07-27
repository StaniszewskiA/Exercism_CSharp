using System;

public class Orm : IDisposable
{
    private Database _database;

    public Orm(Database database) => this._database = database;

    public void Begin()
    {
        try
        {
            this._database.BeginTransaction();
        }
        catch
        {
            this._database.Dispose();
        }
    }

    public void Write(string data)
    {
        try
        {
            this._database.Write(data);
        }
        catch
        {
            this._database.Dispose();
        }
    }

    public void Commit()
    {
        try
        {
            this._database.EndTransaction();
        }
        catch
        {
            this._database.Dispose();
        }
    }

    public void Dispose() => this._database.Dispose();
}
