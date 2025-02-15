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
            List<string> nomesLista = new List<string>();

            //Separa o seu código em outra Thread e vai executar esse código separado, não travando sua outra thread
            //Aqui por exemplo está adicionando o nome depois da thread dormir por 3000 milisegundos
            await Task.Run(() => {
                Thread.Sleep(3000); // Simula uma operação demorada
                nomesLista.Add("Eduarda");
            });
;
            var result = _service.GetNomes(nomesLista);

            //Assincronísmo 
            //Await deve ser utilizado em toda chamada de um método que retorne Task ou Task<T>
            //(Em caso de métodos sincronos, chame o await e transforme o resultado em Task<T> com o Task.FromResult(result);)
            //Utilize pelo menos 1 await em um método sincrono, o await faz com que o método "pare"
            //sua execução temporariamente, até que a Task termine, mas ao mesmo tempo, não bloqueia a thread
            // deixando que outras operações possam acontecer
            //Utilize o Task.FromResult para transformar um resultado não assincrono para assincrono (para o tipo Task)
            return await Task.FromResult(result);
        }
    }
}
