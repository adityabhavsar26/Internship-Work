using Microsoft.AspNetCore.Mvc;
using BookStore.Repository;
using BookStore.Models.Models;
using BookStore.Models.ViewModels;
using System;
using System.Net;

namespace BookStore.Api.Controllers
{
    [Route("api/role")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        RoleRepository _repository = new RoleRepository();

        [HttpGet]
        [Route("list")]
        public IActionResult GetRoles(int pageIndex = 1, int pageSize = 10, string keyword = "")
        {
            try
            {
                var roles = _repository.GetRoles(pageIndex, pageSize, keyword);
                if (roles == null)
                    return StatusCode(HttpStatusCode.NotFound.GetHashCode(), "Please provide correct information");

                return StatusCode(HttpStatusCode.OK.GetHashCode(), roles);
            }
            catch (Exception ex)
            {
                return StatusCode(HttpStatusCode.InternalServerError.GetHashCode(), ex.Message);
            }
        }
        [HttpGet]
        [Route("id")]
        public IActionResult GetRoles(int id)
        {
            try
            {
                var role = _repository.GetRole(id);
                if (role == null)
                    return StatusCode(HttpStatusCode.NotFound.GetHashCode(), "Please provide correct information");

                return StatusCode(HttpStatusCode.OK.GetHashCode(), role);

            }
            catch (Exception ex)
            {
                return StatusCode(HttpStatusCode.InternalServerError.GetHashCode(), ex.Message);
            }
        }
    }
}
