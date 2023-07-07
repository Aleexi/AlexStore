using API.Errors;
using Infrastructure.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class TestController : SuperController
    {
        private readonly StoreContext context;
        public TestController(StoreContext context)
        {
            this.context = context;
        }

        [HttpGet("notfound")]
        public ActionResult GetNotFoundRequest()
        {
            var pokemon = this.context.Pokemons.Find(10);

            if (pokemon == null) return NotFound(new ApiResponse(404));

            return Ok(pokemon);
        }

        [HttpGet("servererror")]
        public ActionResult GetServerError()
        {
            var pokemon = this.context.Pokemons.Find(104);

            pokemon.ToString();

            return Ok();
        }

        [HttpGet("badrequest")]
        public ActionResult GetBadRequest()
        {
            return BadRequest(new ApiResponse(400));
        }

        [HttpGet("badrequest/{id}")]
        public ActionResult GetNotFoundRequest(int id)
        {
            return Ok();
        }

        [HttpGet("authorizedonly")]
        [Authorize]
        
        public ActionResult<string> AuthorizedOnly() {
            return "If you have gotten this message back, you are authorized to receive this :)";
        }
    }
}