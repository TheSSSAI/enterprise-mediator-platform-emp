'use client';

import React, { useState } from 'react';
import { useNotificationStore } from '@/store/use-notification-store';
import { useNotificationPolling } from '@/hooks/use-notification-polling';
import { formatDistanceToNow } from 'date-fns';
import { BellIcon, CheckIcon, XMarkIcon } from '@heroicons/react/24/outline';

/**
 * NotificationCenter Component
 * 
 * Displays a bell icon with an unread badge.
 * expands into a dropdown list of recent notifications.
 * Uses polling hook to keep data fresh.
 */
export function NotificationCenter() {
  // Integrate polling mechanism (Level 3 Hook)
  useNotificationPolling();

  const { 
    notifications, 
    unreadCount, 
    markAsRead, 
    markAllAsRead, 
    removeNotification 
  } = useNotificationStore();

  const [isOpen, setIsOpen] = useState(false);

  const toggleDropdown = () => setIsOpen(!isOpen);

  const handleMarkAsRead = (id: string, e: React.MouseEvent) => {
    e.stopPropagation();
    markAsRead(id);
  };

  const handleRemove = (id: string, e: React.MouseEvent) => {
    e.stopPropagation();
    removeNotification(id);
  };

  return (
    <div className="relative z-50">
      {/* Trigger Button */}
      <button
        onClick={toggleDropdown}
        className="relative p-2 text-slate-500 hover:text-slate-700 focus:outline-none focus:ring-2 focus:ring-slate-200 rounded-full transition-colors"
        aria-label="Notifications"
        aria-expanded={isOpen}
      >
        <BellIcon className="h-6 w-6" />
        {unreadCount > 0 && (
          <span className="absolute top-1 right-1 flex h-4 w-4 items-center justify-center rounded-full bg-red-500 text-[10px] font-bold text-white ring-2 ring-white">
            {unreadCount > 9 ? '9+' : unreadCount}
          </span>
        )}
      </button>

      {/* Dropdown Panel */}
      {isOpen && (
        <>
          {/* Backdrop for mobile/click-outside */}
          <div 
            className="fixed inset-0 z-40" 
            onClick={() => setIsOpen(false)} 
            aria-hidden="true" 
          />
          
          <div className="absolute right-0 mt-2 w-80 sm:w-96 origin-top-right rounded-lg bg-white shadow-lg ring-1 ring-black ring-opacity-5 focus:outline-none z-50 overflow-hidden">
            {/* Header */}
            <div className="px-4 py-3 border-b border-slate-100 flex justify-between items-center bg-slate-50">
              <h3 className="text-sm font-semibold text-slate-900">Notifications</h3>
              {unreadCount > 0 && (
                <button
                  onClick={() => markAllAsRead()}
                  className="text-xs text-blue-600 hover:text-blue-800 font-medium"
                >
                  Mark all read
                </button>
              )}
            </div>

            {/* List */}
            <div className="max-h-[400px] overflow-y-auto">
              {notifications.length === 0 ? (
                <div className="px-4 py-8 text-center text-slate-500 text-sm">
                  No notifications yet.
                </div>
              ) : (
                <ul className="divide-y divide-slate-100">
                  {notifications.map((notification) => (
                    <li 
                      key={notification.id} 
                      className={`group relative px-4 py-3 hover:bg-slate-50 transition-colors ${
                        !notification.isRead ? 'bg-blue-50/50' : ''
                      }`}
                    >
                      <div className="flex justify-between items-start">
                        <div className="flex-1 pr-4">
                          <p className={`text-sm ${!notification.isRead ? 'font-semibold text-slate-900' : 'text-slate-700'}`}>
                            {notification.title}
                          </p>
                          <p className="text-xs text-slate-500 mt-1">
                            {notification.message}
                          </p>
                          <p className="text-[10px] text-slate-400 mt-2">
                            {formatDistanceToNow(new Date(notification.timestamp), { addSuffix: true })}
                          </p>
                        </div>
                        
                        {/* Actions */}
                        <div className="flex items-center space-x-1 opacity-0 group-hover:opacity-100 transition-opacity">
                          {!notification.isRead && (
                            <button
                              onClick={(e) => handleMarkAsRead(notification.id, e)}
                              className="p-1 text-slate-400 hover:text-blue-600 rounded"
                              title="Mark as read"
                            >
                              <CheckIcon className="h-4 w-4" />
                            </button>
                          )}
                          <button
                            onClick={(e) => handleRemove(notification.id, e)}
                            className="p-1 text-slate-400 hover:text-red-600 rounded"
                            title="Remove"
                          >
                            <XMarkIcon className="h-4 w-4" />
                          </button>
                        </div>
                      </div>
                      
                      {/* Unread Indicator Dot */}
                      {!notification.isRead && (
                        <div className="absolute left-2 top-4 h-2 w-2 rounded-full bg-blue-500" />
                      )}
                    </li>
                  ))}
                </ul>
              )}
            </div>
            
            {/* Footer */}
            <div className="bg-slate-50 px-4 py-2 border-t border-slate-100 text-center">
              <a href="/admin/notifications" className="text-xs text-slate-600 hover:text-slate-900 font-medium">
                View all notifications
              </a>
            </div>
          </div>
        </>
      )}
    </div>
  );
}