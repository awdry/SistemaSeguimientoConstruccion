using System;
using System.Collections.Generic;
using System.Text;
using SeguimientoConstruccion.Application.Common;
using SeguimientoConstruccion.Application.DTOs;
using SeguimientoConstruccion.Domain.Entities;
using SeguimientoConstruccion.Infrastructure.Repositories;

namespace SeguimientoConstruccion.Application.Services
{
    public class ResponsableService
    {
        private readonly UnitOfWork _unitOfWork;

        public ResponsableService(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public APIResponse<IEnumerable<ResponsableDTO>> GetAllResponsables()
        {
            var responsables = _unitOfWork.Responsable.GetAll();
            var result = responsables.Select(r => new ResponsableDTO
            {
                Id = r.Id,
                Nombre = r.Nombre,
                Rol = r.Rol,
                Contacto = r.Contacto
            });
            return APIResponse<IEnumerable<ResponsableDTO>>.SuccessResponse(result);
        }

        public APIResponse<ResponsableDTO> GetResponsableById(int id)
        {
            var r = _unitOfWork.Responsable.GetById(id);
            if (r == null)
                return APIResponse<ResponsableDTO>.ErrorResponse("Responsable no encontrado", 404);

            return APIResponse<ResponsableDTO>.SuccessResponse(new ResponsableDTO
            {
                Id = r.Id,
                Nombre = r.Nombre,
                Rol = r.Rol,
                Contacto = r.Contacto
            });
        }

        public APIResponse<int> CreateResponsable(ResponsableDTO dto)
        {
            if (string.IsNullOrEmpty(dto.Nombre))
                return APIResponse<int>.ErrorResponse("El nombre es requerido");

            if (string.IsNullOrEmpty(dto.Rol))
                return APIResponse<int>.ErrorResponse("El rol es requerido");

            var responsable = new Responsable
            {
                Nombre = dto.Nombre,
                Rol = dto.Rol,
                Contacto = dto.Contacto
            };

            _unitOfWork.Responsable.Add(responsable);
            _unitOfWork.Complete();

            return APIResponse<int>.SuccessResponse(responsable.Id, 201);
        }

        public APIResponse<bool> UpdateResponsable(int id, ResponsableDTO dto)
        {
            var responsable = _unitOfWork.Responsable.GetById(id);
            if (responsable == null)
                return APIResponse<bool>.ErrorResponse("Responsable no encontrado", 404);

            if (string.IsNullOrEmpty(dto.Nombre))
                return APIResponse<bool>.ErrorResponse("El nombre es requerido");

            responsable.Nombre = dto.Nombre;
            responsable.Rol = dto.Rol;
            responsable.Contacto = dto.Contacto;

            _unitOfWork.Complete();
            return APIResponse<bool>.SuccessResponse(true);
        }

        public APIResponse<bool> DeleteResponsable(int id)
        {
            var responsable = _unitOfWork.Responsable.GetById(id);
            if (responsable == null)
                return APIResponse<bool>.ErrorResponse("Responsable no encontrado", 404);

            _unitOfWork.Responsable.Delete(id);
            _unitOfWork.Complete();
            return APIResponse<bool>.SuccessResponse(true);
        }
    }
}