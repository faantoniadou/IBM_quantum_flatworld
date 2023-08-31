// This file is  used to start and stop the unity server

import express from 'express';
import { spawn } from 'child_process';
// import cors from 'cors';
// import net from 'net';
 
const app = express();
const port = 8081;

let unityServerProcess;

app.post('/start-server', (req, res) => {
  // Start server if not already running
  if (!unityServerProcess) {
    unityServerProcess = spawn('command-to-start-unity-server', ['arguments']);
    res.send('Server started');
  } else {
    res.send('Server already running');
  }
});

app.post('/stop-server', (req, res) => {
  if (unityServerProcess) {
    unityServerProcess.kill();
    unityServerProcess = null;
    res.send('Game server stopped');
  } else {
    res.send('Game server not running');
  }
});

app.listen(port, () => {
  console.log(`App listening at http://localhost:${port}`);
});

