using DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PTWee_SinKdnaConX.Models;
using PTWee_SinKdnaConX.Services;

namespace PTWee_SinKdnaConX.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegisterController : ControllerBase
    {
        private readonly IRegister _service;
        private readonly WeeCompanyContext _context;

        public RegisterController(IRegister service, WeeCompanyContext context)
        {
            this._service = service;
            this._context = context;
        }

        [HttpGet]
        [Route("getRegister/{id}")]

        public Register_DTO GetRegister(int id)
        {
            Register_DTO register = new Register_DTO();
            register = _service.GetRegisterbyID(id);
            return register;
        }

        [HttpGet]
        [Route("getRegisters")]

        public List<WCompanyRegister> GetRegisters()
        {
            List<WCompanyRegister> lista_registros = new List<WCompanyRegister>();

            lista_registros = _service.GetRegisters();
            return lista_registros;
        }

        [HttpPost]
        [Route("insertarRegister")]

        public IActionResult InsertarRegistro([FromBody] Register_DTO register)
        {
            string respuesta = _service.InsertRegister(register);
            return Ok(new { respuesta });
        }

        [HttpPut]
        [Route("updateRegister")]
        public IActionResult updateRegister([FromBody]
        Register_DTO register)
        { 
        string respuesta = _service.UpdateRegister(register);
            return Ok(new { respuesta });
        }

        [HttpDelete]
        [Route("delete/{id}")]
        public IActionResult deleteRegister(int id)
        {
            string respuesta = _service.DeleteRegister(id);
            return Ok (new { respuesta });  
        }
        
        
        
    }
}
