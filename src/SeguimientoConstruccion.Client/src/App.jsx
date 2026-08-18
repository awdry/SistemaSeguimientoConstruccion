import { BrowserRouter, Routes, Route, Navigate } from 'react-router-dom';
import Login from './pages/Login';
import Dashboard from './pages/Dashboard';
import Obras from './pages/Obras';
import Tareas from './pages/Tareas';
import Responsables from './pages/Responsables';
import Materiales from './pages/Materiales';
import Navbar from './components/Navbar';
import './App.css';


function PrivateRoute({ children }) {
  const isLoggedIn = localStorage.getItem('loggedIn') === 'true';
  return isLoggedIn ? children : <Navigate to="/login" />;
}

function Layout({ children }) {
  return (
    <>
      <Navbar />
      <div className="container">{children}</div>
    </>
  );
}

export default function App() {
  return (
    <BrowserRouter>
      <Routes>
        <Route path="/login" element={<Login />} />
        <Route path="/" element={
          <PrivateRoute>
            <Layout><Dashboard /></Layout>
          </PrivateRoute>
        } />
        <Route path="/obras" element={
          <PrivateRoute>
            <Layout><Obras /></Layout>
          </PrivateRoute>
        } />
        <Route path="/tareas" element={
          <PrivateRoute>
            <Layout><Tareas /></Layout>
          </PrivateRoute>
        } />
        
        <Route path="/responsables" element={
        <PrivateRoute>
        <Layout><Responsables /></Layout>
        </PrivateRoute>
        } />

        <Route path="/materiales" element={
        <PrivateRoute>
        <Layout><Materiales /></Layout>
        </PrivateRoute>
        } />
        
      </Routes>
    </BrowserRouter>
  );
}