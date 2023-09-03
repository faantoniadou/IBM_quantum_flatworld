import express from 'express';
import path from 'path';
import fs from 'fs';
import dotenv from 'dotenv';
import { spawn } from 'child_process';

dotenv.config({ path: path.join(__dirname, '../', '..', '/.env') });

const app = express();

app.use(express.static(path.join(__dirname, '..')));
const gamePort = process.env.PORT || 8081;
const flaskPort = process.env.FLASK_PORT || 5000;
const flaskDir = path.join(__dirname, '..', 'qiskit_backend');

let flask;

const startFlaskServer = () => {
  const flaskEnv = { ...process.env, FLASK_APP: "server.py" };
  flask = spawn('flask', ['run', '--host=0.0.0.0', `--port=${flaskPort}`], { cwd: flaskDir, env: flaskEnv });

  flask.stdout.on('data', (data) => {
    console.log(`Flask stdout: ${data}`);
  });

  flask.stderr.on('data', (data) => {
    console.error(`Flask stderr: ${data}`);
  });

  flask.on('exit', (code) => {
    console.log(`Flask process exited with code ${code}`);
    if (code !== 0) {
      console.log('Restarting Flask server...');
      startFlaskServer();
    }
  });
};


// Course URLs for different courses
const courseURLs = {
  'The Quantum Computer': 'unity-vr',
  'The Bloch Sphere': 'TheBlochSphere',
  // ... other courses
};


app.use(express.static(path.join(__dirname, '..')));

// Define a single route that will handle all courseTitle values
app.get('/:courseTitle', (req, res) => {
  const courseTitle = req.params.courseTitle;
  const courseURL = courseURLs[courseTitle];

  if (!courseURL) {
    console.error('Course not found:', courseTitle);
    res.status(404).send('Course not found');
    return;
  }

  const fullPath = path.join(__dirname, '..', 'public', courseURL, 'index.html');
  console.log("Serving file from: ", fullPath);

  fs.readFile(fullPath, 'utf8', (err, data) => {
    if (err) {
      console.error(`Error reading index.html: ${err}`);
      res.status(500).send('Internal server error');
      return;
    }

    let modifiedData = data.replace(/TemplateData\//g, `../public/${courseURL}/TemplateData/`);
    modifiedData = modifiedData.replace(/\/Build/g, `../public/${courseURL}/Build`);
    modifiedData = modifiedData.replace(/StreamingAssets/g, `../public/${courseURL}/StreamingAssets`);

    res.send(modifiedData);
    // res.send(data);
  });
});

// Gzip handling
app.get('*.js', (req, res, next) => {
  req.url = req.url + '.gz';
  res.set('Content-Encoding', 'gzip');
  res.set('Content-Type', 'text/javascript');
  next();
});




// Start the servers
try {
  const server = app.listen(gamePort, () => {
    console.log(`Server listening on port ${gamePort}`);

  // Start the Flask server 
    startFlaskServer();
  });
} catch (error) {
  console.error('Error starting server:', error);
}


// Stop server function
const stopServer = () => {
  if (server) {
    server.close(() => {
      console.log('Server stopped');
    });
  }
};

export { stopServer };
