import { create } from 'zustand';
import { devtools } from 'zustand/middleware';

/**
 * Types of notifications supported by the system.
 */
export type NotificationType = 'success' | 'error' | 'info' | 'warning';

/**
 * Structure of a single notification object.
 */
export interface Notification {
  id: string;
  type: NotificationType;
  title?: string;
  message: string;
  duration?: number; // Duration in milliseconds, defaults to 5000
  isPersistent?: boolean; // If true, requires manual dismissal
}

/**
 * Payload for creating a new notification (ID is auto-generated).
 */
export type NotificationPayload = Omit<Notification, 'id'>;

/**
 * Interface for the Notification Store state and actions.
 */
interface NotificationState {
  notifications: Notification[];
  
  // Actions
  addNotification: (payload: NotificationPayload) => string;
  removeNotification: (id: string) => void;
  clearNotifications: () => void;
}

/**
 * Helper to generate simple unique IDs
 */
const generateId = (): string => {
  return Date.now().toString(36) + Math.random().toString(36).substring(2);
};

/**
 * Global store for managing toast notifications and alerts.
 * Used by the NotificationCenter component to render feedback to users.
 */
export const useNotificationStore = create<NotificationState>()(
  devtools(
    (set, get) => ({
      notifications: [],

      /**
       * Adds a new notification to the queue.
       * Automatically sets up a dismissal timer if the notification is not persistent.
       */
      addNotification: (payload: NotificationPayload) => {
        const id = generateId();
        const duration = payload.duration ?? 5000;
        
        const newNotification: Notification = {
          ...payload,
          id,
          duration,
        };

        set((state) => ({
          notifications: [...state.notifications, newNotification],
        }));

        // Handle auto-dismissal
        if (!payload.isPersistent) {
          setTimeout(() => {
            get().removeNotification(id);
          }, duration);
        }

        return id;
      },

      /**
       * Removes a specific notification by ID.
       */
      removeNotification: (id: string) => {
        set((state) => ({
          notifications: state.notifications.filter((n) => n.id !== id),
        }));
      },

      /**
       * Clears all active notifications.
       */
      clearNotifications: () => {
        set(() => ({ notifications: [] }));
      },
    }),
    { name: 'notification-store' }
  )
);