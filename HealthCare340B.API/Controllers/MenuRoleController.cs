﻿using HealthCare340B.DataAccess;
using HealthCare340B.DataModel;
using HealthCare340B.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace HealthCare340B.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MenuRoleController : ControllerBase
    {
        private DAMenuRole menuRole;
        private object db;

        public MenuRoleController(HealthCare340BContext _db)
        {

            menuRole = new DAMenuRole(_db);
            db = _db;
        }
        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            try
            {
                VMResponse<List<VMMMenuRole?>> response = await Task.Run(() => menuRole.GetByFilter(""));
                if (response.Data!.Count > 0 && response.Data != null)
                {
                    return Ok(response);
                }
                else
                {
                    throw new Exception(response.Message);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("action/{filter?}")]
        public async Task<ActionResult> GetByFilter(string filter)
        {
            try
            {
                VMResponse<List<VMMMenuRole?>> response = await Task.Run(() => menuRole.GetByFilter(filter));
                if (string.IsNullOrEmpty(filter)) throw new ArgumentNullException("No filter provived");

                return Ok(response);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"MenuController.GetByFilter: {ex.Message}");
                return BadRequest(ex.Message);
            }
        }
         
    }

}