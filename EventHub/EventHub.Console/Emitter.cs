using Azure.Messaging.EventHubs;
using Azure.Messaging.EventHubs.Producer;
using System.Text;
using System.Text.Json;

namespace EventHub.Console;

public class Emitter
{
    private readonly string connection = "";
    private readonly string eventHubName = "eventhub-devices";


    public async Task SendData(List<Device> devices)
    {
        var client = new EventHubProducerClient(connection, eventHubName);
        var batch = await client.CreateBatchAsync();

        devices.ForEach(device =>
        {
            var data = JsonSerializer.Serialize(device);
            if (!batch.TryAdd(new EventData(Encoding.UTF8.GetBytes(data))))
                throw new Exception($"The event could not be added.");
        });

        await client.SendAsync(batch);
        await client.DisposeAsync();
    }

}
