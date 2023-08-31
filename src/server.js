import express from 'express';
import path from 'path';

let server;

const app = express();

app.use(express.static(path.join(__dirname, '..', 'public')));

function startServer(courseTitle, callback) {
  // Course URLs for different courses'
  const courseURLs = {
    "The Quantum Computer": "/unity-vr",
    "The Bloch Sphere": "The Bloch Sphere"
    // ... other courses
  };

  const courseURL = courseURLs[courseTitle];

  if (!courseURL) {
    console.error('Course not found:', courseTitle);
    return;
  }

  app.get('/', (req, res) => {
    res.sendFile(path.join(__dirname, '..', 'public', courseURL, 'index.html'));
  });

  server = app.listen(0, 'localhost', () => {
    const port = server.address().port;
    console.log(`Course server listening at http://localhost:${port}`);
    callback(port);
  });

  return server;
}

const stopServer = () => {
  if (server) {
    server.close(() => {
      console.log('Server stopped');
      if (callback) {
        callback();
      }
    });
  }
};

module.exports = { startServer, stopServer };
