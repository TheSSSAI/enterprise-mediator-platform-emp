/**
 * Enterprise UI Component Library
 *
 * This file serves as the public API surface for the @emp/ui-components package.
 * It aggregates and exports all reusable UI primitives, hooks, and utilities
 * enabling consumers to import specific functionalities via tree-shakeable named exports.
 */

// -----------------------------------------------------------------------------
// Atoms (Level 1)
// Fundamental UI building blocks.
// -----------------------------------------------------------------------------
export * from './components/Atoms/Badge/Badge';
export * from './components/Atoms/Button/Button';
export * from './components/Atoms/Input/Input';
export * from './components/Atoms/Label/Label';

// -----------------------------------------------------------------------------
// Molecules (Level 2)
// Composite components built from atoms and Radix UI primitives.
// -----------------------------------------------------------------------------
export * from './components/Molecules/Avatar/Avatar';
export * from './components/Molecules/Dialog/Dialog';
export * from './components/Molecules/Select/Select';
export * from './components/Molecules/Toast/Toast';

// -----------------------------------------------------------------------------
// Hooks (Level 0)
// Reusable logic for state management and browser interaction.
// -----------------------------------------------------------------------------
export * from './hooks/useControllableState';
export * from './hooks/useDisclosure';
export * from './hooks/useMediaQuery';

// -----------------------------------------------------------------------------
// Utilities & Constants (Level 0)
// Helper functions and shared constant values.
// -----------------------------------------------------------------------------
export * from './lib/utils';
export * from './lib/constants';

// -----------------------------------------------------------------------------
// Design Tokens (Level 0)
// Exported design system tokens for programmatic usage.
// -----------------------------------------------------------------------------
export * from './styles/tokens';