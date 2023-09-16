import BackButton from './BackButton.vue'

describe('<BackButton />', () => {
  it('renders', () => {
    // see: https://on.cypress.io/mounting-vue
    cy.mount(BackButton)
  })
})