namespace PrestadorOnline.Services
{
    public interface IConsultaEspecialidade
    {
        Task<bool> EspecialidadeExistsAsync(string nome);

    }
}
