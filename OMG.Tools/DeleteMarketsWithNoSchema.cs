using Microsoft.Azure.Cosmos;
public class DeleteMarketsWithNoSchema
{
    private static readonly string EndpointUri = "https://omgdb.documents.azure.com:443/";
    private static readonly string PrimaryKey = "<your-primary-key>";
    private static readonly string DatabaseId = "OpenMarketGuide";
    private static readonly string ContainerId = "Markets";

    public static async Task Main(string[] args)
    {
        using CosmosClient cosmosClient = new CosmosClient(EndpointUri, PrimaryKey);
        Microsoft.Azure.Cosmos.Container container = cosmosClient.GetContainer(DatabaseId, ContainerId);

        QueryDefinition queryDefinition = new QueryDefinition(
            "SELECT * FROM c WHERE c.SchemaVersion = null OR NOT IS_DEFINED(c.SchemaVersion)");

        FeedIterator<dynamic> queryResultSetIterator = container.GetItemQueryIterator<dynamic>(queryDefinition);

        while (queryResultSetIterator.HasMoreResults)
        {
            FeedResponse<dynamic> currentResultSet = await queryResultSetIterator.ReadNextAsync();
            foreach (var item in currentResultSet)
            {
                string id = item.id;
                string partitionKey = item.State; // Replace with your partition key property

                await container.DeleteItemAsync<dynamic>(id, new PartitionKey(partitionKey));
                Console.WriteLine($"Deleted item with id: {id}");
            }
        }
    }
}
