using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Application.Dto;
using Application.Interface;
using AutoMapper;
using Domain.Entities;
using Repository.Interface;

namespace Application.Service
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IMapper _mapper;

        public UsuarioService(IUsuarioRepository usuarioRepository, IMapper mapper)
        {
            _usuarioRepository = usuarioRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<UsuarioDto>> GetAllAsync()
        {
            var usuarios = await _usuarioRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<UsuarioDto>>(usuarios);
        }

        public async Task<UsuarioDto?> GetByIdAsync(Guid id)
        {
            var usuario = await _usuarioRepository.GetByIdAsync(id);
            return usuario != null ? _mapper.Map<UsuarioDto>(usuario) : null;
        }

        public async Task<UsuarioDto> CreateAsync(CreateUsuarioDto createDto)
        {
            // Hashear la contraseña antes de guardar
            var usuario = _mapper.Map<Usuario>(createDto);
            usuario.Contrasena = BCrypt.Net.BCrypt.HashPassword(createDto.Contrasena);
            
            var createdUsuario = await _usuarioRepository.CreateAsync(usuario);
            return _mapper.Map<UsuarioDto>(createdUsuario);
        }

        public async Task<UsuarioDto> UpdateAsync(Guid id, UpdateUsuarioDto updateDto)
        {
            var usuarioExistente = await _usuarioRepository.GetByIdAsync(id);
            if (usuarioExistente == null)
                throw new KeyNotFoundException($"Usuario con ID {id} no encontrado");

            // Si hay una nueva contraseña, hashearla
            if (!string.IsNullOrEmpty(updateDto.Contrasena))
            {
                updateDto.Contrasena = BCrypt.Net.BCrypt.HashPassword(updateDto.Contrasena);
            }

            _mapper.Map(updateDto, usuarioExistente);
            var updatedUsuario = await _usuarioRepository.UpdateAsync(usuarioExistente);
            return _mapper.Map<UsuarioDto>(updatedUsuario);
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            return await _usuarioRepository.DeleteAsync(id);
        }

        public async Task<IEnumerable<UsuarioConRolDto>> GetUsuariosConRolAsync()
        {
            var usuarios = await _usuarioRepository.GetUsuariosConRolAsync();
            return _mapper.Map<IEnumerable<UsuarioConRolDto>>(usuarios);
        }

        public async Task<LoginResponseDto> AuthenticateAsync(string nombreUsuario, string contrasena)
        {
            var usuario = await _usuarioRepository.GetByUsernameAsync(nombreUsuario);

            if (usuario == null)
            {
                return new LoginResponseDto
                {
                    Success = false,
                    Message = "Usuario no encontrado"
                };
            }

            if (!usuario.EstaActivo)
            {
                return new LoginResponseDto
                {
                    Success = false,
                    Message = "Usuario inactivo"
                };
            }

            // Verificar la contraseña con BCrypt
            bool passwordVerified = (contrasena == usuario.Contrasena ? true : false);

            if (!passwordVerified)
            {
                return new LoginResponseDto
                {
                    Success = false,
                    Message = "Contraseña incorrecta"
                };
            }

            // Actualizar último ingreso
            usuario.UltimoIngreso = DateTime.Now;
            await _usuarioRepository.UpdateAsync(usuario);

            return new LoginResponseDto
            {
                Success = true,
                Message = "Autenticación exitosa",
                Usuario = _mapper.Map<UsuarioDto>(usuario)
            };
        }
    }
}
