import { useState, useEffect } from 'react';
import * as XLSX from 'xlsx';
import api from '../api';

const empty = { nombre: '', unidadMedida: '', costoUnitario: 0, cantidadUsada: 0, tareaId: '' };

export default function Materiales() {
  const [materiales, setMateriales] = useState([]);
  const [tareas, setTareas] = useState([]);
  const [modal, setModal] = useState(false);
  const [form, setForm] = useState(empty);
  const [editId, setEditId] = useState(null);
  const [toast, setToast] = useState(null);

  function showToast(msg, type = 'success') {
    setToast({ msg, type });
    setTimeout(() => setToast(null), 3000);
  }

  useEffect(() => {
    load();
    api.get('/Tareas').then(r => setTareas(r.data.data || []));
  }, []);

  async function load() {
    const r = await api.get('/Materiales');
    setMateriales(r.data.data || []);
  }

  function openCreate() { setForm(empty); setEditId(null); setModal(true); }

  function openEdit(m) {
    setForm({
      nombre: m.nombre,
      unidadMedida: m.unidadMedida,
      costoUnitario: m.costoUnitario,
      cantidadUsada: m.cantidadUsada,
      tareaId: m.tareaId
    });
    setEditId(m.id);
    setModal(true);
  }

  async function handleSubmit(e) {
    e.preventDefault();
    const payload = {
      ...form,
      tareaId: parseInt(form.tareaId),
      costoUnitario: parseFloat(form.costoUnitario),
      cantidadUsada: parseFloat(form.cantidadUsada),
      costoTotal: parseFloat(form.costoUnitario) * parseFloat(form.cantidadUsada)
    };
    if (editId) {
      await api.put(`/Materiales/${editId}`, { id: editId, ...payload });
      showToast('Material actualizado correctamente');
    } else {
      await api.post('/Materiales', { id: 0, ...payload });
      showToast('Material creado correctamente');
    }
    setModal(false);
    load();
  }

  async function handleDelete(id) {
    if (confirm('¿Estás seguro de que deseas eliminar este material? Esta acción no se puede deshacer.')) {
      await api.delete(`/Materiales/${id}`);
      showToast('Material eliminado', 'error');
      load();
    }
  }

  function tareaNombre(id) {
    return tareas.find(t => t.id === id)?.descripcion || `Tarea #${id}`;
  }

  function exportarExcel() {
    const data = materiales.map(m => ({
      'Nombre': m.nombre,
      'Unidad': m.unidadMedida,
      'Costo Unitario': m.costoUnitario,
      'Cantidad': m.cantidadUsada,
      'Costo Total': m.costoTotal,
      'Tarea': tareaNombre(m.tareaId)
    }));
    const ws = XLSX.utils.json_to_sheet(data);
    const wb = XLSX.utils.book_new();
    XLSX.utils.book_append_sheet(wb, ws, 'Materiales');
    XLSX.writeFile(wb, 'Materiales_SistemaConstruccion.xlsx');
    showToast('Archivo Excel exportado correctamente');
  }

  const costoTotal = materiales.reduce((sum, m) => sum + (m.costoTotal || 0), 0);

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
        <h1 className="page-title" style={{ margin: 0 }}>Materiales</h1>
        <div style={{ display: 'flex', gap: '0.5rem' }}>
          <button className="btn btn-success" onClick={exportarExcel}>⬇ Exportar Excel</button>
          <button className="btn btn-primary" onClick={openCreate}>+ Nuevo Material</button>
        </div>
      </div>

      <div style={{ background: 'white', borderRadius: '10px', padding: '1rem 1.5rem', marginBottom: '1rem', boxShadow: '0 2px 8px rgba(0,0,0,0.08)', display: 'flex', justifyContent: 'space-between', alignItems: 'center' }}>
        <span style={{ color: '#060606', fontSize: '0.95rem' }}> Costo total en materiales</span>
        <span style={{ fontWeight: 700, fontSize: '1.3rem', color: '#1a1a2e' }}>
          RD$ {costoTotal.toLocaleString('es-DO', { minimumFractionDigits: 2 })}
        </span>
      </div>

      <div className="card">
        <table>
          <thead>
            <tr>
              <th>Nombre</th>
              <th>Unidad</th>
              <th>Costo Unit.</th>
              <th>Cantidad</th>
              <th>Costo Total</th>
              <th>Tarea</th>
              <th>Acciones</th>
            </tr>
          </thead>
          <tbody>
            {materiales.map(m => (
              <tr key={m.id}>
                <td>{m.nombre}</td>
                <td>{m.unidadMedida}</td>
                <td>RD$ {parseFloat(m.costoUnitario).toLocaleString('es-DO', { minimumFractionDigits: 2 })}</td>
                <td>{m.cantidadUsada}</td>
                <td style={{ fontWeight: 600, color: '#27ae60' }}>
                  RD$ {parseFloat(m.costoTotal).toLocaleString('es-DO', { minimumFractionDigits: 2 })}
                </td>
                <td>{tareaNombre(m.tareaId)}</td>
                <td>
                  <button className="btn btn-warning" style={{ marginRight: '0.5rem' }} onClick={() => openEdit(m)}>Editar</button>
                  <button className="btn btn-danger" onClick={() => handleDelete(m.id)}>Eliminar</button>
                </td>
              </tr>
            ))}
            {materiales.length === 0 && (
              <tr><td colSpan="7" style={{ textAlign: 'center', color: '#aaa' }}>No hay materiales registrados</td></tr>
            )}
          </tbody>
        </table>
      </div>

      {modal && (
        <div className="modal-overlay">
          <div className="modal">
            <h2>{editId ? 'Editar Material' : 'Nuevo Material'}</h2>
            <form onSubmit={handleSubmit}>
              <div className="form-group">
                <label>Nombre</label>
                <input required value={form.nombre} onChange={e => setForm({ ...form, nombre: e.target.value })} placeholder="Cemento Portland, Varillas, etc." />
              </div>
              <div className="form-group">
                <label>Unidad de Medida</label>
                <select value={form.unidadMedida} onChange={e => setForm({ ...form, unidadMedida: e.target.value })}>
                  <option value="">Selecciona unidad</option>
                  <option>Sacos</option>
                  <option>Metros</option>
                  <option>Metros cuadrados</option>
                  <option>Litros</option>
                  <option>Galones</option>
                  <option>Unidades</option>
                  <option>Toneladas</option>
                  <option>Kilogramos</option>
                </select>
              </div>
              <div className="form-group">
                <label>Costo Unitario (RD$)</label>
                <input type="number" min="0" step="0.01" required value={form.costoUnitario} onChange={e => setForm({ ...form, costoUnitario: e.target.value })} />
              </div>
              <div className="form-group">
                <label>Cantidad Usada</label>
                <input type="number" min="0" step="0.01" required value={form.cantidadUsada} onChange={e => setForm({ ...form, cantidadUsada: e.target.value })} />
              </div>
              <div style={{ background: '#04040429', borderRadius: '6px', padding: '0.6rem 0.8rem', marginBottom: '1rem', fontSize: '0.9rem', color: '#024611' }}>
                 Costo total estimado: <strong>RD$ {(parseFloat(form.costoUnitario || 0) * parseFloat(form.cantidadUsada || 0)).toLocaleString('es-DO', { minimumFractionDigits: 2 })}</strong>
              </div>
              <div className="form-group">
                <label>Tarea asociada</label>
                <select required value={form.tareaId} onChange={e => setForm({ ...form, tareaId: e.target.value })}>
                  <option value="">Selecciona una tarea</option>
                  {tareas.map(t => <option key={t.id} value={t.id}>{t.descripcion}</option>)}
                </select>
              </div>
              <div className="modal-actions">
                <button type="button" className="btn btn-secondary" onClick={() => setModal(false)}>Cancelar</button>
                <button type="submit" className="btn btn-primary">{editId ? 'Guardar cambios' : 'Crear'}</button>
              </div>
            </form>
          </div>
        </div>
      )}
    </>
  );
}