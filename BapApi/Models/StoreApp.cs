using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


/// <summary>
/// [1] We will validate our API model for security reasons because insertion
/// data from the frontend will be saved in our database. And to avoid causing 
/// a mess in our database we will validate the model object in our persistent 
/// storage. For example, if there is a form in the frrontend application and 
/// the frontend developer forgets to validate input fields, the result can 
/// affect the model validation stage. 
/// https://www.c-sharpcorner.com/UploadFile/dacca2/model-validation-in-web-api/
/// https://learn-the-web.algonquindesign.ca/courses/web-dev-4/designing-form-errors/all-errors.png
/// 
/// [2] The DatabaseGenerated attribute specifies how values are generated 
/// for a property by the database. The attribute takes a DatabaseGeneratedOption 
/// enumeration value, which can be one of three values (a) Computed (b) Identity (c) None
/// https://www.learnentityframeworkcore.com/configuration/data-annotation-attributes/databasegenerated-attribute
/// 
/// [3] We also planned to implement a caheing mechanis for thei API. A cache is
/// a hardware or software component that stores data so that future requests for 
/// that data can be served faster. the data stored in a cache might be the result 
/// of an earlier computation or a copy of data stored elsewhere. A cache hit 
/// occurs when the requested data can be found in a cache, while a cache miss occurs
/// when it cannot. Cache hits are served by reading data from the cache, which is
/// faster than recomputing a result or reading from a slower data store.
/// 
/// https://azure.microsoft.com/en-us/services/cache/
/// https://medium.com/net-core/in-memory-distributed-redis-caching-in-asp-net-core-62fb33925818
/// 
/// </summary>
namespace BapApi.Models
{
    public class StoreApp
    {
        [Column("id")]
        [Display(Name = "App ID")]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required(ErrorMessage = "ID is required")]
        public int Id { get; set; }

        [Column("name")]
        [Display(Name = "App Name")]
        [StringLength(100, MinimumLength = 3)]
        [Required(ErrorMessage = "Name must be between 3 and 100 characters")]
        public string Name { get; set; }


        [Column("rating")]
        [Display(Name = "Rating")]
        [StringLength(5)]
        [RegularExpression("^[0-9]*$")]
        public double   Rating  { get; set; }

        [Column("people")]
        [Display(Name = "Number of People Downloaded")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int      People  { get; set; }

        [Column("category")]
        [Display(Name = "Category")]
        [Required(ErrorMessage = "Category is required")]
        public string   Category{ get; set; }

        [Column("date")]
        [Display(Name = "Date")]
        [Required(ErrorMessage = "Date is required")]
        public string   Date    { get; set; }
        public string   Price   { get; set; }
    }
}
