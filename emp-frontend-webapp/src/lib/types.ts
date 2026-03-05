/**
 * Core Type Definitions
 * dependency level: 0
 */

// Generic API Response Wrapper
export interface ApiResponse<T> {
  success: boolean;
  data?: T;
  error?: {
    code: string;
    message: string;
    details?: unknown;
  };
  meta?: {
    timestamp: string;
    requestId: string;
  };
}

// Pagination
export interface PaginatedResult<T> {
  items: T[];
  totalCount: number;
  page: number;
  pageSize: number;
  totalPages: number;
  hasNextPage: boolean;
  hasPreviousPage: boolean;
}

export interface PaginationParams {
  page: number;
  pageSize: number;
  sortBy?: string;
  sortDirection?: 'asc' | 'desc';
}

// User & Auth
export interface User {
  id: string;
  email: string;
  name: string;
  role: 'SystemAdministrator' | 'FinanceManager' | 'ClientContact' | 'VendorContact';
  mfaEnabled: boolean;
  avatarUrl?: string;
}

export interface AuthSession {
  user: User;
  accessToken: string;
  refreshToken: string;
  expiresAt: number;
}

// Vendor
export interface VendorDTO {
  id: string;
  companyName: string;
  status: 'PendingVetting' | 'Active' | 'Deactivated' | 'Blacklisted';
  primaryContactName: string;
  primaryContactEmail: string;
  skills: string[];
  createdAt: string;
}

// Client
export interface ClientDTO {
  id: string;
  companyName: string;
  status: 'Active' | 'Inactive';
  primaryContactName: string;
  primaryContactEmail: string;
  activeProjectsCount: number;
}

// Proposal
export interface ProposalDTO {
  id: string;
  projectId: string;
  vendorId: string;
  vendorName: string;
  cost: number;
  timeline: string;
  status: 'Submitted' | 'InReview' | 'Shortlisted' | 'Accepted' | 'Rejected';
  submittedAt: string;
}

// Common Filter Types
export interface BaseFilter extends PaginationParams {
  search?: string;
  status?: string[];
  dateFrom?: string;
  dateTo?: string;
}