import './App.css';
import { Routes, Route } from 'react-router-dom';
import NavigationBar from './components/navigation/navigation-bar/navigation-bar.component';
import Home from './components/home/home.component';

function App() {
  return (
    <Routes>
      <Route path='/' element={<NavigationBar />}>
        <Route index element={<Home />}/>
      </Route>
    </Routes>
  );
}

export default App;
