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
  const tareasPendientes = tareas.filter(t => t.porcentajeAvance < 100).length;

  return (
    <>
      <h1 className="page-title">Dashboard</h1>
<div className="dashboard-grid">
  <div className="stat-card">
    <div style={{fontSize:'2.5rem', fontWeight:700, color:'#000000'}}>{obras.length}</div>
    <div className="label">Total Obras</div>
  </div>
  <div className="stat-card">
    <div style={{fontSize:'2.5rem', fontWeight:700, color:'#f39c12'}}>{enProgreso}</div>
    <div className="label">En Progreso</div>
  </div>
  <div className="stat-card">
    <div style={{fontSize:'2.5rem', fontWeight:700, color:'#27ae60'}}>{completadas}</div>
    <div className="label">Completadas</div>
  </div>
  <div className="stat-card">
    <div style={{fontSize:'2.5rem', fontWeight:700, color:'#68676c'}}>{tareas.length}</div>
    <div className="label">Total Tareas</div>
  </div>
  <div className="stat-card">
    <div style={{fontSize:'2.5rem', fontWeight:700, color:'#e74c3c'}}>{tareasPendientes}</div>
    <div className="label">Tareas Pendientes</div>
  </div>
</div>

      <div className="card">
        <h2 style={{marginBottom:'1rem', fontSize:'1.1rem'}}>Obras Recientes</h2>
        <table>
          <thead>
            <tr>
              <th>Nombre</th>
              <th>Provincia</th>
              <th>Estado</th>
              <th>Fecha Inicio</th>
              <th>Fecha Fin Est.</th>
            </tr>
          </thead>
          <tbody>
            {obras.slice(0, 5).map(o => (
              <tr key={o.id}>
                <td>{o.nombre}</td>
                <td>{o.provincia}</td>
                <td>
            <span className={`badge ${
            o.estado === 'En progreso' ? 'badge-warning' : 
            o.estado === 'Completada' ? 'badge-success' : 
            o.estado === 'Pausada' ? 'badge-danger' : 
            'badge-info'
            }`}>
            {o.estado}
            </span>
                </td>
                <td>{new Date(o.fechaInicio).toLocaleDateString()}</td>
                <td>{new Date(o.fechaFinEstimada).toLocaleDateString()}</td>
              </tr>
            ))}
            {obras.length === 0 && (
              <tr><td colSpan="5" style={{textAlign:'center', color:'#aaa'}}>No hay obras registradas</td></tr>
            )}
          </tbody>
        </table>
      </div>

      <div className="card" style={{marginTop:'1rem'}}>
        <h2 style={{marginBottom:'1rem', fontSize:'1.1rem'}}>Tareas Recientes</h2>
        <table>
          <thead>
            <tr>
              <th>Descripción</th>
              <th>Avance</th>
              <th>Fecha Fin</th>
            </tr>
          </thead>
          <tbody>
            {tareas.slice(0, 5).map(t => (
              <tr key={t.id}>
                <td>{t.descripcion}</td>
                <td>
                  <div style={{display:'flex', alignItems:'center', gap:'0.5rem'}}>
                    <div style={{background:'#eee', borderRadius:'10px', height:'8px', width:'80px'}}>
                      <div style={{background: t.porcentajeAvance === 100 ? '#27ae60' : '#f39c12', height:'8px', borderRadius:'10px', width:`${t.porcentajeAvance}%`}} />
                    </div>
                    <span style={{fontSize:'0.8rem'}}>{t.porcentajeAvance}%</span>
                  </div>
                </td>
                <td>
                  {t.fechaFin
                    ? <span className="badge badge-success">{new Date(t.fechaFin).toLocaleDateString()}</span>
                    : <span className="badge badge-info">Pendiente</span>
                  }
                </td>
              </tr>
            ))}
            {tareas.length === 0 && (
              <tr><td colSpan="3" style={{textAlign:'center', color:'#aaa'}}>No hay tareas registradas</td></tr>
            )}
          </tbody>
        </table>
      </div>
    </>
  );
}