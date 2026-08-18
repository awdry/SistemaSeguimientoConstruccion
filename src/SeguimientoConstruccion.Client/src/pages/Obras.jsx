import { useState, useEffect } from 'react';
import * as XLSX from 'xlsx';
import api from '../api';

const PROVINCIAS = [
  'Azua','Bahoruco','Barahona','Dajabón','Distrito Nacional','Duarte',
  'Elías Piña','El Seibo','Espaillat','Hato Mayor','Hermanas Mirabal',
  'Independencia','La Altagracia','La Romana','La Vega','María Trinidad Sánchez',
  'Monseñor Nouel','Monte Cristi','Monte Plata','Pedernales','Peravia',
  'Puerto Plata','Samaná','San Cristóbal','San José de Ocoa','San Juan',
  'San Pedro de Macorís','Sánchez Ramírez','Santiago','Santiago Rodríguez',
  'Santo Domingo','Valverde'
];

const empty = { nombre: '', provincia: '', direccion: '', estado: 'En progreso', fechaInicio: '', fechaFinEstimada: '' };

function Toast({ toast }) {
  if (!toast) return null;
  return (
    <div style={{
      position: 'fixed', top: '1rem', right: '1rem', zIndex: 9999,
      background: toast.type === 'error' ? '#e74c3c' : '#27ae60',
      color: 'white', padding: '0.75rem 1.5rem', borderRadius: '8px',
      boxShadow: '0 4px 12px rgba(0,0,0,0.2)', fontSize: '0.9rem', fontWeight: 500
    }}>{toast.msg}</div>
  );
}

export default function Obras() {
  const [obras, setObras] = useState([]);
  const [modal, setModal] = useState(false);
  const [form, setForm] = useState(empty);
  const [editId, setEditId] = useState(null);
  const [toast, setToast] = useState(null);

  useEffect(() => { load(); }, []);

  function showToast(msg, type = 'success') {
    setToast({ msg, type });
    setTimeout(() => setToast(null), 3000);
  }

  async function load() {
    const r = await api.get('/Obras');
    setObras(r.data.data || []);
  }

  function openCreate() { setForm(empty); setEditId(null); setModal(true); }

  function openEdit(o) {
    setForm({
      nombre: o.nombre,
      provincia: o.provincia || '',
      direccion: o.direccion || '',
      estado: o.estado,
      fechaInicio: o.fechaInicio?.split('T')[0] || '',
      fechaFinEstimada: o.fechaFinEstimada?.split('T')[0] || ''
    });
    setEditId(o.id);
    setModal(true);
  }

  async function handleSubmit(e) {
    e.preventDefault();
    const payload = {
      ...form,
      fechaInicio: new Date(form.fechaInicio).toISOString(),
      fechaFinEstimada: new Date(form.fechaFinEstimada).toISOString()
    };
    if (editId) {
      await api.put(`/Obras/${editId}`, { id: editId, ...payload });
      showToast('Obra actualizada correctamente');
    } else {
      await api.post('/Obras', { id: 0, ...payload });
      showToast('Obra creada correctamente');
    }
    setModal(false);
    load();
  }

  async function handleDelete(id) {
    if (confirm('¿Estás seguro de que deseas eliminar esta obra? Esta acción no se puede deshacer.')) {
      await api.delete(`/Obras/${id}`);
      showToast('Obra eliminada', 'error');
      load();
    }
  }

  function exportarExcel() {
    const data = obras.map(o => ({
      'Nombre': o.nombre,
      'Provincia': o.provincia,
      'Dirección': o.direccion,
      'Estado': o.estado,
      'Fecha Inicio': new Date(o.fechaInicio).toLocaleDateString(),
      'Fecha Fin Estimada': new Date(o.fechaFinEstimada).toLocaleDateString()
    }));
    const ws = XLSX.utils.json_to_sheet(data);
    const wb = XLSX.utils.book_new();
    XLSX.utils.book_append_sheet(wb, ws, 'Obras');
    XLSX.writeFile(wb, 'Obras_SistemaConstruccion.xlsx');
    showToast('Archivo Excel exportado correctamente');
  }

  return (
    <>
      <Toast toast={toast} />
      <div style={{ display: 'flex', justifyContent: 'space-between', alignItems: 'center', marginBottom: '1.5rem' }}>
        <h1 className="page-title" style={{ margin: 0 }}>Obras</h1>
        <div style={{ display: 'flex', gap: '0.5rem' }}>
          <button className="btn btn-success" onClick={exportarExcel}>⬇ Exportar Excel</button>
          <button className="btn btn-primary" onClick={openCreate}>+ Nueva Obra</button>
        </div>
      </div>

      <div className="card">
        <table>
          <thead>
            <tr>
              <th>Nombre</th>
              <th>Provincia</th>
              <th>Dirección</th>
              <th>Estado</th>
              <th>Fecha Inicio</th>
              <th>Fecha Fin Est.</th>
              <th>Acciones</th>
            </tr>
          </thead>
          <tbody>
            {obras.map(o => (
              <tr key={o.id}>
                <td>{o.nombre}</td>
                <td>{o.provincia}</td>
                <td>{o.direccion}</td>
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
                <td>
                  <button className="btn btn-warning" style={{ marginRight: '0.5rem' }} onClick={() => openEdit(o)}>Editar</button>
                  <button className="btn btn-danger" onClick={() => handleDelete(o.id)}>Eliminar</button>
                </td>
              </tr>
            ))}
            {obras.length === 0 && (
              <tr><td colSpan="7" style={{ textAlign: 'center', color: '#aaa' }}>No hay obras registradas</td></tr>
            )}
          </tbody>
        </table>
      </div>

      {modal && (
        <div className="modal-overlay">
          <div className="modal">
            <h2>{editId ? 'Editar Obra' : 'Nueva Obra'}</h2>
            <form onSubmit={handleSubmit}>
              <div className="form-group">
                <label>Nombre</label>
                <input required value={form.nombre} onChange={e => setForm({ ...form, nombre: e.target.value })} />
              </div>
              <div className="form-group">
                <label>Provincia</label>
                <select required value={form.provincia} onChange={e => setForm({ ...form, provincia: e.target.value })}>
                  <option value="">Selecciona una provincia</option>
                  {PROVINCIAS.map(p => <option key={p} value={p}>{p}</option>)}
                </select>
              </div>
              <div className="form-group">
                <label>Dirección exacta</label>
                <input value={form.direccion} placeholder="Ej: Av. Winston Churchill #45" onChange={e => setForm({ ...form, direccion: e.target.value })} />
              </div>
              <div className="form-group">
                <label>Estado</label>
                <select value={form.estado} onChange={e => setForm({ ...form, estado: e.target.value })}>
                  <option>En progreso</option>
                  <option>Planificada</option>
                  <option>Completada</option>
                  <option>Pausada</option>
                </select>
              </div>
              <div className="form-group">
                <label>Fecha Inicio</label>
                <input type="date" required value={form.fechaInicio} onChange={e => setForm({ ...form, fechaInicio: e.target.value })} />
              </div>
              <div className="form-group">
                <label>Fecha Fin Estimada</label>
                <input type="date" required value={form.fechaFinEstimada} onChange={e => setForm({ ...form, fechaFinEstimada: e.target.value })} />
              </div>
              <div className="modal-actions">
                <button type="button" className="btn btn-secondary" onClick={() => setModal(false)}>Cancelar</button>
                <button type="submit" className="btn btn-primary">{editId ? 'Guardar' : 'Crear'}</button>
              </div>
            </form>
          </div>
        </div>
      )}
    </>
  );
}