
public record member(string name, int memberID);

public interface ILoanable
{
    Task CheckoutAsync(member m);
    Task ReturnAsync();
}

public abstract class LibraryItem : ILoanable
{
    public virtual async Task CheckoutAsync(member m)
    {
    }

    public virtual async Task ReturnAsync()
    {
    }
}

public struct ISBN
{
    private readonly string _num;

    public ISBN(string num)
    {
        _num = num;
    }

}

public class Book : LibraryItem
{
    public static Library Library { get; set; } = new Library();

    public override async Task CheckoutAsync(member m)
    {
        Library.AddItem(this);
        await Task.Delay(3000);
        Console.WriteLine("Book checked out.");
    }

    public override async Task ReturnAsync()
    {
        Console.WriteLine("Book returned.");
    }
}



public class Library
{

    private List<ILoanable> _items = new List<ILoanable>();

    public Dictionary<ILoanable, member> _track = new Dictionary<ILoanable, member>();

    public void AddItem(ILoanable item)
    {
        _items.Add(item);
    }

    public void trackP(ILoanable item, member m)
    {
        _track.Add(item, m);
    }

}


public class CheckoutSession : IAsyncDisposable
{

    private List<ILoanable> _items = new List<ILoanable>();

    public void AddItem(ILoanable item)
    {
        _items.Add(item);
    }
    public async Task CheckoutItem(member m)
    {
        foreach (var item in _items)
        {
            await item.CheckoutAsync(m);
        }
    }

    public async ValueTask DisposeAsync()
    {
        foreach (var item in _items)
        {
            await item.ReturnAsync();
        }
    }

}

public class Program
{
    public static async Task Main()
    {
        Book book1 = new Book();
        member member1 = new member("Anas", 1);

        await using var session = new CheckoutSession();
        session.AddItem(book1);

        await session.CheckoutItem(member1);

        Console.WriteLine("Session ended, items returned.");
    }
}