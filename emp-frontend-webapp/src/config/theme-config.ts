/**
 * Theme Configuration
 * dependency level: 0
 * Defines the design system tokens for the 2040 aesthetic
 */

export const themeConfig = {
  name: 'Enterprise 2040',
  description: 'Minimalist, high-contrast enterprise theme',
  layout: {
    sidebarWidth: '280px',
    headerHeight: '64px',
    containerMaxWidth: '1440px',
  },
  colors: {
    light: {
      background: '#FFFFFF',
      foreground: '#0F172A', // Slate 900
      primary: '#0F172A',
      primaryForeground: '#F8FAFC', // Slate 50
      secondary: '#F1F5F9', // Slate 100
      secondaryForeground: '#0F172A',
      accent: '#E2E8F0', // Slate 200
      accentForeground: '#0F172A',
      muted: '#F8FAFC',
      mutedForeground: '#64748B', // Slate 500
      border: '#E2E8F0',
      input: '#E2E8F0',
      ring: '#0F172A',
    },
    dark: {
      background: '#020617', // Slate 950
      foreground: '#F8FAFC',
      primary: '#F8FAFC',
      primaryForeground: '#0F172A',
      secondary: '#1E293B', // Slate 800
      secondaryForeground: '#F8FAFC',
      accent: '#1E293B',
      accentForeground: '#F8FAFC',
      muted: '#0F172A',
      mutedForeground: '#94A3B8', // Slate 400
      border: '#1E293B',
      input: '#1E293B',
      ring: '#D1D5DB', // Gray 300
    },
  },
  fonts: {
    sans: 'Inter, system-ui, sans-serif',
    mono: 'JetBrains Mono, monospace',
  },
  borderRadius: {
    DEFAULT: '0.5rem',
    sm: '0.25rem',
    lg: '0.75rem',
    full: '9999px',
  },
} as const;

export type ThemeConfig = typeof themeConfig;