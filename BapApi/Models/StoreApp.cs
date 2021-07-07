using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


/// <summary>
/// [1] We will validate our API model for security reasons because insertion data from the frontend will be saved in our database.
/// And to avoid causing a mess in our database we will validate the model object in our persistent storage. 
/// For example, if there is a form in the frrontend application and the frrontend developer forgets to validate input fields,
/// the result can affect the model validation stage. You may even implement input validation at the DB level too.
/// https://www.c-sharpcorner.com/UploadFile/dacca2/model-validation-in-web-api/
/// 
/// [2] The DatabaseGenerated attribute specifies how values are generated for a property by the database. The attribute 
/// takes a DatabaseGeneratedOption enumeration value, which can be one of three values: 
/// Computed
/// Identity
/// None
/// https://www.learnentityframeworkcore.com/configuration/data-annotation-attributes/databasegenerated-attribute
/// 
/// [3] A cache is a hardware or software component that stores data so that future requests for that data can be served faster; 
/// the data stored in a cache might be the result of an earlier computation or a copy of data stored elsewhere. A cache hit 
/// occurs when the requested data can be found in a cache, while a cache miss occurs when it cannot. Cache hits are served 
/// by reading data from the cache, which is faster than recomputing a result or reading from a slower data store; thus,
/// the more requests that can be served from the cache, the faster the system performs. to do cacheing please visit the links below
/// https://en.wikipedia.org/wiki/Cache_(computing)
/// https://medium.com/net-core/in-memory-distributed-redis-caching-in-asp-net-core-62fb33925818
/// https://dottutorials.net/caching-asp-net-core-app-api-performance-boost/
/// </summary>
namespace BapApi.Models
{
    //public class StoreApp
    //{
    //    [Column("id")]
    //    [Display(Name = "App ID")]
    //    [Key]
    //    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    //    [Required(ErrorMessage = "ID is required")]
    //    public int Id { get; set; }

    //    [Column("name")]
    //    [Display(Name = "App Name")]
    //    [StringLength(100, MinimumLength = 3)]
    //    [Required(ErrorMessage = "Name must be between 3 and 100 characters")]
    //    public string Name { get; set; }

    //    [Column("rating")]
    //    [Display(Name = "Rating")]
    //    [StringLength(5)]
    //    [RegularExpression("^[0-9]*$")]
    //    public double Rating { get; set; }

    //    [Column("people")]
    //    [Display(Name = "Number of People Downloaded")]
    //    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    //    public int People { get; set; }

    //    [Column("category")]
    //    [Display(Name = "Category")]
    //    [Required(ErrorMessage = "Category is required")]
    //    public string Category { get; set; }

    //    [Column("date")]
    //    [Display(Name = "Date")]
    //    [Required(ErrorMessage = "Date is required")]
    //    public string Date { get; set; }

    //    [Column("price")]
    //    [Display(Name = "Price")]
    //    [RegularExpression("^[0-9]*$")]
    //    [Required(ErrorMessage = "Price should be a number")]
    //    public string Price { get; set; }
    //}

    public class StoreApp
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Rating { get; set; }
        public int People { get; set; }
        public string Category { get; set; }
        public string Date { get; set; }
        public string Price { get; set; }
    }

    /// <summary>
    /// To prevent our web API exposing the database entities to the client.
    /// Such as the client receiving data that is mapping directly to our
    /// database tables. Which is not a good idea thus we will create the StoreAppDTO
    /// Data Transfer Object (DTO) to send secured data over the network.
    /// https://docs.microsoft.com/en-us/aspnet/web-api/overview/data/using-web-api-with-entity-framework/part-5
    /// </summary>
    //public class StoreAppDTO
    //{
    //   public int    Id       { get; set; }
    //   public string Name     { get; set; }
    //   public double Rating   { get; set; }
    //   public int    People   { get; set; }
    //   public string Category { get; set; }
    //   public string Date     { get; set; }
    //   public string Price    { get; set; }
    //}
}
