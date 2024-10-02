namespace PrestadorOnline.Services
{
    public interface IConsultaServico
    {
        Task<bool> ServicoExistsAsync(string nome);
    }
}
