using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Project.Domain;
using Project.Repository;
using Project.WebAPI.Dtos;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project.WebAPI.Controllers
{
    [Route("RestAPIFurb/comandas")]
    [ApiController]
    [AllowAnonymous]
    public class ComandasController : ControllerBase
    {
        private readonly IMapper _mapper;
        public readonly IProjectDAO _repo;

        public ComandasController(IProjectDAO repo,
                              IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetComandas()
        {
            try
            {
                var comandas = await _repo.GetComandasAsync();
                var results = _mapper.Map<ComandasDto[]>(comandas);

                return Ok(results);
            }
            catch (System.Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Banco Dados Falhou {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetComandasId(int id)
        {
            try
            {
                var comanda = await _repo.GetComandasAsyncById(id);
                var results = _mapper.Map<ComandaDto>(comanda);

                return Ok(results);
            }
            catch (System.Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Banco Dados Falhou {ex.Message}");
            }
        }

        // POST api/values
        [HttpPost]
        public async Task<IActionResult> Post(ComandaDto model)
        {
            try
            {
                var comanda = _mapper.Map<Comandas>(model);

                _repo.Add(comanda);

                if (await _repo.SaveChangesAsync())
                {
                    return Created($"/api/comanda/{model.idUsuario}", _mapper.Map<ComandaDto>(comanda));
                }
            }
            catch (System.Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                    $"Banco Dados Falhou {ex.Message}");
            }

            return BadRequest();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, ProdutoDto model)
        {
            try
            {
                var produto = await _repo.GetProdutoAsyncById(id);
                if (produto == null) return NotFound();

                _mapper.Map(model, produto);

                _repo.Update(produto);

                if (await _repo.SaveChangesAsync())
                {
                    return Created($"/api/comanda/{model.id}", _mapper.Map<ProdutoDto>(produto));
                }
            }
            catch (System.Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Banco Dados Falhou " + ex.Message);
            }

            return BadRequest();
        }

        [HttpPut("Alterar-Comanda/{id}")]
        public async Task<IActionResult> AlterarComanda(int id, ComandaDto model)
        {
            try
            {
                var comanda = await _repo.GetComandasAsyncById(id);
                if (comanda == null) return NotFound();

                if (model.Produtos != null)
                {
                    var idProduto = new List<int>();

                    model.Produtos.ForEach(item => idProduto.Add(item.id));

                    var comandas = comanda.Produtos.Where(
                         endereco => !idProduto.Contains(endereco.id)
                     ).ToArray();

                    if (comandas.Length > 0) _repo.DeleteRange(comandas);

                    _mapper.Map(model, comanda);

                    _repo.Update(comanda);

                    if (await _repo.SaveChangesAsync())
                    {
                        return Created($"/api/comanda/{model.idUsuario}", _mapper.Map<ComandaDto>(comanda));
                    }
                }
            }
            catch (System.Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Banco Dados Falhou " + ex.Message);
            }

            return BadRequest();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var comanda = await _repo.GetComandasAsyncById(id);
                if (comanda == null) return NotFound();

                _repo.Delete(comanda);

                if (comanda.Produtos != null)
                {
                    foreach (var item in comanda.Produtos)
                    {
                        _repo.Delete(item);
                    }
                }

                if (await _repo.SaveChangesAsync())
                {
                    return Ok(this.StatusCode(StatusCodes.Status200OK, "Comanda Removida Com Sucesso"));
                }
            }
            catch (System.Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Banco Dados Falhou");
            }

            return BadRequest();
        }
    }
}