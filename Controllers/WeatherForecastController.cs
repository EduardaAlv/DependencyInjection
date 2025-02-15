using DependencyInjection.Services;
using Microsoft.AspNetCore.Mvc;

namespace DependencyInjection.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;
        private readonly IService _service;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, IService service)
        {
            _logger = logger;
            _service = service;
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public async Task<List<string>> Get()
        {
            //Exemplo de tupla, para retornar mais de 1 valor em uma vari�vel
            Tuple<int, string, bool> minhaTupla = new Tuple<int, string, bool>(1, "Ol�", true);
            var item1 = minhaTupla.Item1; // 1
            var item2 = minhaTupla.Item2; // Ol�
            var item3 = minhaTupla.Item3; // True

            List<string> nomesLista = new List<string>();

            //Separa o seu c�digo em outra Thread e vai executar esse c�digo separado, n�o travando sua outra thread
            //Aqui por exemplo est� adicionando o nome depois da thread dormir por 3000 milisegundos
            await Task.Run(() => {
                Thread.Sleep(3000); // Simula uma opera��o demorada
                nomesLista.Add("Eduarda");
            });
;
            var result = _service.GetNomes(nomesLista);

            //Assincron�smo 
            //Await deve ser utilizado em toda chamada de um m�todo que retorne Task ou Task<T>
            //(Em caso de m�todos sincronos, chame o await e transforme o resultado em Task<T> com o Task.FromResult(result);)
            //Utilize pelo menos 1 await em um m�todo sincrono, o await faz com que o m�todo "pare"
            //sua execu��o temporariamente, at� que a Task termine, mas ao mesmo tempo, n�o bloqueia a thread principal
            // deixando que outras opera��es possam acontecer
            //Utilize o Task.FromResult para transformar um resultado n�o assincrono para assincrono (para o tipo Task)
            return await Task.FromResult(result);
        }

        // Exemplo de WhenAll
        private async Task<string> BuscarDadoAsync(string dado)
        {
            // Simula um atraso de 2 segundos (como se fosse uma requisi��o a uma base de dados ou servi�o externo)
            await Task.Delay(2000);
            return dado; // Retorna o dado que foi passado
        }

        // GET api/strings
        [HttpGet(Name = "GetStrings")]
        public async Task<IActionResult> GetStrings()
        {
            // Definindo as strings que queremos buscar de forma ass�ncrona
            string dado1 = "Ma��";
            string dado2 = "Banana";
            string dado3 = "Laranja";
            string dado4 = "Uva";
            string dado5 = "Morango";

            // Criando tarefas ass�ncronas para buscar cada dado de forma independente
            Task<string> tarefa1 = BuscarDadoAsync(dado1);
            Task<string> tarefa2 = BuscarDadoAsync(dado2);
            Task<string> tarefa3 = BuscarDadoAsync(dado3);
            Task<string> tarefa4 = BuscarDadoAsync(dado4);
            Task<string> tarefa5 = BuscarDadoAsync(dado5);

            // Usando Task.WhenAll para aguardar (sem travar a thread princial(await)) todas as tarefas simultaneamente
            // Quando todas as tarefas terminarem, retornamos os resultados
            string[] resultados = await Task.WhenAll(tarefa1, tarefa2, tarefa3, tarefa4, tarefa5);

            // Retorna a lista de resultados como uma resposta JSON
            return Ok(resultados);
        }
    }
}
