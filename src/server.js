import express from 'express';
import path from 'path';
import fs from 'fs';
import dotenv from 'dotenv';
import { spawn } from 'child_process';
import cors from 'cors';
import { createProxyMiddleware } from 'http-proxy-middleware';
import multer from 'multer';


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

// Set up Multer to save uploaded files to the 'public' directory
const storage = multer.diskStorage({
  destination: function (req, file, cb) {
    cb(null, path.join(__dirname, '../public'));
  },
  filename: function (req, file, cb) {
    cb(null, file.originalname);
  }
});

const upload = multer({ storage: storage });

app.post('/upload', upload.array('demo[]'), (req, res) => {
  try {
    const course = JSON.parse(req.body.course);
    const files = req.files;

    if (!files || files.length === 0) {
      return res.status(400).send('No files were uploaded.');
    }

    // Save files to the public directory
    files.forEach((file) => {
      fs.writeFile(path.join(__dirname, '../public', file.originalname), file.buffer, (err) => {
        if (err) {
          return res.status(500).send(err);
        }
      });
    });

    // Update the courses.json file with the new course data
    fs.readFile(path.resolve(__dirname, '../src/data/courses.json'), 'utf8', (err, data) => {
      if (err) {
        res.status(500).send('Server error');
        return;
      }

      // Parse the existing courses and add the new course
      const courses = JSON.parse(data);
      courses.push(course);

      // Write the updated courses back to the JSON file
      fs.writeFile(path.resolve(__dirname, '../src/data/courses.json'), JSON.stringify(courses, null, 2), (err) => {
        if (err) {
          res.status(500).send('Server error');
          return;
        }

        res.send('File uploaded and course added successfully!');
      });
    });
  } catch (error) {
    res.status(500).send('Server error: ' + error.message);
  }
});


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
    methods: 'GET,HEAD,PUT,PATCH,POST,DELETE',
    credentials: true,
    optionsSuccessStatus: 204,
}));

app.use(express.json());

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

app.post('/add-course', (req, res) => {
  const newCourse = req.body;

  // Read the existing courses from the JSON file
  fs.readFile(path.resolve(__dirname, '../src/data/courses.json'), 'utf8', (err, data) => {
    if (err) {
      res.status(500).send('Server error');
      return;
    }

    // Parse the existing courses and add the new course
    const courses = JSON.parse(data);
    courses.push(newCourse);

    // Write the updated courses back to the JSON file
    fs.writeFile(path.resolve(__dirname, '../src/data/courses.json'), JSON.stringify(courses, null, 2), (err) => {
      if (err) {
        res.status(500).send('Server error');
        return;
      }

      res.send('Course added successfully');
    });
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
