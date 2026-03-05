import * as React from 'react';

/**
 * A hook to detect if the current viewport matches a CSS media query.
 * Safe for server-side rendering (SSR) by defaulting to false or a provided default state.
 *
 * @param query - The media query string to match (e.g., '(min-width: 768px)')
 * @param defaultState - Optional default value for SSR (default: false)
 * @returns boolean indicating if the query matches
 */
export function useMediaQuery(query: string, defaultState = false): boolean {
  // Use generic state to store the match status
  const [matches, setMatches] = React.useState(defaultState);

  React.useEffect(() => {
    // Ensure we are in a browser environment
    if (typeof window === 'undefined') {
      return;
    }

    const mediaQueryList = window.matchMedia(query);

    // Initial check
    setMatches(mediaQueryList.matches);

    // Event listener callback
    const listener = (event: MediaQueryListEvent) => {
      setMatches(event.matches);
    };

    // Modern browsers support addEventListener on MediaQueryList,
    // older ones use addListener (deprecated but widely supported).
    // We check for addEventListener first.
    if (mediaQueryList.addEventListener) {
      mediaQueryList.addEventListener('change', listener);
    } else {
      // Fallback for older browsers (e.g., older Safari/iOS)
      // eslint-disable-next-line @typescript-eslint/no-explicit-any
      (mediaQueryList as any).addListener(listener);
    }

    // Cleanup function
    return () => {
      if (mediaQueryList.removeEventListener) {
        mediaQueryList.removeEventListener('change', listener);
      } else {
        // eslint-disable-next-line @typescript-eslint/no-explicit-any
        (mediaQueryList as any).removeListener(listener);
      }
    };
  }, [query]);

  return matches;
}