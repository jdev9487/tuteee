import './App.css';
import CustomerCard from './Components/CustomerCard';
import Navbar from './Components/Navbar.js'

function App() {
  const students = ['Lisa', 'John']
  return (
    <div className="App">
      <Navbar />
      <CustomerCard name='Rob Thomas' phone='07829 322 213' email='rob.thomas@pretendmail.com' students={students} />
    </div>
  );
}

export default App;
