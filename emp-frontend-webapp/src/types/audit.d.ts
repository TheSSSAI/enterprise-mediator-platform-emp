export type AuditActionType =
  | 'LOGIN'
  | 'LOGOUT'
  | 'CREATE'
  | 'UPDATE'
  | 'DELETE'
  | 'UPLOAD'
  | 'APPROVE'
  | 'REJECT'
  | 'PAYMENT'
  | 'CONFIGURATION';

export type AuditEntityType =
  | 'USER'
  | 'PROJECT'
  | 'CLIENT'
  | 'VENDOR'
  | 'PROPOSAL'
  | 'INVOICE'
  | 'PAYOUT'
  | 'SOW';

export interface AuditLogEntry {
  id: string;
  timestamp: string;
  actorId: string;
  actorName: string;
  actorEmail: string;
  actorRole: string;
  ipAddress: string;
  userAgent: string;
  action: AuditActionType;
  entityType: AuditEntityType;
  entityId: string;
  entityName?: string;
  details?: string; // JSON string of changes or details
  metadata?: Record<string, unknown>;
  status: 'SUCCESS' | 'FAILURE';
  failureReason?: string;
}

export interface AuditLogFilter {
  actorId?: string;
  action?: AuditActionType[];
  entityType?: AuditEntityType[];
  dateFrom?: string;
  dateTo?: string;
  searchTerm?: string;
}