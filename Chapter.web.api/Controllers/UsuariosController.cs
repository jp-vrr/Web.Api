using Chapter.web.api.models;
using Chapter.web.api.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;

namespace Chapter.web.api.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {
        private readonly UsuarioRepository _usuarioRepository;

        public UsuariosController(UsuarioRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }

        [HttpGet]
        public IActionResult Listar()
        {
            try
            {
                return Ok(_usuarioRepository.Listar());
            }
            catch (System.Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        [HttpGet("{id}")]
        public IActionResult BuscarPorID(int id)
        {
            try
            {
                Usuario UsuarioProcurado = _usuarioRepository.BuscarPorID(id);
                if(_usuarioRepository == null)
                {
                    return NotFound();
                }
                return Ok(UsuarioProcurado);
            }
            catch (System.Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        [HttpPost]
        public IActionResult Cadastrar(Usuario u)
        {
            try
            {
                _usuarioRepository.Cadastrar(u);

                return StatusCode(201);
            }
            catch (System.Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        [HttpPut("{id}")]
        public IActionResult Atualizar(int id, Usuario u)
        {
            try
            {
                _usuarioRepository.Atualizar(id, u);
                return StatusCode(204);
            }
            catch (System.Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        [HttpDelete("{Id}")]
        public IActionResult Deletar(int id)
        {
            try
            {
                _usuarioRepository.Deletar(id);
                return StatusCode(204);
            }
            catch (System.Exception e)
            {
                throw new Exception(e.Message);
            }
        }

    }
}
