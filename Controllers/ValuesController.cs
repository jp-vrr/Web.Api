using charpter.models;
using charpter.Repositors;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;

namespace charpter.Controllers
{
    [Produces("application/json")]

    [Route("api/[controller]")]
    [ApiController]

    [Authorize]
    public class LivrosController : ControllerBase
    {
        private readonly LivroRepository _livroRepository;

        public LivrosController(LivroRepository livroRepository)
        {
            _livroRepository = livroRepository;
        }

        [HttpGet]
        public IActionResult Listar()
        {
            try
            {
                return Ok(_livroRepository.Listar());
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
                Livro livroProcurado = _livroRepository.BuscarPorID(id);

                if (livroProcurado == null)
                {
                    return NotFound();
                }
                return Ok(livroProcurado);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        [HttpPost]
        public IActionResult Cadastrar(Livro livro)
        {
            try
            {
                _livroRepository.Cadastrar(livro);
                return StatusCode(201);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);

            }
        }

        [HttpPut("{id}")]
        public IActionResult atualizar(int id, Livro livros)
        {
            try
            {
                _livroRepository.atualizar(id, livros);
                return StatusCode(204);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);

            }

        }

        [HttpDelete("{id}")]
        public IActionResult Deletar(int id)
        {
            try
            {
                _livroRepository.Deletar(id);
                return StatusCode(204);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);

            }
        }

    }

}
