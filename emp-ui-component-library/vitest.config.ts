import { defineConfig } from 'vitest/config';
import react from '@vitejs/plugin-react';
import path from 'path';

/**
 * Vitest Configuration for Component Testing
 * 
 * Configures the test runner to handle React components, TypeScript paths,
 * and JSDOM environment simulation.
 */
export default defineConfig({
  plugins: [react()],
  test: {
    // Simulates a browser environment for React components
    environment: 'jsdom',
    
    // Enable global usage of describe/test/expect
    globals: true,
    
    // Setup file for configuring testing libraries (e.g., jest-dom matchers)
    // Assuming a standard setup file structure, though not explicitly requested in file list.
    // If it doesn't exist, this line can be commented out or created.
    // setupFiles: './src/test/setup.ts',

    // Coverage configuration to ensure quality gates
    coverage: {
      provider: 'v8',
      reporter: ['text', 'json', 'html'],
      exclude: [
        'node_modules/',
        'src/test/',
        '**/*.d.ts',
        '**/*.stories.tsx', // Exclude stories from coverage
        '**/*.config.{js,ts}'
      ],
    },

    // Alias resolution to match tsconfig.json paths
    alias: {
      '@': path.resolve(__dirname, './src'),
    },
  },
  resolve: {
    alias: {
      '@': path.resolve(__dirname, './src'),
    },
  },
});