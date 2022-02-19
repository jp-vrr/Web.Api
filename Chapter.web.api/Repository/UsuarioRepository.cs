using Chapter.web.api.models;
using charpter.Contexts;
using System.Collections.Generic;
using System.Linq;

namespace Chapter.web.api.Repository
{
    public class UsuarioRepository
    {
        private readonly ChapterContext _context;

        public UsuarioRepository(ChapterContext context)
        {
            _context = context;
        }

        public List<Usuario> Listar()
        {
            return _context.Usuarios.ToList();
        }

        public void Cadastrar(Usuario u)
        {
            _context.Usuarios.Add(u);
            _context.SaveChanges();
        }

        public Usuario BuscarPorID(int id)
        {
          return _context.Usuarios.Find(id);
        }

        public void Atualizar(int id, Usuario u)
        {
            Usuario UsuarioEncontrado = _context.Usuarios.Find(id);
            if(UsuarioEncontrado != null)
            {
                UsuarioEncontrado.Email = u.Email;
                UsuarioEncontrado.Senha = u.Senha;
            }

            _context.Usuarios.Update(UsuarioEncontrado);
            _context.SaveChanges();
        }

        public void Deletar(int id)
        {
            Usuario UsuarioEncontrado = _context.Usuarios.Find(id);
            if(UsuarioEncontrado != null)
            {
                _context.Usuarios.Remove(UsuarioEncontrado);
                _context.SaveChanges();
            }
        }

        public Usuario Login(string email, string senha)
        {
            return _context.Usuarios.FirstOrDefault(u => u.Email == email && u.Senha == senha);
        }
    }
}
