/**
 * Enterprise Mediator Platform Constants
 * dependency level: 0
 */

export const APP_NAME = process.env.NEXT_PUBLIC_APP_NAME || 'Enterprise Mediator Platform';

export const COOKIE_NAME = process.env.NEXT_PUBLIC_AUTH_COOKIE_NAME || 'emp_session';

export const DEFAULT_PAGE_SIZE = 20;

export const DATE_FORMAT = 'MMM dd, yyyy';
export const DATETIME_FORMAT = 'MMM dd, yyyy HH:mm';

export const FILE_UPLOAD_MAX_SIZE = 10 * 1024 * 1024; // 10MB
export const ACCEPTED_FILE_TYPES = [
  'application/pdf',
  'application/vnd.openxmlformats-officedocument.wordprocessingml.document', // .docx
];

export const API_ENDPOINTS = {
  AUTH: {
    LOGIN: '/auth/login',
    LOGOUT: '/auth/logout',
    REFRESH: '/auth/refresh',
    ME: '/auth/me',
    MFA_SETUP: '/auth/mfa/setup',
    MFA_VERIFY: '/auth/mfa/verify',
  },
  PROJECTS: {
    BASE: '/projects',
    SOW: (id: string) => `/projects/${id}/sow`,
    SOW_EXTRACT: (id: string) => `/projects/${id}/sow/extract`,
    BRIEF: (id: string) => `/projects/${id}/brief`,
    VENDORS: (id: string) => `/projects/${id}/vendors`,
  },
  VENDORS: {
    BASE: '/vendors',
    PROFILE: (id: string) => `/vendors/${id}`,
  },
  CLIENTS: {
    BASE: '/clients',
  },
  FINANCE: {
    TRANSACTIONS: '/finance/transactions',
    PAYOUTS: '/finance/payouts',
    INVOICES: '/finance/invoices',
  },
  AUDIT: {
    LOGS: '/audit/logs',
  },
} as const;

export const ROLES = {
  ADMIN: 'SystemAdministrator',
  FINANCE: 'FinanceManager',
  CLIENT: 'ClientContact',
  VENDOR: 'VendorContact',
} as const;

export const STATUS_COLORS = {
  ACTIVE: 'bg-green-500',
  PENDING: 'bg-yellow-500',
  INACTIVE: 'bg-gray-500',
  REJECTED: 'bg-red-500',
  COMPLETED: 'bg-blue-500',
} as const;