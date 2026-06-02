using CoreWebSample.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreWebSample.Services.Abstract
{
    public interface IPersonaService
    {
        Task<IEnumerable<Persona>> GetAllAsync();
        Task<Persona?> GetByIdAsync(string id);
        Task<int> InsertAsync(Persona persona);
        Task<int> UpdateAsync(Persona persona);
        Task<int> DeleteAsync(string id);
    }
}
