namespace StargateAPI.Business.Interfaces
{
    public interface IPersonService
    {
        Task<bool> IsPersonUnique(string name);
    }
}
