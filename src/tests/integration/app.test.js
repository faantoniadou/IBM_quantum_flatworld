import request from 'supertest';
// import { startServer, stopServer } from '../../server.js';
import { stopServer } from '../../server.js';
import fs from 'fs';
import path from 'path';




describe('Integration Tests', () => {
  beforeAll(async () => {
    // startServer();
  });

  afterAll(() => {
    stopServer();
  });

  it('should upload files successfully', async () => {
    const response = await request(app)
      .post('/upload')
      .attach('demo[]', fs.readFileSync(path.join(__dirname, 'path/to/your/test/file.txt')), 'testfile.txt');

    expect(response.status).toBe(200);
    expect(response.text).toBe('File uploaded successfully!');
  });

  it('should get the list of courses', async () => {
    const response = await request(app).get('/courses');

    expect(response.status).toBe(200);
    expect(response.text).toContain('courseTitle1'); // Adjust with actual course title in your courses.json
  });

  it('should add a new course successfully', async () => {
    const newCourse = { title: 'New Course', description: 'New course description' };
    const response = await request(app)
      .post('/add-course')
      .send(newCourse)
      .set('Accept', 'application/json');

    expect(response.status).toBe(200);
    expect(response.text).toBe('Course added successfully');
  });

  it('should get a specific course by title', async () => {
    const courseTitle = 'The Quantum Computer'; // Adjust with an actual course title in your database
    const response = await request(app).get(`/${encodeURIComponent(courseTitle)}`);

    expect(response.status).toBe(200);
    expect(response.text).toContain('Expected content here'); // Replace with actual content you expect to find in the response
  });
});
