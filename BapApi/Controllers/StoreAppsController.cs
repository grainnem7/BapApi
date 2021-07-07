using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BapApi.Models;
using Microsoft.AspNetCore.Authorization;


/// <summary>
///                                         README.txt
///                                         Date: 30/06/2021
///                                         
/// [1] Unit tests help to ensure functionality, and provide a means of verification for refactoring efforts.                                        
/// You should make sure that this API is tested using TDD (Test-driven development) 
/// as a developmental approach in which TFD (Test-First Development) is used through out
/// and where you should write a test before writing a code for the production. 
/// Look at the below articles on how to write unit tests for the Web API controller.
/// https://www.c-sharpcorner.com/article/unit-testing-controllers-in-web-api/
/// https://docs.microsoft.com/en-us/aspnet/web-api/overview/testing-and-debugging/unit-testing-with-aspnet-web-api
/// 
/// [2] Also think of Code coverage as is a measure of the amount of code that is run by unit tests
/// either lines, branches, or methods for example, if you have an application with two branches 
/// of code (branch X, and branch Y) a unit test will verify that branch "X" has a code coverage 
/// of 50%. see the following code for more information.
///  https://docs.microsoft.com/en-us/dotnet/core/testing/unit-testing-code-coverage?tabs=windows
/// 
/// Remember that Code coverage is an important measure that quantifies the degree of which your 
/// code has been thoroughly tested. There are plenty of Code Coverage Tools on the market but 
/// below I've listed tools with the most popular features and latest download links, checkout below link
/// https://blog.ndepend.com/guide-code-coverage-tools/
///
/// [3] Automation testing for the API is equally important and should be given the same weight of 
/// importance as the unit tests and Code coverage mentioned above. Because with APIs increasingly 
/// becoming important to developers, so its importantfor developers and programmers to automate their 
/// API tests and know how to do it best. As it determs whether an API that has been developed meets 
/// the anticipated threshold in terms of functionality, performance, reliability, and security. 
/// Please look at the below link for your best automated API testing tools out there.
/// https://rapidapi.com/blog/best-api-testing-tools/
/// 
/// [4] For performance reasons I've included the [ApiController] attribute to the controller class below 
/// to enable the following opinionated, API-specific behaviors such as 
/// Attribute routing requirement
/// Automatic HTTP 400 responses
/// Binding source parameter inference
/// Multipart / form-data request inference
/// These features require a compatibility version of 2.1 or later.
/// https://docs.microsoft.com/en-us/aspnet/core/web-api/?view=aspnetcore-5.0#apicontroller-attribute
///
/// 
/// [5] The Web API Service authentication and authorization is very important as the process should happen 
/// in the host Server and we generally host the Web API Service as IIS. The IIS Server uses the HTTP modules 
/// for checking the authentication of a user. So you can configure your project to use any of the built-in 
/// authentication modules which are available in IIS or ASP.NET, or you can also create your own HTTP module 
/// to perform custom authentication, in this API we used the built in IIS or ASP.NET.
/// https://dotnettutorials.net/lesson/authentication-and-authorization-in-web-api/
/// 
/// [6] In this API try as much as possibel to use LINQ Queries as they are an expression that retrieves data 
/// from a data source. Queries are usually expressed in a specialized query language, such as SQL for relational 
/// databases and XQuery for XML.
/// Language-Integrated Query (LINQ) offers a simpler, consistent model for working with data across various kinds
/// of data sources and formats. And in LINQ query, you always work with programming objects.   
/// https://entityframework.net/linq-queries
/// </summary>


namespace BapApi.Controllers 
{
    //[Authorize(Roles = "admin")]
   // [ApiConventionType(typeof(DefaultApiConventions))]
    [Route("api/[controller]")]
    [ApiController]
    public class StoreAppsController : ControllerBase
    {
        private readonly StoreAppsContext _context;
        public StoreAppsController(StoreAppsContext context)
        {
            _context = context;
        }

