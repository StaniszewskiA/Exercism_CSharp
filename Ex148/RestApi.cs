using System;
using System.Collections.Generic;
using System.Linq;

using Newtonsoft.Json;

public class RestApi
{
    public RestApi(string database) => User.ParseDatabase(database);

    public string Get(string url, string payload = null) => payload is null
        ? JsonConvert.SerializeObject(User.Users)
        : JsonConvert.SerializeObject(User.GetByName(JsonConvert.DeserializeObject<GetDto>(payload).Users));

    public string Post(string url, string payload)
    {
        if (string.IsNullOrWhiteSpace(payload))
            return null;

        var urlActions = new Dictionary<string, Func<string, string>>
        {
            { "/add", ProcessAddUser },
            { "/iou", ProcessAddIou }
        };


        if (!urlActions.TryGetValue(url, out var action))
            throw new Exception("Unrecognized url.");

        return action(payload);
    }

    private string ProcessAddUser(string payload)
    {
        var userDto = JsonConvert.DeserializeObject<AddUserDto>(payload);
        var result = User.AddUser(userDto.User);
        return JsonConvert.SerializeObject(result);
    }

    private string ProcessAddIou(string payload)
    {
        var iouDto = JsonConvert.DeserializeObject<AddIouDto>(payload);
        var result = User.AddIou(iouDto);
        return JsonConvert.SerializeObject(result);
    }
}

public class User
{
    public static List<User> Users = new();

    [JsonProperty("name")]
    public string Name { get; private set; }

    [JsonProperty("owes")]
    public Dictionary<string, int> Owes { get; private set; }

    [JsonProperty("owed_by")]
    public Dictionary<string, int> OwedBy { get; private set; }

    [JsonProperty("balance")]
    public int Balance { get => CalculateBalance(); }

    public User(string name)
    {
        Name = name;
        Owes = [];
        OwedBy = [];
        Users.Add(this);
    }

    public static void ParseDatabase(string database) => Users = JsonConvert.DeserializeObject<List<User>>(database);

    public static List<User> GetByName(string[] users) => Users.Where(user => users.Contains(user.Name)).ToList<User>();

    public static User AddUser(string name) => new(name);

    public static User[] AddIou(AddIouDto iouDto)
    {
        User lender = Users.FirstOrDefault(user => user.Name == iouDto.Lender);
        User borrower = Users.FirstOrDefault(user => user.Name == iouDto.Borrower);
        int amount = iouDto.Amount;

        if (lender.OwedBy.Keys.Contains(borrower.Name))
        {
            lender.OwedBy[borrower.Name] += amount;
            borrower.Owes[lender.Name] += amount;
        }
        else if (lender.Owes.Keys.Contains(borrower.Name))
        {
            if (lender.Owes[borrower.Name] > amount)
            {
                lender.Owes[borrower.Name] -= amount;
                borrower.OwedBy[lender.Name] -= amount;
            }
            else if (lender.Owes[borrower.Name] == amount)
            {
                lender.Owes.Remove(borrower.Name);
                borrower.OwedBy.Remove(lender.Name);
            }
            else
            {
                int remainder = amount - lender.Owes[borrower.Name];

                lender.Owes.Remove(borrower.Name);
                lender.OwedBy[borrower.Name] = remainder;

                borrower.OwedBy.Remove(lender.Name);
                borrower.Owes[lender.Name] = remainder;
            }
        }
        else
        {
            lender.OwedBy[borrower.Name] = amount;
            borrower.Owes[lender.Name] = amount;
        }

        lender.OwedBy = lender.OwedBy.OrderBy(key => key.Key).ToDictionary();
        borrower.Owes = borrower.Owes.OrderBy(key => key.Key).ToDictionary();

        User[] involved = [lender, borrower];
        return involved.OrderBy(user => user.Name).ToArray();
    }

    private int CalculateBalance() => OwedBy.Values.Sum() - Owes.Values.Sum();
}

public class GetDto
{
    [JsonProperty("users")]
    public string[] Users { get; set; }
}

public class AddUserDto
{
    [JsonProperty("user")]
    public string User { get; set; }
}

public class AddIouDto
{
    [JsonProperty("lender")]
    public string Lender { get; set; }

    [JsonProperty("borrower")]
    public string Borrower { get; set; }

    [JsonProperty("amount")]
    public int Amount { get; set; }
}