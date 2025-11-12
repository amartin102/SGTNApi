using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Application.Dto;

namespace Application.Interface
{
    public interface IUsuarioService
    {
        Task<IEnumerable<UsuarioDto>> GetAllAsync();
        Task<UsuarioDto?> GetByIdAsync(Guid id);
        Task<UsuarioDto> CreateAsync(CreateUsuarioDto createDto);
        Task<UsuarioDto> UpdateAsync(Guid id, UpdateUsuarioDto updateDto);
        Task<bool> DeleteAsync(Guid id);
        Task<IEnumerable<UsuarioConRolDto>> GetUsuariosConRolAsync();
        Task<LoginResponseDto> AuthenticateAsync(string nombreUsuario, string contrasena);
    }
}
