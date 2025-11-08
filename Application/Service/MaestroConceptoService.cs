using Application.Dto;
using Application.Interface;
using AutoMapper;
using Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Service
{
    public class MaestroConceptoService : IMaestroConceptoService
    {
        private readonly IMaestroConceptoRepository _repository;
        private readonly IMapper _mapper;

        public MaestroConceptoService(IMaestroConceptoRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ConceptoNovedadDto>> GetAllWithTipoAsync()
        {
            var items = await _repository.GetAllWithTipoAsync();
            return items.Select(c => new ConceptoNovedadDto
            {
                Id = c.Id,
                NombreConcepto = c.NombreConcepto,
                NombreTipo = c.TipoConcepto?.NombreTipo
            });
        }
    }
}
