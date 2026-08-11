import { useState, useEffect } from 'react';
import api from '../api';

const empty = { descripcion: '', fechaInicio: '', fechaFin: '', porcentajeAvance: 0, obraId: '', responsableId: '' };

export default function Tareas() {
  const [tareas, setTareas] = useState([]);
  const [obras, setObras] = useState([]);
  const [responsables, setResponsables] = useState([]);
  const [modal, setModal] = useState(false);
  const [form, setForm] = useState(empty);
  const [editId, setEditId] = useState(null);

  useEffect(() => {
    load();
    api.get('/Obras').then(r => setObras(r.data.data || []));
    api.get('/Responsables').then(r => setResponsables(r.data.data || []));
  }, []);

  async function load() {
    const r = await api.get('/Tareas');
    setTareas(r.data.data || []);
  }

  function openCreate() { setForm(empty); setEditId(null); setModal(true); }

  function openEdit(t) {
    setForm({
      descripcion: t.descripcion,
      fechaInicio: t.fechaInicio?.split('T')[0] || '',
      fechaFin: t.fechaFin?.split('T')[0] || '',
      porcentajeAvance: t.porcentajeAvance,
      obraId: t.obraId,
      responsableId: t.responsableId || ''
    });
    setEditId(t.id);
    setModal(true);
  }

  async function handleSubmit(e) {
    e.preventDefault();
    const payload = {
      ...form,
      obraId: parseInt(form.obraId),
      porcentajeAvance: parseFloat(form.porcentajeAvance),
      responsableId: form.responsableId ? parseInt(form.responsableId) : null,
      fechaInicio: new Date(form.fechaInicio).toISOString(),
      fechaFin: new Date(form.fechaFin).toISOString()
    };
    if (editId) {
      await api.put(`/Tareas/${editId}`, { id: editId, ...payload });
    } else {
      await api.post('/Tareas', { id: 0, ...payload });
    }
    setModal(false);
    load();
  }

  async function handleDelete(id) {
    if (confirm('¿Eliminar esta tarea?')) {
      await api.delete(`/Tareas/${id}`);
      load();
    }
  }

  function obraNombre(id) {
    return obras.find(o => o.id === id)?.nombre || `Obra #${id}`;
  }

  function responsableNombre(id) {
    return responsables.find(r => r.id === id)?.nombre || '-';
  }

  return (
    <>
      <div style={{ display: 'flex', justifyContent: 'space-between', alignItems: 'center', marginBottom: '1.5rem' }}>
        <h1 className="page-title" style={{ margin: 0 }}>Tareas</h1>
        <button className="btn btn-primary" onClick={openCreate}>+ Nueva Tarea</button>
      </div>

      <div className="card">
        <table>
          <thead>
            <tr>
              <th>Descripción</th>
              <th>Obra</th>
              <th>Responsable</th>
              <th>Avance</th>
              <th>Fecha Inicio</th>
              <th>Fecha Fin</th>
              <th>Acciones</th>
            </tr>
          </thead>
          <tbody>
            {tareas.map(t => (
              <tr key={t.id}>
                <td>{t.descripcion}</td>
                <td>{obraNombre(t.obraId)}</td>
                <td>{responsableNombre(t.responsableId)}</td>
                <td>
                  <div style={{ display: 'flex', alignItems: 'center', gap: '0.5rem' }}>
                    <div style={{ background: '#eee', borderRadius: '10px', height: '8px', width: '80px' }}>
                      <div style={{ background: '#27ae60', height: '8px', borderRadius: '10px', width: `${t.porcentajeAvance}%` }} />
                    </div>
                    <span style={{ fontSize: '0.8rem' }}>{t.porcentajeAvance}%</span>
                  </div>
                </td>
                <td>{new Date(t.fechaInicio).toLocaleDateString()}</td>
                <td>{new Date(t.fechaFin).toLocaleDateString()}</td>
                <td>
                  <button className="btn btn-warning" style={{ marginRight: '0.5rem' }} onClick={() => openEdit(t)}>Editar</button>
                  <button className="btn btn-danger" onClick={() => handleDelete(t.id)}>Eliminar</button>
                </td>
              </tr>
            ))}
            {tareas.length === 0 && (
              <tr><td colSpan="7" style={{ textAlign: 'center', color: '#aaa' }}>No hay tareas registradas</td></tr>
            )}
          </tbody>
        </table>
      </div>

      {modal && (
        <div className="modal-overlay">
          <div className="modal">
            <h2>{editId ? 'Editar Tarea' : 'Nueva Tarea'}</h2>
            <form onSubmit={handleSubmit}>
              <div className="form-group">
                <label>Descripción</label>
                <input required value={form.descripcion} onChange={e => setForm({ ...form, descripcion: e.target.value })} />
              </div>
              <div className="form-group">
                <label>Obra</label>
                <select required value={form.obraId} onChange={e => setForm({ ...form, obraId: e.target.value })}>
                  <option value="">Selecciona una obra</option>
                  {obras.map(o => <option key={o.id} value={o.id}>{o.nombre}</option>)}
                </select>
              </div>
              <div className="form-group">
                <label>Responsable</label>
                <select value={form.responsableId} onChange={e => setForm({ ...form, responsableId: e.target.value })}>
                  <option value="">Sin responsable</option>
                  {responsables.map(r => <option key={r.id} value={r.id}>{r.nombre} - {r.rol}</option>)}
                </select>
              </div>
              <div className="form-group">
                <label>Porcentaje de Avance</label>
                <input type="number" min="0" max="100" required value={form.porcentajeAvance} onChange={e => setForm({ ...form, porcentajeAvance: e.target.value })} />
              </div>
              <div className="form-group">
                <label>Fecha Inicio</label>
                <input type="date" required value={form.fechaInicio} onChange={e => setForm({ ...form, fechaInicio: e.target.value })} />
              </div>
              <div className="form-group">
                <label>Fecha Fin</label>
                <input type="date" required value={form.fechaFin} onChange={e => setForm({ ...form, fechaFin: e.target.value })} />
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