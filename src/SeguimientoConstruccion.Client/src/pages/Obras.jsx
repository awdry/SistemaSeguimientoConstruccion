import { useState, useEffect } from 'react';
import api from '../api';

const empty = { nombre: '', ubicacion: '', estado: 'En progreso', fechaInicio: '', fechaFinEstimada: '' };

export default function Obras() {
  const [obras, setObras] = useState([]);
  const [modal, setModal] = useState(false);
  const [form, setForm] = useState(empty);
  const [editId, setEditId] = useState(null);

  useEffect(() => { load(); }, []);

  async function load() {
    const r = await api.get('/Obras');
    setObras(r.data.data || []);
  }

  function openCreate() { setForm(empty); setEditId(null); setModal(true); }

  function openEdit(o) {
    setForm({
      nombre: o.nombre,
      ubicacion: o.ubicacion,
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
    } else {
      await api.post('/Obras', { id: 0, ...payload });
    }
    setModal(false);
    load();
  }

  async function handleDelete(id) {
    if (confirm('¿Eliminar esta obra?')) {
      await api.delete(`/Obras/${id}`);
      load();
    }
  }

  return (
    <>
      <div style={{ display: 'flex', justifyContent: 'space-between', alignItems: 'center', marginBottom: '1.5rem' }}>
        <h1 className="page-title" style={{ margin: 0 }}>Obras</h1>
        <button className="btn btn-primary" onClick={openCreate}>+ Nueva Obra</button>
      </div>

      <div className="card">
        <table>
          <thead>
            <tr>
              <th>Nombre</th>
              <th>Ubicación</th>
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
                <td>{o.ubicacion}</td>
                <td>
                  <span className={`badge ${o.estado === 'En progreso' ? 'badge-warning' : o.estado === 'Completada' ? 'badge-success' : 'badge-info'}`}>
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
              <tr><td colSpan="6" style={{ textAlign: 'center', color: '#aaa' }}>No hay obras registradas</td></tr>
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
                <label>Ubicación</label>
                <input required value={form.ubicacion} onChange={e => setForm({ ...form, ubicacion: e.target.value })} />
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