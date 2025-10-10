using Application.Interface;
using Domain.Entities;
using Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace Application.Service
{
    public class ParameterMasterService: IParameterMasterService
    {
        public readonly IParameterMasterRepository _parameterMasterRepository;
        public ParameterMasterService(IParameterMasterRepository parameterMasterRepository)
        {
            _parameterMasterRepository = parameterMasterRepository;
        }
        public async Task<bool> CreateParameter(ParameterMasterDto entity)
        {
            try
            {
                ParameterMasterEntity entityObj = new ParameterMasterEntity()
                {
                    strIdParametro = entity.strIdParametro,
                    intIdTipoDato = entity.intIdTipoDato,
                    intIdNivelInconsistencia = entity.intIdNivelInconsistencia,
                    strCodParametro = entity.strCodParametro,
                    strPermisoConsultar = entity.strPermisoConsultar,
                    strModificadoPor = entity.strModificadoPor,
                    strPermisoModificar = entity.strPermisoModificar,
                    datFechaCreacion = DateTime.Now,
                    datFechaModificacion = DateTime.Now,
                    strUsuarioCreador = entity.strUsuarioCreador
                };             

                var headerOrder = await _parameterMasterRepository.CreateParameter(entityObj);

                return (headerOrder != null ? true : false);
            }            
            catch (Exception ex)
            {
              
                throw;
            }
        }

        public async Task<ParameterMasterDto> GetParameterById(Guid id)
        {
            ParameterMasterDto itemDto = new ParameterMasterDto();
            var result = await _parameterMasterRepository.GetParameterById(id);

            if (result != null) //Seteamos los campos al DTO
            {
                itemDto.strIdParametro = result.strIdParametro;
                itemDto.strCodParametro = result.strCodParametro;
                itemDto.datFechaCreacion   = result.datFechaCreacion;
            }

            return itemDto;
        }


        public async Task<bool> UpdateParameter(ParameterMasterDto dto)
        {
            try
            {
                //1. Consultar el encabezadO
                var headerOrder = await _parameterMasterRepository.GetParameterById(dto.strIdParametro);
                               
                headerOrder.datFechaModificacion = DateTime.Now;

                return await UpdateParameter(dto);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

    }
}
