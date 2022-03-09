using Microsoft.AspNetCore.Mvc;
using Api.Cliente.Models;

namespace Api.Cliente.Controllers
{
    [ApiController]
    [Route("")]
    public class ClienteController : ControllerBase
    {
        private static List<ClienteModel> _lista = new List<ClienteModel>()
        {
            new ClienteModel()
            {
                Cpf = "1234213",
                Nome = "Iago",
                Sobrenome ="Alves",
                DataDeNascimento = new DateTime(28052000)
            },
            new ClienteModel()
            {
                Cpf = "85224",
                Nome = "Iago",
                Sobrenome ="Alves",
                DataDeNascimento = new DateTime(28052000)
            }
        };
        public ClienteController()
        {
        }

        [HttpGet]
        [Route("/api/v1/clientes/{cpf}")]

        public ActionResult<ClienteModel> BuscarCliente(string cpf)
        {
            var cliente = _lista.FirstOrDefault(c => c.Cpf == cpf);
            if (cliente == null)
            {
                return NotFound("Cliente não encontrado");
            }
            return Ok(cliente);
        }

        [HttpPost]
        [Route("/api/v1/clientes")]
        public ActionResult<ClienteModel> AdicionarCliente([FromBody] ClienteModel cliente)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest("Erro ao cadastrar cliente ");
                }
                _lista.Add(cliente);

                return Ok("Cliente cadastrado com sucesso!");
            }
            catch
            {
                return NotFound("Cliente não encontrado");
            }
        }

        [HttpGet]
        [Route("/api/v1/clientes")]
        public ActionResult<List<ClienteModel>> BuscarClientePorCpf()
        {
            if (_lista == null)
            {
                return NotFound("Nenhum cliente encontrado");
            }
            return Ok(_lista);
        }

        [HttpGet]
        [Route("/api/v1/clientes/{cpf}")]
        public ActionResult<List<ClienteModel>> BuscarClientePorCpf(string cpf)
        {
            ClienteModel? cliente = _lista.FirstOrDefault(c => c.Cpf == cpf);

            if (_lista.Remove(cliente!) == false)
            {
                return NotFound("Cliente não encontrado");
            }

            return Ok(cliente);
        }

        [HttpPut]
        [Route("/api/v1/clientes/{cpf}")]
        public ActionResult<List<ClienteModel>> AlterarClientePorCpf(string cpf, [FromBody] ClienteModel cliente)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest("Cliente não alterado");
                }
                var NovoCliente = _lista.FirstOrDefault(c => c.Cpf == cpf);
                if (NovoCliente != null)
                {
                    NovoCliente.Sobrenome = cliente.Sobrenome;
                    NovoCliente.Nome = cliente.Nome;
                }

                return Ok("Cliente atualizado");
            }
            catch
            {
                return BadRequest("Não atualizado");
            }
        }

    }
}


