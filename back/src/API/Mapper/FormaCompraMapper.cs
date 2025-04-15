using API.Dto;
using API.Models;

namespace API.Mapper
{
    public class FormaCompraMapper
    {
        public static FormaCompraDto ToDTO(FormaCompra formaCompra)
        {
            return new FormaCompraDto
            {
                CodForComp = formaCompra.CodForComp,
                Descricao = formaCompra.Descricao
            };
        }
    }
}
