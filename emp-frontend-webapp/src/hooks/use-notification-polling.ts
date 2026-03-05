'use client';

import { useEffect, useRef } from 'react';
import { useNotificationStore } from '@/store/use-notification-store';
import { useUiStore } from '@/store/use-ui-store';

interface PollingOptions {
  enabled?: boolean;
  intervalMs?: number;
  entityId?: string; // Optional: filter notifications by entity (project/user)
}

/**
 * Hook to poll for new notifications or specific entity status updates.
 * Used for real-time feedback on long-running processes like SOW ingestion.
 */
export function useNotificationPolling({
  enabled = true,
  intervalMs = 10000, // Default 10 seconds
  entityId,
}: PollingOptions = {}) {
  const { fetchNotifications, unreadCount } = useNotificationStore();
  const { showToast } = useUiStore();
  const timerRef = useRef<NodeJS.Timeout | null>(null);

  useEffect(() => {
    if (!enabled) {
      if (timerRef.current) clearInterval(timerRef.current);
      return;
    }

    const poll = async () => {
      try {
        const newNotifications = await fetchNotifications(entityId);
        
        // If critical notifications arrive, we can trigger a toast
        // This logic assumes fetchNotifications returns the latest batch or we check the store
        if (newNotifications && newNotifications.length > 0) {
          // Example: Show toast for the most recent critical alert
          const latest = newNotifications[0];
          if (latest.priority === 'HIGH' && !latest.read) {
            showToast({
              title: latest.title,
              message: latest.message,
              type: 'info',
            });
          }
        }
      } catch (error) {
        console.error('Notification polling failed:', error);
        // Implement exponential backoff or disable polling on repeated failures if needed
      }
    };

    // Initial fetch
    poll();

    // Set interval
    timerRef.current = setInterval(poll, intervalMs);

    return () => {
      if (timerRef.current) clearInterval(timerRef.current);
    };
  }, [enabled, intervalMs, entityId, fetchNotifications, showToast]);

  return {
    unreadCount,
  };
}