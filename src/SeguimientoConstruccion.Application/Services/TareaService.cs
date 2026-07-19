using System;
using System.Collections.Generic;
using System.Text;
using SeguimientoConstruccion.Application.Common;
using SeguimientoConstruccion.Application.DTOs;
using SeguimientoConstruccion.Domain.Entities;
using SeguimientoConstruccion.Infrastructure.Repositories;

namespace SeguimientoConstruccion.Application.Services
{
    public class TareaService
    {
        private readonly UnitOfWork _unitOfWork;

        public TareaService(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public APIResponse<IEnumerable<TareaDTO>> GetAllTareas()
        {
            var tareas = _unitOfWork.Tarea.GetAll();
            var result = tareas.Select(t => new TareaDTO
            {
                Id = t.Id,
                Descripcion = t.Descripcion,
                FechaInicio = t.FechaInicio,
                FechaFin = t.FechaFin,
                PorcentajeAvance = t.PorcentajeAvance,
                ObraId = t.ObraId,
                ResponsableId = t.ResponsableId

            });
            return APIResponse<IEnumerable<TareaDTO>>.SuccessResponse(result);
        }

        public APIResponse<TareaDTO> GetTareaById(int id)
        {
            var tarea = _unitOfWork.Tarea.GetById(id);
            if (tarea == null)
                return APIResponse<TareaDTO>.ErrorResponse("Tarea no encontrada", 404);

            return APIResponse<TareaDTO>.SuccessResponse(new TareaDTO
            {
                Id = tarea.Id,
                Descripcion = tarea.Descripcion,
                FechaInicio = tarea.FechaInicio,
                FechaFin = tarea.FechaFin,
                PorcentajeAvance = tarea.PorcentajeAvance,
                ObraId = tarea.ObraId,
                ResponsableId = tarea.ResponsableId
            });
        }

        public APIResponse<int> CreateTarea(TareaDTO dto)
        {
            if (string.IsNullOrEmpty(dto.Descripcion))
                return APIResponse<int>.ErrorResponse("La descripción es requerida");

            if (dto.ObraId <= 0)
                return APIResponse<int>.ErrorResponse("El ID de obra es requerido");

            if (dto.PorcentajeAvance < 0 || dto.PorcentajeAvance > 100)
                return APIResponse<int>.ErrorResponse("El porcentaje de avance debe estar entre 0 y 100");

            var tarea = new Tarea
            {
                Descripcion = dto.Descripcion,
                FechaInicio = dto.FechaInicio,
                FechaFin = dto.FechaFin,
                PorcentajeAvance = dto.PorcentajeAvance,
                ObraId = dto.ObraId,
                ResponsableId = dto.ResponsableId
            };

            _unitOfWork.Tarea.Add(tarea);
            _unitOfWork.Complete();

            return APIResponse<int>.SuccessResponse(tarea.Id, 201);

        }

        public APIResponse<bool> UpdateTarea(int id, TareaDTO dto)
        {
            var tarea = _unitOfWork.Tarea.GetById(id);
            if (tarea == null)
                return APIResponse<bool>.ErrorResponse("Tarea no encontrada", 404);

            if (string.IsNullOrEmpty(dto.Descripcion))
                return APIResponse<bool>.ErrorResponse("La descripción es requerida");

            if (dto.PorcentajeAvance < 0 || dto.PorcentajeAvance > 100)
                return APIResponse<bool>.ErrorResponse("El porcentaje de avance debe estar entre 0 y 100");

            tarea.Descripcion = dto.Descripcion;
            tarea.FechaInicio = dto.FechaInicio;
            tarea.FechaFin = dto.FechaFin;
            tarea.PorcentajeAvance = dto.PorcentajeAvance;
            tarea.ObraId = dto.ObraId;
            tarea.ResponsableId = dto.ResponsableId;

            _unitOfWork.Complete();
            return APIResponse<bool>.SuccessResponse(true);
       
        }

        public APIResponse<bool> DeleteTarea(int id)
        {
            var tarea = _unitOfWork.Tarea.GetById(id);
            if (tarea == null)
                return APIResponse<bool>.ErrorResponse("Tarea no encontrada", 404);

            _unitOfWork.Tarea.Delete(id);
            _unitOfWork.Complete();
            return APIResponse<bool>.SuccessResponse(true);
        }
    }
}
