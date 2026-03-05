import resolve from '@rollup/plugin-node-resolve';
import commonjs from '@rollup/plugin-commonjs';
import typescript from '@rollup/plugin-typescript';
import dts from 'rollup-plugin-dts';
import postcss from 'rollup-plugin-postcss';
import peerDepsExternal from 'rollup-plugin-peer-deps-external';
import terser from '@rollup/plugin-terser';
import { readFileSync } from 'fs';

const packageJson = JSON.parse(readFileSync('./package.json'));

/**
 * Enterprise-grade Rollup Configuration
 * 
 * Objectives:
 * 1. Bundle React components into ESM (for tree-shaking) and CJS (for legacy support).
 * 2. Extract and process Tailwind CSS into a single distributable file.
 * 3. Generate comprehensive TypeScript definition files (.d.ts).
 * 4. Externalize peer dependencies (React, Radix UI) to prevent bundle duplication.
 * 5. Minimize output for performance (LCP requirement).
 */
export default [
  {
    input: 'src/index.ts',
    output: [
      {
        file: packageJson.main,
        format: 'cjs',
        sourcemap: true,
        name: '@emp/ui-components',
      },
      {
        file: packageJson.module,
        format: 'esm',
        sourcemap: true,
      },
    ],
    plugins: [
      // Prevent bundling peer dependencies (React, ReactDOM, etc.)
      peerDepsExternal(),

      // Resolve external modules from node_modules
      resolve(),

      // Convert CommonJS modules to ES6
      commonjs(),

      // Compile TypeScript with declaration file generation
      typescript({ 
        tsconfig: './tsconfig.build.json',
        exclude: ['**/*.test.tsx', '**/*.test.ts', '**/*.stories.tsx']
      }),

      // Process CSS/Tailwind
      // Extracts to dist/index.css to be imported by consumers
      postcss({
        config: {
          path: './postcss.config.js',
        },
        extensions: ['.css'],
        minimize: true,
        inject: {
          insertAt: 'top',
        },
        extract: 'index.css', // Extract to separate file for caching benefits
        sourceMap: true
      }),

      // Minify the bundle for production performance
      terser(),
    ],
    // Ensure Radix UI primitives and other heavy deps are not bundled if they are peers
    external: ['react', 'react-dom', 'react/jsx-runtime'],
  },
  {
    // Second pass: Bundle Types
    input: 'src/index.ts',
    output: [{ file: 'dist/index.d.ts', format: 'es' }],
    plugins: [
      dts(),
    ],
    // Import styles in .d.ts is invalid, so we treat .css as external
    external: [/\.css$/],
  },
];