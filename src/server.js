import express from 'express';
import path from 'path';
import fs from 'fs';
import dotenv from 'dotenv';

dotenv.config({ path: path.join(__dirname, '..', '/.env') });


// let server;

const app = express();

app.use(express.static(path.join(__dirname, '..')));
const gamePort = process.env.PORT || 8081;

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

// Start the server
try {
  const server = app.listen(gamePort, () => {
    console.log(`Server listening on port ${gamePort}`);
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
