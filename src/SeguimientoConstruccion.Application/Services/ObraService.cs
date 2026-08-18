using System;
using System.Collections.Generic;
using System.Text;
using SeguimientoConstruccion.Application.Common;
using SeguimientoConstruccion.Application.DTOs;
using SeguimientoConstruccion.Domain.Entities;
using SeguimientoConstruccion.Infrastructure.Repositories;

namespace SeguimientoConstruccion.Application.Services
{
    public class ObraService
    {
        private readonly UnitOfWork _unitOfWork;

        public ObraService(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public APIResponse<IEnumerable<ObraDTO>> GetAllObras()
        {
            var obras = _unitOfWork.Obra.GetAll();
            var result = obras.Select(o => new ObraDTO
            {
                Id = o.Id,
                Nombre = o.Nombre,
                Provincia = o.Provincia,
                Direccion = o.Direccion,
                FechaInicio = o.FechaInicio,
                FechaFinEstimada = o.FechaFinEstimada,
                Estado = o.Estado
            });
            return APIResponse<IEnumerable<ObraDTO>>.SuccessResponse(result);
        }

        public APIResponse<ObraDTO> GetObraById(int id)
        {
            var obra = _unitOfWork.Obra.GetById(id);
            if (obra == null)
                return APIResponse<ObraDTO>.ErrorResponse("Obra no encontrada", 404);

                return APIResponse<ObraDTO>.SuccessResponse(new ObraDTO
                {
                    Id = obra.Id,
                    Nombre = obra.Nombre,
                    Provincia = obra.Provincia,
                    Direccion = obra.Direccion,
                    FechaInicio = obra.FechaInicio,
                    FechaFinEstimada = obra.FechaFinEstimada,
                    Estado = obra.Estado
                });
        }

        public APIResponse<int> CreateObra(ObraDTO dto)
        {
            if (string.IsNullOrEmpty(dto.Nombre))
                return APIResponse<int>.ErrorResponse("El nombre es requerido");

            if (string.IsNullOrEmpty(dto.Provincia))
                return APIResponse<int>.ErrorResponse("La provincia es requerida");

            if (string.IsNullOrEmpty(dto.Estado))
                return APIResponse<int>.ErrorResponse("El estado es requerido");

            var obra = new Obra
            {
                Nombre = dto.Nombre,
                Provincia = dto.Provincia,
                Direccion = dto.Direccion,
                FechaInicio = dto.FechaInicio,
                FechaFinEstimada = dto.FechaFinEstimada,
                Estado = dto.Estado
            };

            _unitOfWork.Obra.Add(obra);
            _unitOfWork.Complete();

            return APIResponse<int>.SuccessResponse(obra.Id, 201);
        }

        public APIResponse<bool> UpdateObra(int id, ObraDTO dto)
        {
            var obra = _unitOfWork.Obra.GetById(id);
            if (obra == null)
                return APIResponse<bool>.ErrorResponse("Obra no encontrada", 404);

            if (string.IsNullOrEmpty(dto.Nombre))
                return APIResponse<bool>.ErrorResponse("El nombre es requerido");

            obra.Nombre = dto.Nombre;
            obra.Provincia = dto.Provincia;
            obra.Direccion = dto.Direccion;
            obra.FechaInicio = dto.FechaInicio;
            obra.FechaFinEstimada = dto.FechaFinEstimada;
            obra.Estado = dto.Estado;

            _unitOfWork.Complete();
            return APIResponse<bool>.SuccessResponse(true);
        }

        public APIResponse<bool> DeleteObra(int id)
        {
            var obra = _unitOfWork.Obra.GetById(id);
            if (obra == null)
                return APIResponse<bool>.ErrorResponse("Obra no encontrada", 404);

            _unitOfWork.Obra.Delete(id);
            _unitOfWork.Complete();
            return APIResponse<bool>.SuccessResponse(true);
        }

    }
}
