using m01_labMedicine.Model;
using Microsoft.AspNetCore.Mvc;

namespace m01_labMedicine.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MedicoController : ControllerBase
    {
        private readonly AtendimentoMedicoContext locacaoContext;

        //Construtor com parametro (Injetado)   
        public MedicoController(AtendimentoMedicoContext locacaoContext)
        {
            this.locacaoContext = locacaoContext;
        }                
    }
}