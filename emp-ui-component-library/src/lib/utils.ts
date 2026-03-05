import { type ClassValue, clsx } from 'clsx';
import { twMerge } from 'tailwind-merge';

/**
 * Merges Tailwind CSS classes with clsx for conditional rendering.
 * This utility ensures that style conflicts are resolved correctly (e.g., 'px-2 px-4' becomes 'px-4').
 *
 * @param inputs - List of class names or conditional class objects
 * @returns Merged class name string
 */
export function cn(...inputs: ClassValue[]): string {
  return twMerge(clsx(inputs));
}

/**
 * Checks if code is running in a browser environment.
 */
export const isBrowser = typeof window !== 'undefined' && typeof window.document !== 'undefined';

/**
 * No-operation function for default prop values.
 */
// eslint-disable-next-line @typescript-eslint/no-empty-function
export const noop = () => {};

/**
 * Generates a random ID if one is not provided.
 * @param id - Optional ID provided by the user
 * @returns A string ID
 */
export function useId(id?: string): string {
  if (id) return id;
  // Simple random ID generation for client-side only fallback
  // In React 18, use the official useId hook in components instead
  return `emp-${Math.random().toString(36).substr(2, 9)}`;
}