        //[AllowAnonymous]
        //[ProducesDefaultResponseType]
        //[ProducesResponseType(StatusCodes.Status200OK)]
        //[ProducesResponseType(StatusCodes.Status204NoContent)]
        //[ProducesResponseType(StatusCodes.Status404NotFound)]
        //[ProducesResponseType(StatusCodes.Status400BadRequest)]

        [HttpGet()]
        public async Task<ActionResult<IEnumerable<StoreApp>>> GetStoreApps()
         {
            return await _context.StoreApps.Select(x => StoreApp(x)).ToListAsync();
         }

        /// <summary>
        /// [1] The below HttpGet("{id}") only allows to Get a single row from the database by Id
        /// The [ProducesDefaultResponseType] comes in handy for non-success (200) return codes.
        /// for example if a failure status code returns a model that describes the problem
        /// you can specify that the status code in that case produces the default response to 
        /// to describe the errors collectively, not individually. �Default� means this response 
        /// is used for all HTTP codes that are not covered individually for the operation. for more
        /// information see the linbk below for more indepth 
        /// https://github.com/dotnet/AspNetCore.Docs/issues/10072
        /// https://docs.microsoft.com/en-us/aspnet/core/web-api/advanced/conventions?view=aspnetcore-5.0
        /// https://docs.microsoft.com/en-us/aspnet/core/web-api/action-return-types?view=aspnetcore-2.2
        /// 
        /// [2]  It takes time for a function to fetch data from an API thus  asynchronous programming 
        /// was devised to accommodate for the lag between when a function is called to when the value 
        /// of that function is returned because without asynchronous programming, apps would delay 
        /// loading on a clients screens in such a way that 
        /// (a) a loading screen might appear: When a user signs in, waiting for all their user data 
        /// to be returned from the database. This is bad User Experience (UX), waiting for the data to 
        /// load at each new screen. 
        /// (b) Thus Asynchronous programming allows a user to go about his business in an application,
        /// while processes run in the background, good UX.
        /// https://www.webtrainingroom.com/aspnetmvc/async-task 
        /// https://stackoverflow.com/questions/25720977/return-list-from-async-await-method
        /// 
        /// [3]  The HttpGet() method gets all the data from the database using the StoreAppsController above,
        /// since the controller is restricted to only be accessed admin privilages the AllowAnonymous 
        /// authorization only allows a non admin user to get the data, but for everything else they need 
        /// admin privilages. 
        /// [3] The API calls are most often called by back-end code and you dont want to simply display the 
        /// response from the API but instead we need to check the status code and parse the response to determine 
        /// if our action was successful, displaying data to the user as necessary. An error page is not helpful
        /// in this  situations as it will just bloats the response with HTML and makes client code difficult 
        /// because the JSON (or XML) is expected, not HTML. 
        /// 
        /// Thus while we want to return information in a different format for Web API actions, the techniques for
        /// handling errors are not so different from MVC. Thus instead of returning a View, we return JSON. 
        /// For an API returning errors as empty response bodies is permissible (forgivable) for many invalid 
        /// request types thus simply returning a 404 status code (with no response body) can provide a  client 
        /// with enough information to fix their code. For more error handling in a web API see the following article
        /// https://www.devtrends.co.uk/blog/handling-errors-in-asp.net-core-web-api
        /// 
        /// [4] Routing is very important to define in an API as its the path taken to reach the destination
        /// Thus routing in a Web API is the mechanism to reach the destination. The destination is the exact 
        /// action method which is to be invoked based on API's Request. For more information on web API routing 
        /// see this link below 
        /// https://dotnettutorials.net/lesson/routing-in-asp-net-core-web-api/
        /// https://dotnettutorials.net/lesson/controller-action-return-types-core-web-api/
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>

        //[AllowAnonymous]
        //[ProducesDefaultResponseType]
        //[ProducesResponseType(StatusCodes.Status200OK)]
        //[ProducesResponseType(StatusCodes.Status204NoContent)]
        //[ProducesResponseType(StatusCodes.Status404NotFound)]
        //[ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpGet("{id}")]
        
        public async Task<ActionResult<StoreApp>> GetStoreApp(int id)
        {
            var storeApp = await _context.StoreApps.FindAsync(id);

            if (storeApp == null)
            {
                return NotFound();
            }

            return StoreApp(storeApp);
        }
       
