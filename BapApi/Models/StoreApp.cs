namespace BapApi.Models
{
    public class StoreApp
    {
       public int    Id       { get; set; }
       public string Name     { get; set; }
       public double Rating   { get; set; }
       public int    People   { get; set; }
       public string Category { get; set; }
       public string Date     { get; set; }
       public string Price    { get; set; }
    }

    /// <summary>
    /// To prevent our web API exposing the database entities to the client.
    /// Such as the client receiving data that is mapping directly to our
    /// database tables. Which is not a good idea thus we will create the StoreAppDTO
    /// Data Transfer Object (DTO) to send secured data over the network.
    /// https://docs.microsoft.com/en-us/aspnet/web-api/overview/data/using-web-api-with-entity-framework/part-5
    /// </summary>
    public class StoreAppDTO
    {
       public int    Id       { get; set; }
       public string Name     { get; set; }
       public double Rating   { get; set; }
       public int    People   { get; set; }
       public string Category { get; set; }
       public string Date     { get; set; }
       public string Price    { get; set; }
    }
}