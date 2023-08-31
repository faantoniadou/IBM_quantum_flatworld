import express from 'express';
let server;

const startServer = (callback) => {
  const app = express();

  // Start the server on a dynamically-allocated port
  server = app.listen(0, () => {
    const port = server.address().port;
    console.log(`Server started on port ${port}`);
    
    if (callback) {
      callback(port);
    }
  });
};

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
