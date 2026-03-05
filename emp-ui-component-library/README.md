# @emp/ui-components

A reusable, accessible UI component library for the Enterprise Mediator Platform (EMP). Built with React 18, Radix UI primitives, and Tailwind CSS.

## Features

- **Accessible**: Built on Radix UI primitives to ensure WCAG 2.1 AA compliance (REQ-INT-001).
- **Themable**: Full support for Dark Mode and custom theming via CSS variables (US-090).
- **Performance**: Tree-shakeable ESM/CJS exports to support LCP goals (REQ-PERF-002).
- **Consistent**: Enforces the "2040" design aesthetic using Tailwind CSS.

## Installation

```bash
npm install @emp/ui-components
# or
yarn add @emp/ui-components
```

## Usage

1. **Import Styles**: Import the global CSS in your application's root entry point (e.g., `_app.tsx` or `layout.tsx`).

   ```tsx
   import '@emp/ui-components/dist/style.css';
   ```

2. **Tailwind Configuration**: Add the library's content to your `tailwind.config.js` to ensure styles are generated correctly.

   ```js
   module.exports = {
     content: [
       // ... your app paths
       './node_modules/@emp/ui-components/dist/**/*.{js,mjs}'
     ],
     presets: [require('@emp/ui-components/tailwind.config')],
   };
   ```

3. **Use Components**:

   ```tsx
   import { Button } from '@emp/ui-components';

   export default function App() {
     return <Button variant="default">Click Me</Button>;
   }
   ```

## Development

### Setup

```bash
npm install
```

### Storybook

Run Storybook to develop and view components in isolation.

```bash
npm run storybook
```

### Building

Build the library for distribution.

```bash
npm run build
```

### Linting & Formatting

```bash
npm run lint
npm run format
```

## Accessibility

All components are tested against axe-core accessibility rules within Storybook. Ensure any new components maintain this standard.