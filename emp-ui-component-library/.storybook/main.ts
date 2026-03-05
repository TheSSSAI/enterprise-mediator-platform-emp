import type { StorybookConfig } from "@storybook/react-vite";
import path from "path";

/**
 * Storybook Main Configuration
 * 
 * Orchestrates the documentation and visual testing environment.
 * Integrates Vite builder for fast HMR and build times.
 * Includes accessibility addons to enforce REQ-INT-001 (WCAG 2.1 AA).
 */
const config: StorybookConfig = {
  // Pattern to find all stories in the src directory
  stories: ["../src/**/*.mdx", "../src/**/*.stories.@(js|jsx|mjs|ts|tsx)"],
  
  addons: [
    // Essentials includes Controls, Backgrounds, Docs, Viewport, Toolbars, Measure, Outline
    "@storybook/addon-links",
    "@storybook/addon-essentials",
    "@storybook/addon-onboarding",
    "@storybook/addon-interactions",
    
    // CRITICAL: Enforces Accessibility Compliance (REQ-INT-001)
    "@storybook/addon-a11y" 
  ],
  
  framework: {
    name: "@storybook/react-vite",
    options: {},
  },
  
  docs: {
    autodocs: "tag", // Automatically generate docs pages for components with 'autodocs' tag
  },

  // Configure Vite within Storybook to handle path aliases and Tailwind
  async viteFinal(config) {
    if (!config.resolve) {
      config.resolve = {};
    }
    
    // Ensure alias resolution matches tsconfig and vitest config
    config.resolve.alias = {
      ...config.resolve.alias,
      "@": path.resolve(__dirname, "../src"),
    };

    return config;
  },
};

export default config;