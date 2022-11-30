import './App.css';
import { Table } from './features/table/table';
import { SignalRService } from './features/signalr/signalr-service';
import { Game } from './features/skull/game';

function App() {
  const signalRService = new SignalRService();
  return (
    <div className="App" >
      <header className="App-header">
        <Table  signalrService={signalRService}/>
        <Game signalrService={signalRService}/>
      </header>
    </div>
  );
}

export default App;
