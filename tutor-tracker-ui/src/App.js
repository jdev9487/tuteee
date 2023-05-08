import { Container } from '@mui/material';
import './App.css';
import Customers from './Components/Customers';
import Navbar from './Components/Navbar.js'

function App() {
  return (
    <div className="App">
      <Navbar />
      <Container>
        <Customers />
      </Container>
    </div>
  );
}

export default App;
