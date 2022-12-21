import './App.css';
import { Table } from './features/table/table';
import { SignalRService } from './features/signalr/signalr-service';
import { Game } from './features/skull/game';
import { Routes, Route, Outlet } from "react-router-dom";

function App() {
  const signalRService = new SignalRService();
  
  return (
    <div className="App" >
        <Routes>
          <Route path="/" element={<Table />}/>
          <Route path=":table" element={<Game signalrService={signalRService}/>}/>
        </Routes>
        <Outlet/>
    </div>
  );
}

export default App;
