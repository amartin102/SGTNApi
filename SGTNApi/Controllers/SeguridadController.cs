using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Application.Dto;
using Application.Interface;
using Microsoft.AspNetCore.Mvc;

namespace SGTNApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SeguridadController : ControllerBase
    {
        private readonly IRolService _rolService;
        private readonly IUsuarioService _usuarioService;
        private readonly IPermisoService _permisoService;

        public SeguridadController(
            IRolService rolService,
            IUsuarioService usuarioService,
            IPermisoService permisoService)
        {
            _rolService = rolService;
            _usuarioService = usuarioService;
            _permisoService = permisoService;
        }

        #region Autenticación

        /// <summary>
        /// Endpoint para autenticar un usuario con nombre de usuario y contraseña
        /// </summary>
        [HttpPost("login")]
        public async Task<ActionResult<LoginResponseDto>> Login([FromBody] LoginDto loginDto)
        {
            try
            {
                if (string.IsNullOrEmpty(loginDto.NombreUsuario) || string.IsNullOrEmpty(loginDto.Contrasena))
                {
                    return BadRequest(new LoginResponseDto
                    {
                        Success = false,
                        Message = "Usuario y contraseña son requeridos"
                    });
                }

                var resultado = await _usuarioService.AuthenticateAsync(loginDto.NombreUsuario, loginDto.Contrasena);

                if (!resultado.Success)
                {
                    return Unauthorized(resultado);
                }

                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new LoginResponseDto
                {
                    Success = false,
                    Message = $"Error en la autenticación: {ex.Message}"
                });
            }
        }

        #endregion

        #region Roles

        [HttpGet("roles")]
        public async Task<ActionResult<IEnumerable<RolDto>>> GetAllRoles()
        {
            try
            {
                var roles = await _rolService.GetAllAsync();
                return Ok(roles);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error al obtener roles", error = ex.Message });
            }
        }

        [HttpGet("roles/{id}")]
        public async Task<ActionResult<RolDto>> GetRolById(Guid id)
        {
            try
            {
                var rol = await _rolService.GetByIdAsync(id);
                if (rol == null)
                    return NotFound(new { message = $"Rol con ID {id} no encontrado" });

                return Ok(rol);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error al obtener el rol", error = ex.Message });
            }
        }

        [HttpPost("roles")]
        public async Task<ActionResult<RolDto>> CreateRol([FromBody] CreateRolDto createDto)
        {
            try
            {
                var rol = await _rolService.CreateAsync(createDto);
                return CreatedAtAction(nameof(GetRolById), new { id = rol.IdRol }, rol);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error al crear el rol", error = ex.Message });
            }
        }

        [HttpPut("roles/{id}")]
        public async Task<ActionResult<RolDto>> UpdateRol(Guid id, [FromBody] UpdateRolDto updateDto)
        {
            try
            {
                var rol = await _rolService.UpdateAsync(id, updateDto);
                return Ok(rol);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error al actualizar el rol", error = ex.Message });
            }
        }

        [HttpDelete("roles/{id}")]
        public async Task<ActionResult> DeleteRol(Guid id)
        {
            try
            {
                var result = await _rolService.DeleteAsync(id);
                if (!result)
                    return NotFound(new { message = $"Rol con ID {id} no encontrado" });

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error al eliminar el rol", error = ex.Message });
            }
        }

        /// <summary>
        /// Endpoint para listar todos los roles con sus permisos asociados
        /// </summary>
        [HttpGet("roles-con-permisos")]
        public async Task<ActionResult<IEnumerable<RolConPermisosDto>>> GetRolesConPermisos()
        {
            try
            {
                var roles = await _rolService.GetRolesConPermisosAsync();
                return Ok(roles);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error al obtener roles con permisos", error = ex.Message });
            }
        }

        [HttpPost("roles/asignar-permisos")]
        public async Task<ActionResult> AsignarPermisos([FromBody] AsignarPermisosRolDto asignarDto)
        {
            try
            {
                var result = await _rolService.AsignarPermisosAsync(asignarDto);
                if (!result)
                    return BadRequest(new { message = "Error al asignar permisos" });

                return Ok(new { message = "Permisos asignados exitosamente" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error al asignar permisos", error = ex.Message });
            }
        }

        #endregion

        #region Usuarios

        [HttpGet("usuarios")]
        public async Task<ActionResult<IEnumerable<UsuarioDto>>> GetAllUsuarios()
        {
            try
            {
                var usuarios = await _usuarioService.GetAllAsync();
                return Ok(usuarios);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error al obtener usuarios", error = ex.Message });
            }
        }

        [HttpGet("usuarios/{id}")]
        public async Task<ActionResult<UsuarioDto>> GetUsuarioById(Guid id)
        {
            try
            {
                var usuario = await _usuarioService.GetByIdAsync(id);
                if (usuario == null)
                    return NotFound(new { message = $"Usuario con ID {id} no encontrado" });

                return Ok(usuario);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error al obtener el usuario", error = ex.Message });
            }
        }

        [HttpPost("usuarios")]
        public async Task<ActionResult<UsuarioDto>> CreateUsuario([FromBody] CreateUsuarioDto createDto)
        {
            try
            {
                var usuario = await _usuarioService.CreateAsync(createDto);
                return CreatedAtAction(nameof(GetUsuarioById), new { id = usuario.IdUsuario }, usuario);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error al crear el usuario", error = ex.Message });
            }
        }

        [HttpPut("usuarios/{id}")]
        public async Task<ActionResult<UsuarioDto>> UpdateUsuario(Guid id, [FromBody] UpdateUsuarioDto updateDto)
        {
            try
            {
                var usuario = await _usuarioService.UpdateAsync(id, updateDto);
                return Ok(usuario);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error al actualizar el usuario", error = ex.Message });
            }
        }

        [HttpDelete("usuarios/{id}")]
        public async Task<ActionResult> DeleteUsuario(Guid id)
        {
            try
            {
                var result = await _usuarioService.DeleteAsync(id);
                if (!result)
                    return NotFound(new { message = $"Usuario con ID {id} no encontrado" });

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error al eliminar el usuario", error = ex.Message });
            }
        }

        /// <summary>
        /// Endpoint para listar todos los usuarios con su rol asociado
        /// </summary>
        [HttpGet("usuarios-con-rol")]
        public async Task<ActionResult<IEnumerable<UsuarioConRolDto>>> GetUsuariosConRol()
        {
            try
            {
                var usuarios = await _usuarioService.GetUsuariosConRolAsync();
                return Ok(usuarios);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error al obtener usuarios con rol", error = ex.Message });
            }
        }

        #endregion

        #region Permisos

        [HttpGet("permisos")]
        public async Task<ActionResult<IEnumerable<PermisoDto>>> GetAllPermisos()
        {
            try
            {
                var permisos = await _permisoService.GetAllAsync();
                return Ok(permisos);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error al obtener permisos", error = ex.Message });
            }
        }

        [HttpGet("permisos/{id}")]
        public async Task<ActionResult<PermisoDto>> GetPermisoById(Guid id)
        {
            try
            {
                var permiso = await _permisoService.GetByIdAsync(id);
                if (permiso == null)
                    return NotFound(new { message = $"Permiso con ID {id} no encontrado" });

                return Ok(permiso);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error al obtener el permiso", error = ex.Message });
            }
        }

        [HttpPost("permisos")]
        public async Task<ActionResult<PermisoDto>> CreatePermiso([FromBody] CreatePermisoDto createDto)
        {
            try
            {
                var permiso = await _permisoService.CreateAsync(createDto);
                return CreatedAtAction(nameof(GetPermisoById), new { id = permiso.IdPermiso }, permiso);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error al crear el permiso", error = ex.Message });
            }
        }

        [HttpPut("permisos/{id}")]
        public async Task<ActionResult<PermisoDto>> UpdatePermiso(Guid id, [FromBody] UpdatePermisoDto updateDto)
        {
            try
            {
                var permiso = await _permisoService.UpdateAsync(id, updateDto);
                return Ok(permiso);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error al actualizar el permiso", error = ex.Message });
            }
        }

        [HttpDelete("permisos/{id}")]
        public async Task<ActionResult> DeletePermiso(Guid id)
        {
            try
            {
                var result = await _permisoService.DeleteAsync(id);
                if (!result)
                    return NotFound(new { message = $"Permiso con ID {id} no encontrado" });

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error al eliminar el permiso", error = ex.Message });
            }
        }

        #endregion
    }
}
