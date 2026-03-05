import type { Preview } from "@storybook/react";
// Import global styles to ensure Tailwind CSS is applied to all stories
import "../src/styles/globals.css";

/**
 * Storybook Preview Configuration
 * 
 * Sets global parameters for all stories, including:
 * - Action logging patterns
 * - Control matchers for automatic UI generation
 * - Accessibility (A11y) configuration rules
 */
const preview: Preview = {
  parameters: {
    // Regex for actions to automatically log
    actions: { argTypesRegex: "^on[A-Z].*" },
    
    // Regex for controls to infer input types (Color pickers, Date pickers)
    controls: {
      matchers: {
        color: /(background|color)$/i,
        date: /Date$/i,
      },
    },

    // Accessibility Addon Configuration
    // Enforces WCAG 2.1 AA standards as per REQ-INT-001
    a11y: {
      config: {
        rules: [
          {
            id: 'color-contrast',
            enabled: true, // Strict enforcement of contrast ratios
          },
        ],
      },
    },

    // Layout configuration
    layout: 'centered',
    
    // Background configuration for checking contrast in light/dark modes
    backgrounds: {
      default: 'light',
      values: [
        {
          name: 'light',
          value: '#ffffff',
        },
        {
          name: 'dark',
          value: '#0f172a', // Matches Slate-900 (typical dark mode bg)
        },
      ],
    },
  },
};

export default preview;