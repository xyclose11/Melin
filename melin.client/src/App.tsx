import { useEffect } from "react";
import "./App.css";

function App() {
    useEffect(() => {}, []);

    return (
        <div>
            <h1 id="tabelLabel">Weather forecast</h1>
            <p>This component demonstrates fetching data from the server.</p>
        </div>
    );
}

export default App;
