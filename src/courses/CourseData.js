export const CourseData = {
  getCourseData() {
    return [
      {
        id: 1,
        title: 'Introduction to Quantum Computing',
        description: 'This course is an introduction to quantum computing for non-physicists. It introduces the basic principles of quantum mechanics. No background in physics is required. The course is aimed at those who have heard about quantum computing and want to understand what it is about, its promise, its limitations and to assess whether it is something they should learn more about.',
        image: 'https://www.edx.org/sites/default/files/course/image/promoted/quantum-computing-what-is-it-and-what-can-it-do-for-you_378x225.jpg',
        level: 'Introductory',
        // author: 'University College London'
      },
      {
        id: 2,
        title: 'The Quantum Computer',
        description: 'This course explores the hardware behind the qubit systems of a quantum computer and how to operate and control them. It also introduces the mathematical formalism of quantum mechanics and its application to quantum algorithms.',
        image: 'https://www.edx.org/sites/default/files/course/image/promoted/quantum-computer_378x225.jpg',
        level: 'Intermediate',
        // author: 'University College London'
      },
    ];
  },

  getCourses() {
    return Promise.resolve(this.getCourseData());
  }

};


