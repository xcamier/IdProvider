using IdProvider.Models;

namespace IdProvider.Interfaces;

public interface IIdsService
{
    Task<NewIdResult> GetNewIdByPrefix(string prefix);
    Task<CurrentIdResult> GetCurrentIdByPrefix(string prefix);
    Task Create(IdCreation newId);
}

