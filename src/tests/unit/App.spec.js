import { shallowMount } from '@vue/test-utils';
import App from '@/App.vue';
import MenubarComponent from '@/components/MenubarComponent.vue';

describe('App.vue', () => {
  let wrapper;

  beforeEach(() => {
    wrapper = shallowMount(App, {
      global: {
        stubs: {
          RouterView: true,
          MenubarComponent: true
        }
      }
    });
  });

  it('should be named "App"', () => {
    expect(wrapper.vm.$options.name).toBe('App');
  });

  it('should render MenubarComponent', () => {
    expect(wrapper.findComponent(MenubarComponent).exists()).toBe(true);
  });

  it('should render router-view', () => {
    expect(wrapper.findComponent({ name: 'RouterView' }).exists()).toBe(true);
  });
});
