import Learning from './Learning.vue'

describe('<Learning />', () => {
  it('renders', () => {
    // see: https://on.cypress.io/mounting-vue
    cy.mount(Learning)
  })
})