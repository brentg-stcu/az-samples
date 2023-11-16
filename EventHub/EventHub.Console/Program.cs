using EventHub.Console;


var emitter = new Emitter();
var devices = GenerateDevices(10);

foreach (var device in devices)
{
    Console.WriteLine($"Device {device.Id}: {device.Tempurature}°C");
}

await emitter.SendData(devices);
Console.WriteLine("Data sent.");



List<Device> GenerateDevices(int count)
{
    var devices = new List<Device>();
    var random = new Random();

    for (int i = 0; i < count; i++)
    {
        var device = new Device
        {
            Id = i + 1,
            Tempurature = (float)random.NextDouble() * 100
        };
        devices.Add(device);
    }

    return devices;
}



