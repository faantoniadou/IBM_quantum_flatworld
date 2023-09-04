import express from 'express';
import path from 'path';
import fs from 'fs';
import dotenv from 'dotenv';
import { spawn } from 'child_process';
import cors from 'cors';
import { createProxyMiddleware } from 'http-proxy-middleware';


dotenv.config({ path: path.join(__dirname, '../', '..', '/.env') });

const app = express();

const gamePort = process.env.PORT || 8081;
const flaskPort = process.env.FLASK_PORT || 3000;
const flaskDir = path.join(__dirname, '..', 'qiskit_backend');

const allowedOrigins = [
  `http://localhost:${gamePort}`,
  `${process.env.BASE_URL}`,
  'http://localhost:${flaskPort}',
];

// Course URLs for different courses
const courseURLs = {
  'The Quantum Computer': 'unity-vr',
  'The Bloch Sphere': 'the-bloch-sphere',
  // ... other courses
};

let flask;

app.use(cors({
  origin: (origin, callback) => { 
    if (!origin) return callback(null, true);
    if (allowedOrigins.indexOf(origin) === -1) {
      const msg = `The CORS policy for this site does not allow access from the specified Origin.`;
      return callback(new Error(msg), false);
    }
    return callback(null, true);
  },
}));


// app.use(cors({origin: `http://localhost:${gamePort}`}));


app.use(express.static(path.join(__dirname, '..')));

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


app.use('/api', createProxyMiddleware({
  target: `http://localhost:${flaskPort}`,  // Flask server URL
  changeOrigin: true,
}));

// Define a route for the courses.json file
app.get('/courses', (req, res) => {
  // read the courses.json file which is in the same directory as this file
  const coursesPath = path.join(__dirname, '../src/data/courses.json');
  fs.readFile(coursesPath, 'utf8', (err, data) => {
    if (err) {
      console.error(`Error reading courses.json: ${err}`);
      res.status(500).send('Internal server error');
      return;
    } else if (!data) {
      console.error('No data in courses.json');
      return;
    }

    res.send(data);
  });
});

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
    modifiedData = modifiedData.replace(/Build/g, `../public/${courseURL}/Build`);
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
