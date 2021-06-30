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
/// Unit tests help to ensure functionality, and provide a means of verification for refactoring efforts.                                        
/// You should make sure that this API is tested using TDD (Test-driven development) 
/// as a developmental approach in which TFD (Test-First Development) is used through out
/// and where you should write a test before writing a code for the production. 
/// Look at the below articles on how to write unit tests for the Web API controller.
/// https://www.c-sharpcorner.com/article/unit-testing-controllers-in-web-api/
/// https://docs.microsoft.com/en-us/aspnet/web-api/overview/testing-and-debugging/unit-testing-with-aspnet-web-api
/// 
/// Also think of Code coverage as is a measure of the amount of code that is run by unit tests
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
/// 
/// 
/// 
/// 
/// The [ApiController] attribute enables a few features including attribute 
/// routing requirement, automatic model validation and binding source parameter 
/// inference.
/// https://stackoverflow.com/questions/66545845/what-does-the-apicontroller-attribute-do
/// https://docs.microsoft.com/en-us/aspnet/core/tutorials/first-web-api?view=aspnetcore-5.0&tabs=visual-studio
/// https://entityframework.net/linq-queries
/// https://docs.microsoft.com/en-us/aspnet/web-api/overview/security/authentication-and-authorization-in-aspnet-web-api
/// </summary>


namespace BapApi.Controllers 
{
    [Authorize(Roles = "admin")]
    [ApiController]
    [ApiConventionType(typeof(DefaultApiConventions))]
    [Route("api/[controller]")] 
    public class StoreAppsController : ControllerBase
    {
        private readonly StoreAppsContext _context;

        public StoreAppsController(StoreAppsContext context)
        {
            _context = context;
        }

        /// <summary>
        /// https://docs.microsoft.com/en-us/aspnet/core/web-api/advanced/conventions?view=aspnetcore-5.0
        /// The below HttpGet() gets all the data from the database using 
        /// the StoreAppsController above and since the whole controller is 
        /// restricted to eb accessed only by people with admin prrivilages 
        /// the AllowAnonymous authorrization only allows a non admin user 
        /// to get the data, but for everything else they need admin privilages
        /// </summary>
        /// <returns></returns>

        [AllowAnonymous]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpGet()]
        public async Task<ActionResult<IEnumerable<StoreAppDTO>>> GetStoreApps()
        {
            return await _context.StoreApps.Select(x => StoreAppToDTO(x)).ToListAsync();
        }


        /// <summary>
        /// The below HttpGet("{id}") only allows to Get a single row from the database by Id
        /// The [ProducesDefaultResponseType] comes in handy for non-success (200) return codes.
        /// for example if a failure status code returns a model that describes the problem,
        /// you can specify that the status code in that case produces something different than the success case. 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>

        [AllowAnonymous]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpGet("{id}")]
        public async Task<ActionResult<StoreAppDTO>> GetStoreApp(int id)
        {
            var storeApp = await _context.StoreApps.FindAsync(id);

            if (storeApp == null)
            {
                return NotFound();
            }
            return StoreAppToDTO(storeApp);
        }

        // GET: api/StoreApps/FirstTen
        // Get the first ten results from the database aftering ordering the data by Id
        [HttpGet("FirstTen")]
        public async Task<ActionResult<IEnumerable<StoreAppDTO>>> GetStoreTopTen()
        {

            var storeTopTen = await _context.StoreApps.Select(x => StoreAppToDTO(x)).Take(10).ToListAsync();

            if (storeTopTen == null)
            {
                return NotFound();
            }
            
            return storeTopTen; 
        }

        // POST: api/StoreApps
        // Add a new record to the database

        // Delete: api/StoreApps/1
        // Delete a single row from the database by Id

        // DTO helper method. "Production apps typically limit the data that's input and returned using a subset of the model"

        /// <summary>
        /// Right now our web API exposes the database entities to the client. 
        /// And the client receives data that maps directly to our database tables. 
        /// However, that's not always a good idea thus the storeApp will defines how the 
        /// data will be sent over the network.
        /// https://docs.microsoft.com/en-us/aspnet/web-api/overview/data/using-web-api-with-entity-framework/part-5
        /// </summary>
        /// <param name="storeApp"></param>
        /// <returns></returns>
        private static StoreAppDTO StoreAppToDTO(StoreApp storeApp) =>
            new StoreAppDTO
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
