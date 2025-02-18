﻿using ApiCrudMongo.Data.Repositories;
using ApiCrudMongo.Models;
using ApiCrudMongo.Models.InputModels;
using Microsoft.AspNetCore.Mvc;



namespace ApiCrudMongo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TarefasController : ControllerBase
    {
        private ITarefasRepository _tarefasRepository;

        public TarefasController(ITarefasRepository tarefasRepository)
        {
            _tarefasRepository = tarefasRepository;
        }


        // GET: api/tarefas
        [HttpGet]
        public IActionResult Get()
        {
            var tarefas = _tarefasRepository.Buscar();
            return Ok(tarefas);
        }

        // GET api/tarefas/{id}
        [HttpGet("{id}")]
        public IActionResult Get(string id)
        {

            var tarefa = _tarefasRepository.Buscar(id);

            if (tarefa == null)
                return NotFound();

            return Ok(tarefa);
        }

        // POST api/tarefas
        [HttpPost]
        public IActionResult Post([FromBody] TarefaInputModels novaTarefa)
        {
            var tarefa = new Tarefa(novaTarefa.Nome, novaTarefa.Descricao);

            _tarefasRepository.Adicionar(tarefa);
            return Created("", tarefa);
        }

        // PUT api/tarefas/{id}
        [HttpPut("{id}")]
        public IActionResult Put(string id, [FromBody] TarefaInputModels tarefaAtualizada)
        {
            var tarefa = _tarefasRepository.Buscar(id);

            if (tarefa == null)
                return NotFound();

            tarefa.AtualizarTarefa(tarefaAtualizada.Nome, tarefaAtualizada.Descricao, tarefaAtualizada.Concluido);

            _tarefasRepository.Atualizar(id, tarefa);

            return Ok(tarefa);
        }

        // DELETE api/tarefas/{id}
        [HttpDelete("{id}")]
        public IActionResult Delete(string id)
        {
            var tarefa = _tarefasRepository.Buscar(id);

            if (tarefa == null)
                return NotFound();

            _tarefasRepository.Remover(id);

            return NoContent();
        }
    }
}
