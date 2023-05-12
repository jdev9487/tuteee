import { Container } from '@mui/material';
import './App.css';
import Customers from './Components/Customers';
import { BrowserRouter, Routes, Route } from "react-router-dom";
import Navbar from './Components/Navbar.js'
import Student from './Components/Student';

function App() {
  return (
    <div className="App">
      <Navbar />
      <Container>
        <BrowserRouter>
          <Routes>
            <Route path="/" element={<Customers />} />
            <Route path="/customers" element={<Customers />} />
            <Route path="/student/:id" element={<Student />} />
          </Routes>
        </BrowserRouter>
      </Container>
    </div>
  );
}

export default App;
