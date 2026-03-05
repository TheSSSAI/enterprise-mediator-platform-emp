import * as React from 'react';
import { useControllableState } from './useControllableState';

export interface UseDisclosureProps {
  isOpen?: boolean;
  defaultIsOpen?: boolean;
  onClose?: () => void;
  onOpen?: () => void;
  id?: string;
}

/**
 * A custom hook to manage common open/close states for UI components like
 * Modals, Drawers, Accordions, and Menus.
 *
 * It supports both controlled and uncontrolled usage via `useControllableState`.
 *
 * @param props - Configuration object
 * @returns Object containing state (isOpen) and handlers (onOpen, onClose, onToggle)
 */
export function useDisclosure(props: UseDisclosureProps = {}) {
  const { onClose: onCloseProp, onOpen: onOpenProp, isOpen: isOpenProp, id: idProp } = props;

  // Generate a unique ID if one wasn't provided, useful for ARIA attributes
  const [uniqueId] = React.useState(
    () => idProp || `disclosure-${Math.random().toString(36).substr(2, 9)}`,
  );

  // Manage state using the controllable pattern
  const [isOpen, setIsOpen] = useControllableState({
    prop: isOpenProp,
    defaultProp: props.defaultIsOpen || false,
    onChange: (isOpen) => {
      if (isOpen) {
        onOpenProp?.();
      } else {
        onCloseProp?.();
      }
    },
  });

  const onClose = React.useCallback(() => {
    setIsOpen(false);
  }, [setIsOpen]);

  const onOpen = React.useCallback(() => {
    setIsOpen(true);
  }, [setIsOpen]);

  const onToggle = React.useCallback(() => {
    setIsOpen((prev) => !prev);
  }, [setIsOpen]);

  return {
    isOpen: !!isOpen,
    onOpen,
    onClose,
    onToggle,
    isControlled: isOpenProp !== undefined,
    getButtonProps: (props: React.HTMLAttributes<HTMLElement> = {}) => ({
      ...props,
      'aria-expanded': isOpen,
      'aria-controls': uniqueId,
      onClick: (e: React.MouseEvent) => {
        props.onClick?.(e);
        onToggle();
      },
    }),
    getDisclosureProps: (props: React.HTMLAttributes<HTMLElement> = {}) => ({
      ...props,
      hidden: !isOpen,
      id: uniqueId,
    }),
  };
}