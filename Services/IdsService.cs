using IdProvider.DataAccess;
using IdProvider.Entities;
using IdProvider.Interfaces;
using IdProvider.Models;
using Microsoft.EntityFrameworkCore;

namespace IdProvider.Services;

public class IdsService : IIdsService
{
    private DataContext _context;

    public IdsService(DataContext context)
    {
        _context = context;
    }

    public async Task Create(IdCreation newId)
    {
        // validate
        if (newId == null)
        {
            throw new ArgumentException("You must provide a valid couple prefix/initial value");
        }
        if (string.IsNullOrEmpty(newId.Prefix))
        {
            throw new ArgumentException("The prefix cannot be empty");
        }
        else if (_context.Ids.Any(i => i.Prefix == newId.Prefix))
        {
            throw new ArgumentException($"A prefix already exists for {newId.Prefix}.");
        }

        Ids dbNewId = new()
        {
            Prefix = newId.Prefix,
            CurrentId = newId.InitialId
        };

        // save id counter
        await _context.Ids.AddAsync(dbNewId);
        await _context.SaveChangesAsync();
    }

    public async Task<CurrentIdResult> GetCurrentIdByPrefix(string prefix)
    {
        Ids foundId = await GetCurrentIdByPrefixFromContext(_context, prefix);
        
        if (foundId != null)
        {
            return new CurrentIdResult
            {
                Prefix = foundId.Prefix,
                CurrentId = foundId.CurrentId
            };
        }  

        return null;      
    }

    public async Task<NewIdResult> GetNewIdByPrefix(string prefix)
    {
        Ids foundId = await GetCurrentIdByPrefixFromContext(_context, prefix);
    
        if (foundId != null)
        {
            //increments and save
            foundId.CurrentId++;
            await _context.SaveChangesAsync();

            return new NewIdResult
            {
                Prefix = foundId.Prefix,
                NewId = foundId.CurrentId
            };
        }  

        return null;
    }

    private static async Task<Ids> GetCurrentIdByPrefixFromContext(DataContext context, string prefix)
    {
        return await context.Ids.FirstOrDefaultAsync(c => c.Prefix.ToUpper() ==  prefix.ToUpper());
    }
}
