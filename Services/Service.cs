namespace DependencyInjection.Services
{
    public class Service : IService
    {
        public List<string> Nomes { get; set; }

        public List<string> GetNomes(List<string> nomes)
        {
            List<string> nomesLista = new List<string>();
            foreach (string nome in nomes)
            {
                nomesLista.Add(nome);
            }

            Nomes = nomesLista;
            return Nomes;
        }

    }
}
