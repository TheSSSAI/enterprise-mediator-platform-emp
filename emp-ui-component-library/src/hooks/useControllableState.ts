import * as React from 'react';

/**
 * Configuration options for useControllableState
 */
interface UseControllableStateParams<T> {
  prop?: T | undefined;
  defaultProp?: T | undefined;
  onChange?: (state: T) => void;
}

/**
 * A custom hook that manages the state of a component which can be either controlled or uncontrolled.
 *
 * Controlled: The state is driven by the `prop` passed from the parent. The parent listens to `onChange` to update the state.
 * Uncontrolled: The state is driven internally. The `defaultProp` sets the initial value.
 *
 * This pattern is widely used in Radix UI and other component libraries to provide flexibility to consumers.
 *
 * @param params - Configuration object containing prop, defaultProp, and onChange handler
 * @returns [state, setState] - A tuple containing the current state and a setter function
 */
export function useControllableState<T>({
  prop,
  defaultProp,
  onChange = () => {},
}: UseControllableStateParams<T>) {
  // Determine if the component is being controlled by checking if 'prop' is defined.
  // We use useRef to keep track of whether it was initially controlled or uncontrolled
  // to warn in dev mode if it switches, though that logic is omitted here for brevity.
  const [uncontrolledProp, setUncontrolledProp] = React.useState<T | undefined>(defaultProp);
  const isControlled = prop !== undefined;

  // The current value is the prop if controlled, otherwise the internal state
  const value = isControlled ? prop : uncontrolledProp;

  // We wrap the onChange prop in a useRef to avoid re-binding the callback unnecessarily
  const handleChange = React.useCallbackRef
    ? React.useCallbackRef(onChange)
    : // Fallback for older React or if useCallbackRef is not available in environment
      // (Simplified implementation of useCallbackRef logic)
      React.useRef(onChange);

  React.useEffect(() => {
    handleChange.current = onChange;
  }, [onChange, handleChange]);

  const setValue: React.Dispatch<React.SetStateAction<T | undefined>> = React.useCallback(
    (nextValue) => {
      if (isControlled) {
        // If controlled, we just call the onChange handler with the new value.
        // We calculate the new value if a function update was passed.
        const setter = nextValue as (prevState?: T) => T;
        const valueToEmit = typeof nextValue === 'function' ? setter(prop) : nextValue;
        if (valueToEmit !== prop) {
          handleChange.current(valueToEmit as T);
        }
      } else {
        // If uncontrolled, we update the internal state AND call onChange
        setUncontrolledProp((prev) => {
          const setter = nextValue as (prevState?: T) => T;
          const valueToSet = typeof nextValue === 'function' ? setter(prev) : nextValue;
          if (valueToSet !== prev) {
            handleChange.current(valueToSet as T);
          }
          return valueToSet;
        });
      }
    },
    [isControlled, prop, setUncontrolledProp, handleChange],
  );

  return [value, setValue] as const;
}