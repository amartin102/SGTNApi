using Application.Interface;
using Domain.Entities;
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
        public readonly IParameterMasterRepository _parameterMasterService;
        public ParameterMasterService()
        {
           // _parameterMasterService = parameterMasterService;
        }
        public async Task<bool> CreateParameter(ParameterMasterDto entity)
        {
            try
            {
                entity.strUsuarioCreador = "Sistema";
                entity.datFechaCreacion = DateTime.Now;

                var headerOrder = await CreateParameter(entity);

                return (headerOrder != null) ? true : false;
            }            
            catch (Exception ex)
            {
              
                throw;
            }
        }

        public async Task<ParameterMasterDto> GetOrderById(Guid id)
        {
            ParameterMasterDto itemDto = new ParameterMasterDto();
            var result = await GetParameterById(id);

            if (result != null) //Seteamos los campos al DTO
            {
                itemDto.strIdParametro = result.strIdParametro;
                itemDto.strCodParametro = result.strCodParametro;
                itemDto.datFechaCreacion   = result.datFechaCreacion;
            }

            return itemDto;
        }

        public async Task<bool> UpdateOrder(ParameterMasterDto dto)
        {
            try
            {
                //1. Consultar el encabezadO
                var headerOrder = await GetParameterById(dto.strIdParametro);
                               
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
