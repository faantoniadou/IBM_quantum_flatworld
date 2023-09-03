import express from 'express';
import path from 'path';
import fs from 'fs';


let server;

const app = express();

app.use(express.static(path.join(__dirname, '..')));


function startServer(courseTitle, callback) {
  // Course URLs for different courses'
  const courseURLs = {
    'The Quantum Computer': 'unity-vr',
    'The Bloch Sphere': 'TheBlochSphere',
    // ... other courses
  };

  const courseURL = courseURLs[courseTitle];

  if (!courseURL) {
    console.error('Course not found:', courseTitle);
    return;
  }

  app.get('/', (req, res) => {
    const fullPath = path.join(__dirname, '..', '/public', '/', courseURL, '/index.html');
    console.log("Serving file from: ", fullPath);

    // Read the HTML file
    fs.readFile(fullPath, 'utf8', function (err, data) {
      if (err) {
        console.error(`Error reading index.html: ${err}`);
        res.status(500).send('Internal server error');
        return;
      }

      // Replace placeholders in HTML with actual paths based on courseTitle
      let modifiedData = data.replace(/TemplateData\//g, `../public/${courseURL}/TemplateData/`);
      modifiedData = modifiedData.replace(/\/Build/g, `../public/${courseURL}/Build`);
      modifiedData = modifiedData.replace(/StreamingAssets/g, `../public/${courseURL}/StreamingAssets`);
    
      // Send modified HTML
      res.send(modifiedData);
      // res.sendFile(fullPath);
    });
  });

  app.use((req, res, next) => {
    console.log("HTTP Request: ", req.method, req.url);
    next();
  });

  server = app.listen(0, 'localhost', () => {
    const port = server.address().port;
    console.log(`Course server listening at port ${port}`);
    callback(port);
  });

  return server;
}

app.get('*.js', function (req, res, next) {
  req.url = req.url + '.gz';
  res.set('Content-Encoding', 'gzip');
  res.set('Content-Type', 'text/javascript');
  next();
});

const stopServer = () => {
  if (server) {
    server.close(() => {
      console.log('Server stopped');
    });
  }
};

export { startServer, stopServer };

