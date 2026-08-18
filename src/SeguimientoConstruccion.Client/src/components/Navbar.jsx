import { Link, useNavigate } from 'react-router-dom';

export default function Navbar() {
  const navigate = useNavigate();

  function logout() {
    localStorage.removeItem('loggedIn');
    navigate('/login');
  }

  return (
    <div className="navbar">
      <h1>🏗️ SistemaConstrucción</h1>
      <nav>
        <Link to="/">Dashboard</Link>
        <Link to="/obras">Obras</Link>
        <Link to="/tareas">Tareas</Link>
        <Link to="/responsables">Responsables</Link>
        <Link to="/materiales">Materiales</Link>
      </nav>
      <button className="logout-btn" onClick={logout}>Cerrar sesión</button>
    </div>
  );
}