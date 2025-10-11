
using Hotel.ATR.WebApi.Interfaces;
using Hotel.ATR.WebApi.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System.Net;


namespace Hotel.ATR.WebApi.Controllers
{
    //[Route("api/[controller]")]
    [Route("api/team")]
    [ApiController]
   
    public class TeamController : ControllerBase
    {
        private readonly ILogger<TeamController> _logger;
        private readonly IRepository _db;
        private ReturnResult result;

        public static List<Team> teams = new List<Team>();
        public TeamController(ILogger<TeamController> logger, IRepository db)
        {
            _logger = logger;
            _db = db;
            result = new ReturnResult();
        }

        [HttpGet]
        [Route("[action]")]
        [Route("/get-all-teams")]
        [Authorize]
        public async Task<ReturnResult> GetTeams()
        {
            try
            {
                result.IsSuccess = true;
                result.StatusCode = HttpStatusCode.OK;
                result.Result = await _db.GetAllAsync<Team>();
            }
            catch (Exception ex)
            {
                result.IsSuccess = false;
                result.StatusCode = HttpStatusCode.NotFound;

                result.ErrorMessag = new List<string>(){ ex.Message };
                if(ex.InnerException!=null)
                    result.ErrorMessag.Add(ex.Message);
            }
           
            return result;
        }

        [HttpGet(Name = "GetTeam")]
        public async Task<ReturnResult> GetTeam(int id)
        {
            try
            {
                result.IsSuccess = true;
                result.StatusCode = HttpStatusCode.OK;
                result.Result = await _db.GetAsync<Team>(t=>t.Id == id);
            }
            catch (Exception ex)
            {
                result.IsSuccess = false;
                result.StatusCode = HttpStatusCode.NotFound;

                result.ErrorMessag = new List<string>() { ex.Message };
                if (ex.InnerException != null)
                    result.ErrorMessag.Add(ex.Message);
            }

            return result;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ReturnResult Post([FromForm] Team team)
        {

            return null;
            //if (team == null)
            //    //return BadRequest();
            //    //return BadRequest("Отсутствуют данные!");
            //    return BadRequest(new { ErrorMessage = "Отсутствуют данные!", IsError = true });

            //try
            //{
            //    teams.Add(team);
            //    _returnResult.StatusCode = HttpStatusCode.Created;

            //    return CreatedAtRoute("GetTeam", new ) Ok(new { ErrorMessage = "Данные успешно добавлены!", IsError = false });
            //}
            //catch (Exception ex)
            //{
            //    _returnResult.StatusCode = HttpStatusCode.BadRequest;
            //    return BadRequest(new { ErrorMessage = ex.Message, IsError = true });
            //}
        }


        [HttpDelete()]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ReturnResult Delete(int id)
        {
            if (id <= 0)
            {
                result.IsSuccess = false;
                result.StatusCode = HttpStatusCode.BadRequest;
                result.ErrorMessag = new List<string>() { "Id указан не корректно" };
                return result;
            }

            var data = teams.FirstOrDefault(f => f.Id== id);

            if (data != null)
            {
                teams.Remove(data);
                result.IsSuccess = true;
                result.StatusCode = HttpStatusCode.OK;               
            }
            else
            {
                result.IsSuccess = false;
                result.StatusCode = HttpStatusCode.NotFound;
                result.ErrorMessag = new List<string>() { "Данные не найдены" };
            }
            return result;
        }

        [HttpPut]
        public ReturnResult Put([FromBody] Team team, int Id)
        {
            var data = teams.FirstOrDefault(f => f.Id == Id);

            if (data != null)
            {
                data.PositionName = team.PositionName;
                data.PathImage = team.PathImage;
                data.Description = team.Description;

                result.IsSuccess = true;
                result.StatusCode = HttpStatusCode.OK;
            }
            else
            {
                result.IsSuccess = false;
                result.StatusCode = HttpStatusCode.NotFound;
            }
            return result;
        }

        [HttpPatch]
        public ReturnResult Patch(int Id, [FromBody] JsonPatchDocument<Team> jsonPatch)
        {
            var res = teams.FirstOrDefault(f => f.Id == Id);
            if (res != null)
            {
                jsonPatch.ApplyTo(res);

                result.IsSuccess = true;
                result.StatusCode = HttpStatusCode.OK;
            }
            else
            {
                result.IsSuccess = false;
                result.StatusCode = HttpStatusCode.NotFound;
            }
            return result;
        }
    }
}