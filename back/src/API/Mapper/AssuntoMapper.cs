using API.Dto;
using API.Models;

namespace API.Mapper
{
    public static class AssuntoMapper
    {
        public static AssuntoDto ToDTO(Assunto assunto)
        {
            return new AssuntoDto
            {
                Código = assunto.CodAs,
                Descrição = assunto.Descricao
            };
        }

        //public static Assunto ToEntity(AssuntoDto dto)
        //{
        //    return new Assunto
        //    {
        //        Descricao = dto.descricao        
        //    };
        //}
    }
}
