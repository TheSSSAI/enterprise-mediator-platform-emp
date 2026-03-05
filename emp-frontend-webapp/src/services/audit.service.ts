import { apiClient } from '@/services/api-client';
import type { 
  AuditLogDTO, 
  AuditLogFilter, 
  PaginatedResponse 
} from '@/lib/types';

/**
 * Service responsible for Observability and Compliance.
 * Handles retrieval and export of immutable audit logs.
 */
class AuditService {
  private readonly baseUrl = '/audit-logs';

  /**
   * Retrieves a paginated list of audit logs based on filtering criteria.
   * @param filters Optional filters for date range, user, action type, and entity.
   * @returns A paginated list of audit log entries.
   */
  async getAuditLogs(filters?: AuditLogFilter): Promise<PaginatedResponse<AuditLogDTO>> {
    const queryParams = new URLSearchParams();

    if (filters) {
      if (filters.page) queryParams.append('page', filters.page.toString());
      if (filters.limit) queryParams.append('limit', filters.limit.toString());
      if (filters.userId) queryParams.append('userId', filters.userId);
      if (filters.action) queryParams.append('action', filters.action);
      if (filters.entityId) queryParams.append('entityId', filters.entityId);
      if (filters.startDate) queryParams.append('startDate', filters.startDate);
      if (filters.endDate) queryParams.append('endDate', filters.endDate);
    }

    const queryString = queryParams.toString();
    const endpoint = queryString ? `${this.baseUrl}?${queryString}` : this.baseUrl;

    return await apiClient.get<PaginatedResponse<AuditLogDTO>>(endpoint, ['audit-logs']);
  }

  /**
   * Retrieves the full detail of a specific audit log entry, including diffs.
   * @param id The unique identifier of the audit log entry.
   * @returns The detailed audit log DTO.
   */
  async getAuditLogById(id: string): Promise<AuditLogDTO> {
    return await apiClient.get<AuditLogDTO>(`${this.baseUrl}/${id}`, [`audit-log-${id}`]);
  }

  /**
   * Initiates an export of audit logs matching the provided filters.
   * @param filters Filters to define the scope of the export.
   * @returns A URL or Blob reference to the exported file.
   */
  async exportAuditLogs(filters?: AuditLogFilter): Promise<void> {
    const queryParams = new URLSearchParams();
    
    if (filters) {
      if (filters.startDate) queryParams.append('startDate', filters.startDate);
      if (filters.endDate) queryParams.append('endDate', filters.endDate);
      // Other filters as needed for export context
    }

    const queryString = queryParams.toString();
    const endpoint = queryString ? `${this.baseUrl}/export?${queryString}` : `${this.baseUrl}/export`;

    return await apiClient.post<void>(endpoint, {});
  }
}

export const auditService = new AuditService();