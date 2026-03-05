/**
 * Global constant values used across the UI library.
 */

// Keyboard Event Keys
export const KEYS = {
  ENTER: 'Enter',
  SPACE: ' ',
  ESCAPE: 'Escape',
  TAB: 'Tab',
  ARROW_UP: 'ArrowUp',
  ARROW_DOWN: 'ArrowDown',
  ARROW_LEFT: 'ArrowLeft',
  ARROW_RIGHT: 'ArrowRight',
  HOME: 'Home',
  END: 'End',
} as const;

// Viewport Breakpoints (Must match tailwind.config.js default theme)
export const BREAKPOINTS = {
  SM: 640,
  MD: 768,
  LG: 1024,
  XL: 1280,
  '2XL': 1536,
} as const;

// Z-Index Layers
export const Z_INDEX = {
  HIDE: -1,
  AUTO: 'auto',
  BASE: 0,
  DOCK: 10,
  DROPDOWN: 1000,
  STICKY: 1100,
  BANNER: 1200,
  OVERLAY: 1300,
  MODAL: 1400,
  POPOVER: 1500,
  SKIPLINK: 1600,
  TOAST: 1700,
  TOOLTIP: 1800,
} as const;

// Animation Durations (ms)
export const DURATION = {
  FAST: 100,
  NORMAL: 200,
  SLOW: 300,
  VERY_SLOW: 500,
} as const;