import { useState, useEffect } from 'react';
import api from '../api';

const empty = { nombre: '', rol: '', contacto: '' };

export default function Responsables() {
  const [responsables, setResponsables] = useState([]);
  const [modal, setModal] = useState(false);
  const [form, setForm] = useState(empty);
  const [editId, setEditId] = useState(null);
  const [toast, setToast] = useState(null);

  function showToast(msg, type = 'success') {
    setToast({ msg, type });
    setTimeout(() => setToast(null), 3000);
  }

  useEffect(() => { load(); }, []);

  async function load() {
    const r = await api.get('/Responsables');
    setResponsables(r.data.data || []);
  }

  function openCreate() { setForm(empty); setEditId(null); setModal(true); }

  function openEdit(r) {
    setForm({ nombre: r.nombre, rol: r.rol, contacto: r.contacto });
    setEditId(r.id);
    setModal(true);
  }

  async function handleSubmit(e) {
    e.preventDefault();
    if (editId) {
      await api.put(`/Responsables/${editId}`, { id: editId, ...form });
      showToast('Responsable actualizado correctamente');
    } else {
      await api.post('/Responsables', { id: 0, ...form });
      showToast('Responsable creado correctamente');
    }
    setModal(false);
    load();
  }

  async function handleDelete(id) {
    if (confirm('¿Estás seguro de que deseas eliminar este responsable? Esta acción no se puede deshacer.')) {
      await api.delete(`/Responsables/${id}`);
      showToast('Responsable eliminado', 'error');
      load();
    }
  }

  return (
    <>
      {toast && (
        <div style={{
          position: 'fixed', top: '1rem', right: '1rem', zIndex: 9999,
          background: toast.type === 'error' ? '#e74c3c' : '#27ae60',
          color: 'white', padding: '0.75rem 1.5rem', borderRadius: '8px',
          boxShadow: '0 4px 12px rgba(0,0,0,0.2)', fontSize: '0.9rem', fontWeight: 500
        }}>{toast.msg}</div>
      )}

      <div style={{ display: 'flex', justifyContent: 'space-between', alignItems: 'center', marginBottom: '1.5rem' }}>
        <h1 className="page-title" style={{ margin: 0 }}>Responsables</h1>
        <button className="btn btn-primary" onClick={openCreate}>+ Nuevo Responsable</button>
      </div>

      <div className="card">
        <table>
          <thead>
            <tr>
              <th>Nombre</th>
              <th>Rol</th>
              <th>Contacto</th>
              <th>Acciones</th>
            </tr>
          </thead>
          <tbody>
            {responsables.map(r => (
              <tr key={r.id}>
                <td>{r.nombre}</td>
                <td>{r.rol}</td>
                <td>{r.contacto}</td>
                <td>
                  <button className="btn btn-warning" style={{ marginRight: '0.5rem' }} onClick={() => openEdit(r)}>Editar</button>
                  <button className="btn btn-danger" onClick={() => handleDelete(r.id)}>Eliminar</button>
                </td>
              </tr>
            ))}
            {responsables.length === 0 && (
              <tr><td colSpan="4" style={{ textAlign: 'center', color: '#aaa' }}>No hay responsables registrados</td></tr>
            )}
          </tbody>
        </table>
      </div>

      {modal && (
        <div className="modal-overlay">
          <div className="modal">
            <h2>{editId ? 'Editar Responsable' : 'Nuevo Responsable'}</h2>
            <form onSubmit={handleSubmit}>
              <div className="form-group">
                <label>Nombre</label>
                <input required value={form.nombre} onChange={e => setForm({ ...form, nombre: e.target.value })} />
              </div>
              <div className="form-group">
                <label>Rol</label>
                <input required value={form.rol} onChange={e => setForm({ ...form, rol: e.target.value })} />
              </div>
              <div className="form-group">
                <label>Contacto</label>
                <input value={form.contacto} onChange={e => setForm({ ...form, contacto: e.target.value })} />
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