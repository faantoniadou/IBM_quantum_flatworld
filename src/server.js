import express from 'express';
let server;

function startServer(courseTitle, callback) {
  // Course URLs for different courses'
  const courseURLs = {
    "The Quantum Computer": "/path/to/quantum-computer-course",
    "QiSkit Schematics": "/path/to/qiskit-schematics-course",
    "The Bloch Sphere": "/path/to/qiskit-schematics-course"
    // ... other courses
  };

  const courseURL = courseURLs[courseTitle];

  if (!courseURL) {
    console.error('Course not found for course:', courseTitle);
    return;
  }

  app.get('/', (req, res) => {
    res.sendFile(path.join(__dirname, courseURL));
  });

  const server = app.listen(0, 'localhost', () => {
    const port = server.address().port;
    console.log(`Course server listening at http://localhost:${port}`);
    callback(port);
  });

  return server;
}

const stopServer = (callback) => {
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
