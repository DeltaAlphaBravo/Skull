import './App.css';
import { Table } from './features/table/table';
import { SignalRService } from './features/signalr/signalr-service';
import { Game } from './features/skull/game';
import { Routes, Route, Outlet, Link } from "react-router-dom";

function App() {
  const signalRService = new SignalRService();
  return (
    <div className="App" >
      <header className="App-header">
        <Routes>
          <Route path="/" element={<Table  signalrService={signalRService}/>}/>
          <Route path=":table" element={<Game signalrService={signalRService}/>}/>
        </Routes>
        <Outlet/>
      </header>
    </div>
  );
}

export default App;
