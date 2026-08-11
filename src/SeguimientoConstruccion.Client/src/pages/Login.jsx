import { useState } from 'react';
import { useNavigate } from 'react-router-dom';

export default function Login() {
  const [usuario, setUsuario] = useState('');
  const [password, setPassword] = useState('');
  const [error, setError] = useState('');
  const navigate = useNavigate();

  function handleLogin(e) {
    e.preventDefault();
    if (usuario === 'admin' && password === 'admin123') {
      localStorage.setItem('loggedIn', 'true');
      navigate('/');
    } else {
      setError('Usuario o contraseña incorrectos');
    }
  }

  return (
    <div className="login-wrapper">
      <div className="login-card">
        <h2>🏗️ SistemaConstrucción</h2>
        <p>Sistema de Seguimiento de Proyectos</p>
        <form onSubmit={handleLogin}>
          <div className="form-group">
            <label>Usuario</label>
            <input
              type="text"
              value={usuario}
              onChange={e => setUsuario(e.target.value)}
            />
          </div>
          <div className="form-group">
            <label>Contraseña</label>
            <input
              type="password"
              value={password}
              onChange={e => setPassword(e.target.value)}
            />
          </div>
          {error && <p style={{color:'red', fontSize:'0.85rem', marginBottom:'0.5rem'}}>{error}</p>}
          <button className="login-btn" type="submit">Iniciar sesión</button>
        </form>
      </div>
    </div>
  );
}