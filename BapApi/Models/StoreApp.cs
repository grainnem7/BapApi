using System.ComponentModel.DataAnnotations;

/// <summary>
/// We will validate our API model for security reasons because insertion data from the frontend will be saved in our database.
/// And to avoid causing a mess in our database we will validate the model object in our persistent storage. 
/// For example, if there is a form in the frrontend application and the frrontend developer forgets to validate input fields,
/// the result can affect the model validation stage. You may even implement input validation at the DB level too.
/// https://www.c-sharpcorner.com/UploadFile/dacca2/model-validation-in-web-api/
/// </summary>
namespace BapApi.Models
{
    public class StoreApp
    {
        [Required]
        public int    Id       { get; set; }

        [StringLength(100, MinimumLength = 3), Required]
        public string Name     { get; set; }

        [Range(0, 4)]
        public double Rating   { get; set; }

        [RegularExpression("^[0-9]*$")]
        public int    People   { get; set; }

        [Required]
        public string Category { get; set; }

        [Required]
        public string Date     { get; set; }

        [Required]
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