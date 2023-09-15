import { mount } from '@vue/test-utils';
import { nextTick } from 'vue';
import Learning from '../../../views/Learning.vue'; // Adjust the path to your file structure

global.useRouter = jest.fn()


jest.mock('vue-router', () => ({
  useRouter: jest.fn().mockReturnValue({
    push: jest.fn()
  }),
}));

const mockRouter = {
  push: jest.fn(),
};


describe('Learning.vue', () => {
  let wrapper;

  beforeEach(() => {
    wrapper = mount(Learning, {
      global: {
        mocks: {
          $router: mockRouter,
        },
      },
    });
  });


  it('should group courses correctly', async () => {
    const mockData = [
      { id: 1, title: 'Course 1', description: 'Description 1', category: 'Category 1' },
      { id: 2, title: 'Course 2', description: 'Description 2', category: 'Category 1' },
      { id: 3, title: 'Course 3', description: 'Description 3', category: 'Category 2' },
    ];
    
    wrapper.vm.courses = mockData;
  
    const groupedCourses = wrapper.vm.groupedCourses;
    expect(groupedCourses['Category 1'].length).toBe(2);
    expect(groupedCourses['Category 2'].length).toBe(1);
  });

  it('should handle ipcRenderer correctly when exists', async () => {
    await nextTick();
    global.window = Object.create(window);
    Object.defineProperty(window, 'ipcRenderer', {
      value: { send: jest.fn() },
      writable: true
    });
    
    await wrapper.vm.checkCourse("The Quantum Computer");
    
    expect(window.ipcRenderer.send).toHaveBeenCalledWith('open-unity-window', "The Quantum Computer");
  });

  it('should handle ipcRenderer correctly when does not exist', async () => {
    await nextTick();
    global.window = Object.create(window);
    Object.defineProperty(window, 'ipcRenderer', {
      value: null,
      writable: true
    });
    
    console.log = jest.fn();
    
    await wrapper.vm.checkCourse("The Quantum Computer");
    
    expect(console.log).toHaveBeenCalledWith('ipcRenderer does not exist');
  });

  
  it('should render properly on small screens', async () => {
    global.innerWidth = 500;
    global.innerHeight = 800;
    global.dispatchEvent(new Event('resize'));

    wrapper = mount(Learning, {
      propsData: {
        initialCourses: [],  // Pass initial data if necessary
      },
    });

    expect(wrapper.find('.container').exists()).toBe(true);
  });

  it('should render properly on medium screens', async () => {
    global.innerWidth = 800;
    global.innerHeight = 600;
    global.dispatchEvent(new Event('resize'));

    wrapper = mount(Learning, {
      propsData: {
        initialCourses: [], 
      },
    });

    expect(wrapper.find('.container').exists()).toBe(true);
  });

  it('should render properly on large screens', async () => {
    global.innerWidth = 1200;
    global.innerHeight = 800;
    global.dispatchEvent(new Event('resize'));

    wrapper = mount(Learning, {
      propsData: {
        initialCourses: [],  
      },
    });

    expect(wrapper.find('.container').exists()).toBe(true);
  });
});

