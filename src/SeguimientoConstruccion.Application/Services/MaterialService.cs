using System;
using System.Collections.Generic;
using System.Text;
using SeguimientoConstruccion.Application.Common;
using SeguimientoConstruccion.Application.DTOs;
using SeguimientoConstruccion.Domain.Entities;
using SeguimientoConstruccion.Infrastructure.Repositories;

namespace SeguimientoConstruccion.Application.Services
{
    public class MaterialService
    {
        private readonly UnitOfWork _unitOfWork;

        public MaterialService(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public APIResponse<IEnumerable<MaterialDTO>> GetAllMateriales()
        {
            var materiales = _unitOfWork.Material.GetAll();
            var result = materiales.Select(m => new MaterialDTO
            {
                Id = m.Id,
                Nombre = m.Nombre,
                UnidadMedida = m.UnidadMedida,
                CostoUnitario = m.CostoUnitario,
                CantidadUsada = m.CantidadUsada,
                CostoTotal = m.CostoTotal,
                TareaId = m.TareaId
            });
            return APIResponse<IEnumerable<MaterialDTO>>.SuccessResponse(result);
        }

        public APIResponse<MaterialDTO> GetMaterialById(int id)
        {
            var m = _unitOfWork.Material.GetById(id);
            if (m == null)
                return APIResponse<MaterialDTO>.ErrorResponse("Material no encontrado", 404);

            return APIResponse<MaterialDTO>.SuccessResponse(new MaterialDTO
            {
                Id = m.Id,
                Nombre = m.Nombre,
                UnidadMedida = m.UnidadMedida,
                CostoUnitario = m.CostoUnitario,
                CantidadUsada = m.CantidadUsada,
                CostoTotal = m.CostoTotal,
                TareaId = m.TareaId
            });
        }

        public APIResponse<int> CreateMaterial(MaterialDTO dto)
        {
            if (string.IsNullOrEmpty(dto.Nombre))
                return APIResponse<int>.ErrorResponse("El nombre es requerido");

            if (dto.CostoUnitario <= 0)
                return APIResponse<int>.ErrorResponse("El costo unitario debe ser mayor a 0");

            if (dto.CantidadUsada <= 0)
                return APIResponse<int>.ErrorResponse("La cantidad debe ser mayor a 0");

            if (dto.TareaId <= 0)
                return APIResponse<int>.ErrorResponse("Debe seleccionar una tarea");

            var material = new Material
            {
                Nombre = dto.Nombre,
                UnidadMedida = dto.UnidadMedida,
                CostoUnitario = dto.CostoUnitario,
                CantidadUsada = dto.CantidadUsada,
                TareaId = dto.TareaId
            };

            _unitOfWork.Material.Add(material);
            _unitOfWork.Complete();

            return APIResponse<int>.SuccessResponse(material.Id, 201);
        }

        public APIResponse<bool> UpdateMaterial(int id, MaterialDTO dto)
        {
            var material = _unitOfWork.Material.GetById(id);
            if (material == null)
                return APIResponse<bool>.ErrorResponse("Material no encontrado", 404);

            if (string.IsNullOrEmpty(dto.Nombre))
                return APIResponse<bool>.ErrorResponse("El nombre es requerido");

            material.Nombre = dto.Nombre;
            material.UnidadMedida = dto.UnidadMedida;
            material.CostoUnitario = dto.CostoUnitario;
            material.CantidadUsada = dto.CantidadUsada;
            material.TareaId = dto.TareaId;

            _unitOfWork.Complete();
            return APIResponse<bool>.SuccessResponse(true);
        }

        public APIResponse<bool> DeleteMaterial(int id)
        {
            var material = _unitOfWork.Material.GetById(id);
            if (material == null)
                return APIResponse<bool>.ErrorResponse("Material no encontrado", 404);

            _unitOfWork.Material.Delete(id);
            _unitOfWork.Complete();
            return APIResponse<bool>.SuccessResponse(true);
        }
    }
}
