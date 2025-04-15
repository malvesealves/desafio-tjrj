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
                CodAs = assunto.CodAs,
                Descricao = assunto.Descricao
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
