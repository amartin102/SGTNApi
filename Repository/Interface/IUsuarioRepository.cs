using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Entities;

namespace Repository.Interface
{
    public interface IUsuarioRepository
    {
        Task<IEnumerable<Usuario>> GetAllAsync();
        Task<Usuario?> GetByIdAsync(Guid id);
        Task<Usuario> CreateAsync(Usuario usuario);
        Task<Usuario> UpdateAsync(Usuario usuario);
        Task<bool> DeleteAsync(Guid id);
        Task<IEnumerable<Usuario>> GetUsuariosConRolAsync();
        Task<Usuario?> GetByUsernameAsync(string username);
    }
}
