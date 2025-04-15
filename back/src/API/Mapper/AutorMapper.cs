using API.Dto;
using API.Models;

namespace API.Mapper
{
    public static class AutorMapper
    {
        public static AutorDto ToDTO(Autor autor)
        {
            return new AutorDto
            {
                CodAu = autor.CodAu,
                Nome = autor.Nome
            };
        }
    }
}
