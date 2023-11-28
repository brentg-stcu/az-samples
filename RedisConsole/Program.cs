using StackExchange.Redis;
using System.Text.Json;

string connection = "brentg-redis.redis.cache.windows.net:6380,password=UsHXgDeSSYT7vIHaLM2xWJ1fV8kuLOs2LAzCaI2oudw=,ssl=True,abortConnect=False";
ConnectionMultiplexer redis = ConnectionMultiplexer.Connect(connection);

Console.WriteLine("getting product...");
var item = GetItem();


Console.WriteLine("setting new product...");
var product = new Product { Id = 1, Name = "Product 1" };
CacheItem(product);

Console.WriteLine("getting product...");
item = GetItem();
Console.WriteLine(item);
Console.WriteLine("done");


void CacheItem(Product product)
{
    var db = redis.GetDatabase();
    db.StringSet("top:products", JsonSerializer.Serialize(product));
    //db.ListRightPush("top:products", JsonSerializer.Serialize(product));

    Console.WriteLine("Item cached");
}

Product? GetItem()
{
    var db = redis.GetDatabase();
    var item = db.StringGet("top:products");

    if (!item.HasValue)
    {
        Console.WriteLine("Item not found");
        return null;
    }


    Console.WriteLine(item);
    var product = JsonSerializer.Deserialize<Product>(item);
    return product;
}

public class Product
{
    public int Id { get; set; }
    public string Name { get; set; }
}