import { createApp } from 'vue';
import Learning from '../../src/views/Learning.vue'; // Adjust path as necessary

describe('Learning.cy.js', () => {
  it('playground', () => {
    cy.mount(Learning)
  })
})