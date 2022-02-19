using charpter.Contexts;
using charpter.models;
using System.Collections.Generic;
using System.Linq;

namespace charpter.Repositors
{
    public class LivroRepository
    {
        private readonly ChapterContext _context;
        // Variavel para armazenar valor.

        public LivroRepository(ChapterContext context)
        {
            _context = context;
            // Sempre que chamarem a classe LivroRepository será executada a "ChapterContext".
        }

        public List<Livro> Listar()
        {
            return _context.Livros.ToList();
            // O "_" Chama a conexão para Livros(Banco de dados) e traz a irfomação em forma de lista (ToList).
        }

        public Livro BuscarPorID(int id)
        {
            return _context.Livros.Find(id);
        }

        public void Cadastrar(Livro livro)
        {
            _context.Livros.Add(livro);

            _context.SaveChanges();
        }

        public void atualizar(int id, Livro livro)
        {
            Livro livroBuscar = _context.Livros.Find(id);
            if (livroBuscar != null)
            {
                livroBuscar.Titulo = livro.Titulo;
                livroBuscar.QuantidadePaginas = livro.QuantidadePaginas;
                livroBuscar.Disponivel = livro.Disponivel;
            }

            _context.Livros.Update(livroBuscar);

            _context.SaveChanges();
        }

        public void Deletar(int id)
        {
            Livro livroBuscar = _context.Livros.Find(id);

            _context.Livros.Remove(livroBuscar);

            _context.SaveChanges();
        }
    }
}
