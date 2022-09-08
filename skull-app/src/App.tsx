import './App.css';
import { Table } from './features/table/table';
import { SignalRService } from './features/signalr/signalr-service';

function App() {
  return (
    <div className="App">
      <header className="App-header">
        <Table signalrService={new SignalRService()}/>
      </header>
    </div>
  );
}

export default App;
