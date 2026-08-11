import { useState, useEffect } from 'react';
import api from '../api';

export default function Dashboard() {
  const [obras, setObras] = useState([]);
  const [tareas, setTareas] = useState([]);

  useEffect(() => {
    api.get('/Obras').then(r => setObras(r.data.data || []));
    api.get('/Tareas').then(r => setTareas(r.data.data || []));
  }, []);

  const enProgreso = obras.filter(o => o.estado === 'En progreso').length;
  const completadas = obras.filter(o => o.estado === 'Completada').length;

  return (
    <>
      <h1 className="page-title">Dashboard</h1>
      <div className="dashboard-grid">
        <div className="stat-card">
          <div className="number">{obras.length}</div>
          <div className="label">Total Obras</div>
        </div>
        <div className="stat-card">
          <div className="number" style={{color:'#27ae60'}}>{enProgreso}</div>
          <div className="label">En Progreso</div>
        </div>
        <div className="stat-card">
          <div className="number" style={{color:'#17a2b8'}}>{completadas}</div>
          <div className="label">Completadas</div>
        </div>
        <div className="stat-card">
          <div className="number" style={{color:'#f39c12'}}>{tareas.length}</div>
          <div className="label">Total Tareas</div>
        </div>
      </div>

      <div className="card">
        <h2 style={{marginBottom:'1rem', fontSize:'1.1rem'}}>Obras Recientes</h2>
        <table>
          <thead>
            <tr>
              <th>Nombre</th>
              <th>Ubicación</th>
              <th>Estado</th>
              <th>Fecha Inicio</th>
            </tr>
          </thead>
          <tbody>
            {obras.slice(0, 5).map(o => (
              <tr key={o.id}>
                <td>{o.nombre}</td>
                <td>{o.ubicacion}</td>
                <td>
                  <span className={`badge ${o.estado === 'En progreso' ? 'badge-warning' : o.estado === 'Completada' ? 'badge-success' : 'badge-info'}`}>
                    {o.estado}
                  </span>
                </td>
                <td>{new Date(o.fechaInicio).toLocaleDateString()}</td>
              </tr>
            ))}
          </tbody>
        </table>
      </div>
    </>
  );
}