        [HttpGet("Search")]
        public async Task<ActionResult<StoreApp>> GetSearchApp(string SearchTerm)
        {

            // .Trim to make sure that the search term string can take multiple words
            // .ToLower to make sure that the search term is not case sensitive
            var lowerCaseSearchTerm = SearchTerm.Trim().ToLower();

            // lambda expression will have a specific condition that when results to true will return the specific object (=>)
            // creates temporary var to use as a comparison (a)
            // temporary var will check the data to see whether the name or category contains the search term 'lowercasesearchterm'
            // and if true will store it under search app
            var searchApp = await _context.StoreApps
                .Where(a => a.Name.ToLower()
                .Contains(lowerCaseSearchTerm) || a.Category.ToLower().Contains(lowerCaseSearchTerm)).ToListAsync();


            // if there are no results display not found 
            if (searchApp == null)
            {
                return NotFound();
            }
            // if there are results display them
            return Ok(searchApp);

        }

       // GET: api/StoreApps/FirstTen
        // Get the first ten results from the database aftering ordering the data by Id
        [HttpGet("FirstTen")]
        public async Task<ActionResult<IEnumerable<StoreApp>>> GetStoreTopTen()
        {

            var storeTopTen = await _context.StoreApps.Select(x => StoreApp(x)).Take(10).ToListAsync();

            if (storeTopTen == null)
            {
                return NotFound();
            }
            
            return storeTopTen; 
        }

        /// <summary>
        /// create a post request, allows the user to input a new add that will be saved to the database 
        /// </summary>
        /// <param name="storeApp"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<StoreApp>> CreateApp(StoreApp storeApp)
        {
            var storeapp = new StoreApp
            {
                
                Id = storeApp.Id,
                Name = storeApp.Name,
                Rating = storeApp.Rating,
                People = storeApp.People,
                Category = storeApp.Category,
                Date = storeApp.Date,
                Price = storeApp.Price
            };

            _context.StoreApps.Add(storeapp);
            await _context.SaveChangesAsync();

            return CreatedAtAction(
                nameof(storeApp),
                new { id = storeapp.Id },
                StoreApp(storeapp));
        }


        // POST: api/StoreApps
        // Add a new record to the database

        // Delete: api/StoreApps/1
        // Delete a single row from the database by Id

        /// <summary>
        /// [1] Right now our web API exposes the database entities to the client. 
        /// And the client receives data that maps directly to our database tables. 
        /// However, that's not a good idea that such data is sent over the network like that.
        /// 
        /// Thus in programming a data transfer object DTO is an object that carries data 
        /// between processes. The motivation for its use is that communication between 
        /// processes is usually done resorting to remote interfaces (e.g. web services)
        /// where each call is an expensive operation. 
        /// 
        /// Because the majority of the cost of each call is related to the round-trip time 
        /// between the client and the server, one way of reducing the number of calls is to
        /// use an object (the DTO) that aggregates the data that would have been transferred
        /// by the several calls, but that is served by one call only. The difference between 
        /// data transfer objects and business objects or data access objects is that a DTO 
        /// does not have any behavior except for storage, retrieval, serialization and 
        /// deserialization of its own data(mutators, accessors, parsers and serializers). 
        /// In other words, DTOs are simple objects that should not contain any business logic
        /// but may contain serialization and deserialization mechanisms for transferring data
        /// over the wire. Forr more information look up the following links
        /// https://en.wikipedia.org/wiki/Data_transfer_object
        /// https://docs.microsoft.com/en-us/aspnet/web-api/overview/data/using-web-api-with-entity-framework/part-5
        /// </summary>
        /// <param name="storeApp"></param>
        /// <returns></returns>
        /// 


        private static StoreApp StoreApp(StoreApp storeApp) =>
            new StoreApp
            {
                Id          = storeApp.Id,
                Name        = storeApp.Name,
                Rating      = storeApp.Rating,
                People      = storeApp.People,
                Category    = storeApp.Category,
                Date        = storeApp.Date,
                Price       = storeApp.Price
            };
    }

}
